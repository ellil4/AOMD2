using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QAIF;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;

namespace AOMD2Analyzer
{
    public class Kernel
    {
        public Reader mReader;
        FrameForm mForm;
        QAIFDataSpanInfo mFileInfo;
        PictureBox mScreen;

        Color mColorPriamry;
        Color mColorSecondary;
        Color mColorDiff;

        //data
        public byte[] mDataBufferM;//Main
        public byte[] mDataBufferA;//before
        public byte[] mDataBufferA2;//after

        //screen
        Bitmap mBMPScreen;
        BitmapData mBMPD;
        unsafe byte* mpCursor;

        public static long DATALEN = 640 * 480 * 2;
        public static long PIXLINELEN = 640;
        public static long PIXLINELEN3 = 640 * 3;
        public static long PIXLINECOUNT = 480;

        public static Decimal EPSILON = (Decimal)0.000000001;

        NoiseProcessor mNP;
        Dictionary<int, NPPoint> mMapCheckedPixels;

        public Kernel(FrameForm form)
        {
            mForm = form;
            mScreen = mForm.GetPicBox();

            mDataBufferM = new byte[DATALEN];
            mDataBufferA = new byte[DATALEN];
            mDataBufferA2 = new byte[DATALEN];

            mBMPScreen = new Bitmap(640, 480, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            mColorPriamry = Color.FromArgb(238, 30, 255);
            mColorSecondary = Color.FromArgb(56, 47, 238);
            mColorDiff = Color.FromArgb(255, 216, 0);

            mScreen.Image = mBMPScreen;
            
            mNP = new NoiseProcessor(this);
            mMapCheckedPixels = new Dictionary<int, NPPoint>();
        }

        public QAIFDataSpanInfo Set(String path)
        {
            if(mReader != null)
                mReader.Finish();

            QAIFDataSpanInfo retval = QAIF.QAIF.GetFileSpanInfo(path);
            mFileInfo = retval;
            mReader = new Reader(path);
            return retval;
        }

        public void Finish()
        {
            mReader.Finish();
        }

        public void ReadAdjacentFrames(long index)
        {
            mReader.GetData(ref mDataBufferA, index - 1);
            mReader.GetData(ref mDataBufferM, index);
            mReader.GetData(ref mDataBufferA2, index + 1);
        }

        public void FastOneWayRead(long index)
        {
            byte[] transPos = mDataBufferA;
            mDataBufferA = mDataBufferM;
            mDataBufferM = mDataBufferA2;
            mDataBufferA2 = transPos;
            mReader.GetData(ref mDataBufferA2, index + 1);
        }

        private decimal[] mGCOutVal;
        private int mGCFrom;
        private int mGCTo;

        public void SetGlobalCalculateParas(decimal[] outVal, StIntegerPair pair)
        {
            mGCOutVal = outVal;

            for (int i = 0; i < 4; i++)
            {
                mGCFrom = pair.from;
                mGCTo = pair.to;
            }

        }

        private delegate void setProgressBarDelegate();
        private delegate void closeProgressBarStatusDelegate();

        public void GlobalCalculate()//run in sub thread
        {
            lock (mGCOutVal)//////////////////////////////////////////////////////////////////////remove
            {
                long totalFrameCount = mReader.GetItemCount();
                //float[] retval = new float[frameTo - frameFrom];

                ReadAdjacentFrames(mGCFrom);/////////////////////////////core recog
                mGCOutVal[0] = Compare();////////////////////////core recog

                int retIDXUpdateMark = 1 + 100;

                for (int i = mGCFrom + 1, retIDX = 1; i <= mGCTo; i++, retIDX++)
                {
                    //core
                    FastOneWayRead(i);//////////////////////////core recog
                    mGCOutVal[retIDX] = Compare();

                    //UI
                    if (retIDX == retIDXUpdateMark)
                    {
                        retIDXUpdateMark += 100;
                        mForm.Invoke(new setProgressBarDelegate(mForm.mProgWnd.SetProgress));
                    }
                }

                mForm.Invoke(new closeProgressBarStatusDelegate(mForm.ThreadFinishRegister));
                Console.WriteLine("finished invoked");
            }

        }

        public decimal DisplayCompare()
        {
            decimal retval = Compare();
            Display();

            return retval;
        }

        public decimal Compare()
        {
            long lineByteCount = PIXLINELEN * 2;

            int userPxCt = 0, changedPxCt = 0;

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            unsafe
            {
                mBMPD = mBMPScreen.LockBits(
                    new Rectangle(0, 0, mBMPScreen.Width, mBMPScreen.Height),
                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                mpCursor = (byte*)mBMPD.Scan0;

                for (int i = 0; i < PIXLINECOUNT; i++)
                {
                    int j2;
                    short pxM, pxA, pxA2;

                    int baseOff = i * (int)lineByteCount;
                    for (int j = 0; j < PIXLINELEN; j++)
                    {
                        j2 = j * 2;
                        pxM = BitConverter.ToInt16(mDataBufferM, (int)(j2 + baseOff));
                        pxA = BitConverter.ToInt16(mDataBufferA, (int)(j2 + baseOff));
                        pxA2 = BitConverter.ToInt16(mDataBufferA2, (int)(j2 + baseOff));

                        if (pxM != -1)
                        {
                            if (NoiseProcessor.ValueCompare(pxA, pxM, pxA2, 4))//diff
                            {
                                //gold
                                cppSetPxColor(mColorDiff, j, i);

                                //*mpCursor = 0;
                                //mpCursor++;
                                //*mpCursor = 216;
                                //mpCursor++;
                                //*mpCursor = 255;
                                //mpCursor++;
                                //mBMPScreen.SetPixel(j, i, mColorDiff);
                                changedPxCt++;
                            }
                            else//primary
                            {
                                //purple
                                cppSetPxColor(mColorPriamry, j, i);

                                //*mpCursor = 255;
                                //mpCursor++;
                                //*mpCursor = 30;
                                //mpCursor++;
                                //*mpCursor = 238;
                                //mpCursor++;
                                //mBMPScreen.SetPixel(j, i, mColorPriamry);
                            }

                            userPxCt++;
                        }
                        else if (pxA != -1)//secondary
                        {
                            //blue
                            cppSetPxColor(mColorSecondary, j, i);

                            //*mpCursor = 238;
                            //mpCursor++;
                            //*mpCursor = 47;
                            //mpCursor++;
                            //*mpCursor = 56;
                            //mpCursor++;
                            //mBMPScreen.SetPixel(j, i, mColorSecondary);
                        }
                        else//black
                        {
                            cppSetPxColor(Color.Black, j, i);

                            //*mpCursor = 0;
                            //mpCursor++;
                            //*mpCursor = 0;
                            //mpCursor++;
                            //*mpCursor = 0;
                            //mpCursor++;
                            //mBMPScreen.SetPixel(j, i, Color.Black);
                        }
                    }

                    mpCursor += mBMPD.Stride - PIXLINELEN3;

                }

                mBMPScreen.UnlockBits(mBMPD);
            }

            hotpixelDenoise(mColorDiff, mColorPriamry, 22, ref changedPxCt);

            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);
            if (userPxCt != 0)
            {
                return (decimal)changedPxCt / (decimal)userPxCt;
            }
            else
            {
                return decimal.Zero;
            }
        }

        unsafe private void cppGetCursorAt(int x, int y)
        {
            mpCursor = (byte*)mBMPD.Scan0;
            //move to
            mpCursor += 3 * x + y * mBMPD.Stride;
        }

        unsafe public Color cppGetPxColor(int x, int y)
        {
            cppGetCursorAt(x, y);

            byte b = *mpCursor;
            mpCursor++;
            byte g = *mpCursor;
            mpCursor++;
            byte r = *mpCursor;

            Color retval = Color.FromArgb(r, g, b);
            return retval;
        }

        unsafe public void cppSetPxColor(Color color, int x, int y)
        {
            cppGetCursorAt(x, y);

            *mpCursor = color.B;
            mpCursor++;
            *mpCursor = color.G;
            mpCursor++;
            *mpCursor = color.R;
        }

        private static decimal getSD(decimal mean, decimal[] data)
        {
            decimal retval = 0;
            decimal dataBuf = 0;
            for (int i = 0; i < data.Length; i++)
            {
                dataBuf = data[i];
                retval += (decimal)Math.Pow((double)(dataBuf - mean), 2);
            }

            retval /= data.Length;
            retval = (decimal)Math.Sqrt((double)retval);

            return retval;
        }

        public static StPostAnalyzeResult PostAnalyze(Decimal[] data, 
            out List<StNormalizedIntensity> pointIntensities,
            Decimal noise, int frameFrom, int frameTo)
        {
            StPostAnalyzeResult retval = new StPostAnalyzeResult();
            pointIntensities = new List<StNormalizedIntensity>();
            List<Decimal> accuIntensities = new List<Decimal>();

            Decimal curSpanIntensityBuf = 0;
            int curSpanFrameCount = 0;

            bool OnMove = false;
            for (int i = frameFrom; i <= frameTo; i++)
            {
                if (data[i] > noise && OnMove == false)//status change to On
                {
                    OnMove = true;
                    retval.MovementCount++;
                    curSpanFrameCount++;
                    curSpanIntensityBuf += (data[i] - noise);
                }
                else if(data[i] < noise && OnMove == true)//status change to Off
                {
                    OnMove = false;

                    accuIntensities.Add(curSpanIntensityBuf);

                    StNormalizedIntensity intensity = new StNormalizedIntensity();
                    intensity.Intensity = curSpanIntensityBuf / curSpanFrameCount;
                    intensity.Duration = curSpanFrameCount;
                    pointIntensities.Add(intensity);

                    curSpanIntensityBuf = 0;
                    curSpanFrameCount = 0;
                }
                else if (data[i] > noise && OnMove == true)//still On
                {
                    curSpanFrameCount++;
                    curSpanIntensityBuf += (data[i] - noise);
                }
                else //still off
                {
                    //immobility duration
                    retval.ImmobilityDureation++;
                }
            }

            if (curSpanIntensityBuf - 0 > EPSILON)
            {
                accuIntensities.Add(curSpanIntensityBuf);

                StNormalizedIntensity intenLeft = new StNormalizedIntensity();
                intenLeft.Intensity = curSpanIntensityBuf / curSpanFrameCount;
                intenLeft.Duration = curSpanFrameCount;
                pointIntensities.Add(intenLeft);
            }

            //frame count
            retval.FrameCount = frameTo - frameFrom + 1;//multiple span mani till here

            //intentsity mean
            for(int i = 0; i < accuIntensities.Count; i++)
            {
                retval.MovementIntentsityMean += accuIntensities[i];
            }

            if (accuIntensities.Count != 0)
                retval.MovementIntentsityMean /= accuIntensities.Count;
            else
                retval.MovementIntentsityMean = 0;

            //intentsity SD
            retval.MovementIntensitySD =
                getSD(retval.MovementIntentsityMean, accuIntensities.ToArray());

            return retval;
        }

        private void hotpixelDenoise(Color blockColor, Color nonBlockColor,
            int minTolerance, ref int changedPxCt)
        {
            unsafe
            {
                mBMPD = mBMPScreen.LockBits(
                    new Rectangle(0, 0, mBMPScreen.Width, mBMPScreen.Height),
                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                byte* mpCursor = (byte*)mBMPD.Scan0;

                for (int y = 0; y < Kernel.PIXLINECOUNT; y++)
                {
                    //debug
                    for (int x = 0; x < Kernel.PIXLINELEN; x++)
                    {
                        Color curPixelColor = cppGetPxColor(x, y);

                        //if is a target block and has not been checked
                        if (curPixelColor.ToArgb() == blockColor.ToArgb() &&
                            !mMapCheckedPixels.ContainsKey(NoiseProcessor.GetKey(x, y)))
                        {
                            //to find how large the block is
                            mNP.DoBlockStatistics(x, y, blockColor);

                            int[] keys = mNP.mMapCurArea.Keys.ToArray();

                            for (int i = 0; i < keys.Length; i++)
                            {
                                //keep the block in the total
                                if (mNP.mMapCurArea.Count >= minTolerance)
                                {
                                    mMapCheckedPixels.Add(keys[i], mNP.mMapCurArea[keys[i]]);
                                }
                                else //remove from bitmap
                                {
                                    NPPoint point = mNP.mMapCurArea[keys[i]];

                                    cppSetPxColor(nonBlockColor, point.x, point.y);

                                    //bmp.SetPixel(point.x, point.y, nonBlockColor);
                                    changedPxCt--;
                                }
                            }

                            mNP.ClearBlockInfo();
                        }
                    }
                }

                mMapCheckedPixels.Clear();
                mBMPScreen.UnlockBits(mBMPD);
            }
        }

        public void Display()
        {
            mScreen.Image = (Image)mBMPScreen;
        }

        public void PlayFrom(long begIndex, int frameRate)
        {
            long curIndexM = begIndex;
            //read primary
            //mReader.GetData(mDataBufferM, 
            //read secondary
        }
    }
}
