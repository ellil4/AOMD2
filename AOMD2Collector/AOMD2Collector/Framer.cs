using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using QAIF;

namespace AOMD2Collector
{
    //file format: [index][depth]
    public class Framer
    {
        public static int HEIGHT = 480;
        public static int WIDTH = 640;

        private WriteableBitmap mWBmp;
        private Image mImage;
        private byte[] mPxData;
        private short[] mDepthData;
        private short[] mFileFrameBuffer;
        private static int mPxStride = 4 * WIDTH;
        private long mGraphIndex = 0;

        public Writer mWT;
        public bool WRITER_ON = false;

        public void StartWriter(String path)
        {
            mWT = new Writer(path, false);
            WRITER_ON = true;
            mGraphIndex = 0;
        }

        public void FinishWriter()
        {
            WRITER_ON = false;
            mWT.Finish();
        }

        public Framer(Image image)
        {
            mWBmp = new WriteableBitmap(WIDTH, HEIGHT, 96, 96, 
                PixelFormats.Bgr32, null);

            mImage = image;

            mImage.Source = mWBmp;

            mPxData = new byte[HEIGHT * mPxStride];
        }

        public void ColorImage2Screen(AllFramesReadyEventArgs REArgs)
        {
            using (ColorImageFrame colorFrame = REArgs.OpenColorImageFrame())
            {
                if (colorFrame == null)
                    return;

                colorFrame.CopyPixelDataTo(mPxData);

                mWBmp.WritePixels(
                        new Int32Rect(0, 0, WIDTH, HEIGHT), mPxData, mPxStride, 0);
            }
        }

        public void DepthImage2Screen(AllFramesReadyEventArgs REArgs)
        {
            using (DepthImageFrame depthFrame = REArgs.OpenDepthImageFrame())
            {
                if (depthFrame == null)
                    return;

                generateColoredBytes(depthFrame);

                mWBmp.WritePixels(
                    new Int32Rect(0, 0, WIDTH, HEIGHT), mPxData, mPxStride, 0);
            }
            
        }

        private byte getIntensity(int depth)
        {
            float percentage = (depth - 900f) / 7100f;
            byte retval = (byte)(255 * (1 - percentage));
            return retval;
        }

        private void generateColoredBytes(DepthImageFrame depthFrame)
        {
            if (mDepthData == null ||
                mDepthData.Length != depthFrame.PixelDataLength)
            {
                mDepthData = new short[depthFrame.PixelDataLength];

                if (WRITER_ON)
                    mFileFrameBuffer = new short[depthFrame.PixelDataLength];
            }

            if (mFileFrameBuffer == null)
            {
                if (WRITER_ON)
                    mFileFrameBuffer = new short[depthFrame.PixelDataLength];
            }

            depthFrame.CopyPixelDataTo(mDepthData);

            const int BlueIndex = 0;
            const int GreenIndex = 1;
            const int RedIndex = 2;

            int player = 0;
            short depth = 0;
            //byte intensity = 0;

            for (int depthIndex = 0, colorIndex = 0;
                depthIndex < mDepthData.Length && colorIndex < mPxData.Length;
                depthIndex++, colorIndex += 4)
            {
                player = mDepthData[depthIndex] & DepthImageFrame.PlayerIndexBitmask;
                depth = (short)(mDepthData[depthIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth);

                //intensity = getIntensity(depth);

                if (player > 0)
                {
                    mPxData[colorIndex + BlueIndex] = 14;
                    mPxData[colorIndex + GreenIndex] = 201;
                    mPxData[colorIndex + RedIndex] = 255;

                    if (WRITER_ON)
                        mFileFrameBuffer[depthIndex] = depth;
                }
                else
                {
                    mPxData[colorIndex + BlueIndex] = 0;
                    mPxData[colorIndex + GreenIndex] = 0;
                    mPxData[colorIndex + RedIndex] = 0;

                    if (WRITER_ON)
                        mFileFrameBuffer[depthIndex] = -1;
                }
            }

            if (WRITER_ON)
            {
                mWT.Add(mFileFrameBuffer, mGraphIndex);
                mGraphIndex++;
            }
        }
    }
}
