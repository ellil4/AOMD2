using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Printing;
using System.Threading;
using System.IO;

namespace AOMD2Analyzer
{
    public partial class FrameForm : Form
    {
        private Kernel mKernel;
        long mCurFrameIndex = 1;
        bool mIsOn = false;
        private bool mGlobalyCalculated = false;
        public ProgressWnd mProgWnd;
        private int mFinishedThreads = 0;
        private String mPath;

        public int mShowingFrameFrom, mShowingFrameTo;
        public Decimal[] mGlobalData;
        public Decimal mNoiseSampleMean = 0;

        public StPostAnalyzeResult mPostResult;

        public FrameForm()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

            mKernel = new Kernel(this);

            //test
            Focus();

        }

        public PictureBox GetPicBox()
        {
            return amPicBoxScreen;
        }

        delegate void UpdateFrameDelegate();

        void mTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new UpdateFrameDelegate(frameForward));
        }

        private void frameForward()
        {
            if (mIsOn && (mCurFrameIndex + 1) < mKernel.mReader.GetItemCount())
            {
                mCurFrameIndex++;
                mKernel.ReadAdjacentFrames(mCurFrameIndex);
                DisplayCompare();
                amTextBoxFrame.Text = mCurFrameIndex.ToString();
            }
        }

        private void amBtnF_Click(object sender, EventArgs e)
        {
            frameForward();
        }

        private void frameReverse()
        {
            if (mIsOn && mCurFrameIndex > 1)
            {
                mCurFrameIndex--;
                mKernel.ReadAdjacentFrames(mCurFrameIndex);
                DisplayCompare();
                amTextBoxFrame.Text = mCurFrameIndex.ToString();
            }
        }

        private void amBtnR_Click(object sender, EventArgs e)
        {
            frameReverse();
        }

        private void amBtnRev_Click(object sender, EventArgs e)
        {

        }

        private void amBtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open";
            ofd.Filter = "(*." + QAIF.QAIF.INDEX_EXT + ")|*." + QAIF.QAIF.INDEX_EXT;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mIsOn = true;
                mGlobalyCalculated = false;
                mPath = ofd.FileName;
                mKernel.Set(mPath);
                mKernel.ReadAdjacentFrames(mCurFrameIndex);
                DisplayCompare();
                amTextBoxFrame.Text = mCurFrameIndex.ToString();
            }
        }

        private void updateFromFrameBox()
        {
            if (mIsOn)
            {
                int Inputindex;
                if (int.TryParse(amTextBoxFrame.Text, out Inputindex))
                {
                    if (Inputindex > 0 && Inputindex < mKernel.mReader.GetItemCount())
                    {
                        mKernel.ReadAdjacentFrames(Inputindex);
                        DisplayCompare();
                        mCurFrameIndex = Inputindex;
                    }
                }
            }
        }

        private void DisplayCompare()
        {
            decimal perctenage = mKernel.DisplayCompare();
            perctenage *= 100;
            amLabelPercentage.Text = perctenage.ToString("00.00000") + "%";
        }

        private void amTextBoxFrame_Leave(object sender, EventArgs e)
        {
            updateFromFrameBox();
        }

        private void amTextBoxFrame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                updateFromFrameBox();
            }
            else if (e.KeyCode == Keys.Right)
            {
                frameForward();
            }
            else if (e.KeyCode == Keys.Left)
            {
                frameReverse();
            }
        }

        private StIntegerPair[] QualSplit(int start, int end)
        {
            int len = end - start + 1;
            StIntegerPair[] retval = new StIntegerPair[4];
            for (int i = 0; i < 4; i++)
            {
                retval[i] = new StIntegerPair();
            }

            int step = len / 4;
            retval[0].from = start;
            retval[0].to = start + step - 1;
            retval[1].from = retval[0].to + 1;
            retval[1].to = retval[1].from + step - 1;
            retval[2].from = retval[1].to + 1;
            retval[2].to = retval[2].from + step - 1;
            retval[3].from = retval[2].to + 1;
            retval[3].to = end;

            return retval;
        }

        public void ThreadFinishRegister()
        {
            mFinishedThreads++;

            if (mFinishedThreads == 4)
            {
                mProgWnd.Close();
                mFinishedThreads = 0;
            }
        }

        private void amBtnGlobal_Click(object sender, EventArgs e)
        {
            if (mIsOn)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "ddf文件|*.ddf";
                sfd.Title = "保存档案";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    mShowingFrameFrom = 30;
                    mShowingFrameTo = (int)mKernel.mReader.GetItemCount() - 30;
                    mProgWnd = new ProgressWnd(mShowingFrameTo - mShowingFrameFrom);

                    StIntegerPair[] pairs = QualSplit(mShowingFrameFrom, mShowingFrameTo);

                    //buffers

                    decimal[] subCurveValues0 = new decimal[pairs[0].to - pairs[0].from + 1];
                    decimal[] subCurveValues1 = new decimal[pairs[1].to - pairs[1].from + 1];
                    decimal[] subCurveValues2 = new decimal[pairs[2].to - pairs[2].from + 1];
                    decimal[] subCurveValues3 = new decimal[pairs[3].to - pairs[3].from + 1];
                    decimal[][] subCurveValuesArr = new decimal[4][];
                    subCurveValuesArr[0] = subCurveValues0;
                    subCurveValuesArr[1] = subCurveValues1;
                    subCurveValuesArr[2] = subCurveValues2;
                    subCurveValuesArr[3] = subCurveValues3;

                    mKernel.SetGlobalCalculateParas(subCurveValues0, pairs[0]);//0

                    Kernel kernel1 = new Kernel(this);
                    kernel1.Set(mPath);
                    kernel1.SetGlobalCalculateParas(subCurveValues1, pairs[1]);//1

                    Kernel kernel2 = new Kernel(this);
                    kernel2.Set(mPath);
                    kernel2.SetGlobalCalculateParas(subCurveValues2, pairs[2]);//2

                    Kernel kernel3 = new Kernel(this);
                    kernel3.Set(mPath);
                    kernel3.SetGlobalCalculateParas(subCurveValues3, pairs[3]);//3

                    Thread workerThread0 = new Thread(mKernel.GlobalCalculate);
                    workerThread0.Priority = ThreadPriority.Normal;
                    workerThread0.Start();

                    Thread workerThread1 = new Thread(kernel1.GlobalCalculate);
                    workerThread1.Priority = ThreadPriority.Normal;
                    workerThread1.Start();

                    Thread workerThread2 = new Thread(kernel2.GlobalCalculate);
                    workerThread2.Priority = ThreadPriority.Normal;
                    workerThread2.Start();


                    Thread workerThread3 = new Thread(kernel3.GlobalCalculate);
                    workerThread3.Priority = ThreadPriority.Normal;
                    workerThread3.Start();

                    mProgWnd = new ProgressWnd(mShowingFrameTo - mShowingFrameFrom);
                    mProgWnd.ShowDialog();

                    //after processing

                    mGlobalyCalculated = true;

                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                    BinaryWriter bw = new BinaryWriter(fs);

                    int pointsCount = 0;
                    for (int i = 0; i < subCurveValuesArr.Length; i++)
                    {
                        pointsCount += subCurveValuesArr[i].Length;

                        for (int j = 0; j < subCurveValuesArr[i].Length; j++)
                        {
                            bw.Write(subCurveValuesArr[i][j]);
                        }
                    }

                    workerThread0.Abort();
                    workerThread1.Abort();
                    workerThread2.Abort();
                    workerThread3.Abort();

                    if (fs != null)
                        fs.Close();

                    if(bw != null)
                        bw.Close();

                    dataAnalyze(sfd.FileName);
                    UpdateAnalyzeInfo();
                }
            }
        }

        private void dataAnalyze(String path)
        {
            mGlobalData = load(path);

            SetGraph(mGlobalData, mShowingFrameFrom, mShowingFrameTo);

            List<StNormalizedIntensity> intensities = null;
            mPostResult = Kernel.PostAnalyze(mGlobalData, out intensities,
                mNoiseSampleMean, 
                mShowingFrameFrom, mShowingFrameTo);

            SetHistogram(intensities);
        }

        //print
        private void amBtnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog prtDlg = new PrintDialog();
            if (prtDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PrintDocument prtDoc = new PrintDocument();
                prtDoc.PrintPage += new PrintPageEventHandler(prtDoc_PrintPage);
                try
                {
                    prtDoc.Print();
                }
                catch (Exception)
                { }
            }
        }

        private void prtDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charSize = 11;
            int baseY = 100;
            int yAddOn = 40;

            Bitmap chartBitmap = new Bitmap(amChart.Width, amChart.Height);
            amChart.DrawToBitmap(chartBitmap,
                new Rectangle(0, 0, amChart.Width, amChart.Height));

            Bitmap histogramBitmap = new Bitmap(amHistogram.Width, amHistogram.Height);
            amHistogram.DrawToBitmap(histogramBitmap,
                new Rectangle(0, 0, amHistogram.Width, amHistogram.Height));

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            e.Graphics.DrawString("AOMD动作测验报告",
                new Font(new FontFamily("黑体"), 15), Brushes.Black, 320, 20);

            e.Graphics.DrawString("静止时长：" + mPostResult.ImmobilityDureation + " 帧",
                new Font(new FontFamily("黑体"), charSize), Brushes.Black, 20, baseY);

            e.Graphics.DrawString("运动次数统计：" + mPostResult.MovementCount,
                new Font(new FontFamily("黑体"), charSize), Brushes.Black, 20, baseY + yAddOn);

            e.Graphics.DrawString("运动平均强度：" + mPostResult.MovementIntentsityMean.ToString("0.000000"),
                new Font(new FontFamily("黑体"), charSize), Brushes.Black, 20, baseY + yAddOn * 2);

            e.Graphics.DrawString("运动强度标准差：" + mPostResult.MovementIntensitySD.ToString("0.000000"),
                new Font(new FontFamily("黑体"), charSize), Brushes.Black, 20, baseY + yAddOn * 3);

            e.Graphics.DrawString("时间-强度：", new Font(new FontFamily("黑体"), charSize), Brushes.Black, 20, baseY + yAddOn * 4 + 20);

            e.Graphics.DrawImageUnscaled(chartBitmap, 20, baseY + yAddOn * 5 + 20);

            e.Graphics.DrawString("强度分布：", new Font(new FontFamily("黑体"), charSize), Brushes.Black, 20, baseY + yAddOn * 5 + 40 + chartBitmap.Height);

            e.Graphics.DrawImageUnscaled(histogramBitmap, 20, baseY + yAddOn * 6 + 40 + chartBitmap.Height);
        }
        
        private decimal[] load(String path)
        {
            long len = (new FileInfo(path)).Length / 16;
            decimal[] data = new decimal[len];
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            for (long i = 0; i < len; i++)
            {
                data[i] = br.ReadDecimal();
            }

            mShowingFrameFrom = 0;
            mShowingFrameTo = (int)len - 1;

            fs.Close();
            if (br != null)
                br.Close();

            return data;
        }

        public void SetGraph(decimal[] data, int from, int to)
        {
            if (mGlobalyCalculated)
            {
                amChart.Series.Clear();
                amChart.Series.Add("Inten/Time");
                amChart.Series["Inten/Time"].ChartType = SeriesChartType.Line;
                amChart.Series["Inten/Time"].Color = Color.FromArgb(21, 0, 255);

                amChart.ChartAreas[0].AxisY.Maximum = 0.7;
                amChart.ChartAreas[0].AxisY.Minimum = 0.0;
                amTBGrpYMax.Text = (amChart.ChartAreas[0].AxisY.Maximum).ToString("0.000");

                if (from >= 0 && to < data.Length)
                {
                    for (int i = from; i <= to; i++)
                    {
                        amChart.Series["Inten/Time"].Points.AddXY(i, data[i]);
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Out of range", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private Decimal getRoundedKey(Decimal val)
        {
            return Math.Round(val, 5);
        }

        private Decimal getMax(List<Decimal> vals)
        {
            Decimal retval = 0;
            for (int i = 0; i < vals.Count; i++)
            {
                if (vals[i] > retval)
                {
                    retval = vals[i];
                }
            }

            return retval;
        }

        public void SetHistogram(List<StNormalizedIntensity> intensities)
        {
            if (mGlobalyCalculated)
            {
                amHistogram.Series.Clear();
                amHistogram.Series.Add("Dure/Inten");
                amHistogram.Series["Dure/Inten"].ChartType = SeriesChartType.Point;
                amHistogram.Series["Dure/Inten"].Color = Color.FromArgb(255, 0, 0);
                //amHistogram.Series["Dure/Inten"].MarkerStyle = MarkerStyle.Cross;
                amHistogram.Series["Dure/Inten"].MarkerSize = 2;
                
                amHistogram.ChartAreas[0].AxisX.Maximum = 0.16;
                amHistogram.ChartAreas[0].AxisX.Minimum = 0.0;
                amHistogram.ChartAreas[0].AxisY.Maximum = 250;
                amHistogram.ChartAreas[0].AxisY.Minimum = 0;
                amTBHisXMax.Text = (amHistogram.ChartAreas[0].AxisX.Maximum).ToString("0.000");
                amTBHisYMax.Text = (amHistogram.ChartAreas[0].AxisY.Maximum).ToString("0.000");

                //statistics
                //Dictionary<Decimal, int> dictOfFreq = new Dictionary<decimal, int>();
                for (int j = 0; j < intensities.Count; j++)
                {
                    //if (dictOfFreq.ContainsKey(getRoundedKey(intensities[j])))
                    //{
                    //    dictOfFreq[getRoundedKey(intensities[j])]++;
                    //}
                    //else
                    //{
                    //    dictOfFreq.Add(getRoundedKey(intensities[j]), 1);
                    //}

                    amHistogram.Series["Dure/Inten"].Points.AddXY(
                        intensities[j].Intensity, intensities[j].Duration);
                }

                //List<Decimal> keys = new List<Decimal>(dictOfFreq.Keys);

                //add bars
                //for (int i = 0; i < dictOfFreq.Count; i++)
                //{
                //    amHistogram.Series["frequency"].Points.AddXY(keys[i], dictOfFreq[keys[i]]);
                //}
            }
        }

        private void amBtnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ddf文件|*.ddf";
            ofd.Title = "加载数据";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mGlobalyCalculated = true;
                dataAnalyze(ofd.FileName);
                UpdateAnalyzeInfo();
            }
            //load
        }

        public void UpdateAnalyzeInfo()
        {
            String info = "";
            String info2 = "";
            info += "FocusRg:" + mShowingFrameFrom + "-" + mShowingFrameTo + "; ";
            info += "FrameCt:" + mGlobalData.Length + "; ";
            info += "Noise:" + (mNoiseSampleMean).ToString("00.0000") + "; ";
            info2 += "Immo Dure:" + mPostResult.ImmobilityDureation + "; ";
            info2 += "M Count:" + mPostResult.MovementCount + "; ";
            info2 += "M Mean:" + mPostResult.MovementIntentsityMean.ToString("0.000000") + "; ";
            info2 += "M SD:" + mPostResult.MovementIntensitySD.ToString("0.000000") + "; ";

            amLabelAnalzeInfo.Text = info;
            amLabelAnalyzeInfo2.Text = info2;
        }

        private void amBtnClip_Click(object sender, EventArgs e)
        {
            ClipView cv = new ClipView(this);
            cv.ShowDialog();
        }

        private void amBtnNoiseSample_Click(object sender, EventArgs e)
        {
            if (mGlobalyCalculated)
            {
                for (int i = mShowingFrameFrom; i <= mShowingFrameTo; i++)
                {
                    mNoiseSampleMean += mGlobalData[i];
                }

                mNoiseSampleMean /= (mShowingFrameTo - mShowingFrameFrom + 1);

                UpdateAnalyzeInfo();
            }
        }

        private void amBtnClearSample_Click(object sender, EventArgs e)
        {
            mNoiseSampleMean = 0;
            UpdateAnalyzeInfo();
        }

        private void amBtnRefresh_Click(object sender, EventArgs e)
        {
            double doubleBuf = 0;

            if (Double.TryParse(amTBHisXMax.Text, out doubleBuf))
            {
                amHistogram.ChartAreas[0].AxisX.Maximum = doubleBuf;
            }

            if (Double.TryParse(amTBHisYMax.Text, out doubleBuf))
            {
                amHistogram.ChartAreas[0].AxisY.Maximum = doubleBuf;
            }

            if (Double.TryParse(amTBGrpYMax.Text, out doubleBuf))
            {
                amChart.ChartAreas[0].AxisY.Maximum = doubleBuf;
            }
        }

        private void amBtnScaleNormal_Click(object sender, EventArgs e)
        {
            amHistogram.ChartAreas[0].AxisX.Maximum = 0.16;
            amHistogram.ChartAreas[0].AxisY.Maximum = 250;
            amChart.ChartAreas[0].AxisY.Maximum = 0.7;

            amTBHisXMax.Text = (amHistogram.ChartAreas[0].AxisX.Maximum).ToString("0.000");
            amTBHisYMax.Text = (amHistogram.ChartAreas[0].AxisY.Maximum).ToString("0.000");
            amTBGrpYMax.Text = (amChart.ChartAreas[0].AxisY.Maximum).ToString("0.000");
        }
    }
}
