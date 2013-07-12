using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Windows.Forms;
using System.IO;
using System.Windows.Threading;

namespace AOMD2Collector
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private Framer mFramer;
        private ActOutputer mAct;
        private TiltCtrl mTiltControl;
        private System.Timers.Timer mTimer;
        private DateTime mStartTime;
        public bool mKinectReady = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void setUIReady()
        {
            amBtnStop.IsEnabled = true;
            amBtnStart.IsEnabled = true;
            amTiltBtn.IsEnabled = true;
            mKinectReady = true;
        }

        private void setUIInvalid()
        {
            amBtnStop.IsEnabled = false;
            amBtnStart.IsEnabled = false;
            amTiltBtn.IsEnabled = false;
            mKinectReady = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopKinect(amChooser.Kinect);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mFramer = new Framer(amImage);
            mAct = new ActOutputer();
            setUIInvalid();

            amChooser.KinectSensorChanged += 
                new DependencyPropertyChangedEventHandler(amChooser_KinectSensorChanged);

            mTiltControl = new TiltCtrl(amTiltBtn, amTiltBox);

            amTimeDisplay.TextAlignment = TextAlignment.Right;
        }

        void amChooser_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            KinectSensor oldSensor = (KinectSensor)e.OldValue;
            StopKinect(oldSensor);

            KinectSensor newSensor = (KinectSensor)e.NewValue;

            if (KinectSensor.KinectSensors.Count > 0)
            {
                newSensor = KinectSensor.KinectSensors[0];

                if (newSensor.Status == KinectStatus.Connected)
                {
                    newSensor.ColorStream.Enable();
                    newSensor.DepthStream.Enable();
                    newSensor.SkeletonStream.Enable();
                    newSensor.AllFramesReady +=
                        new EventHandler<AllFramesReadyEventArgs>(mSensor_AllFramesReady);

                    try
                    {
                        newSensor.Start();

                        setUIReady();
                        mTiltControl.SetSensor(amChooser.Kinect);
                    }
                    catch (System.IO.IOException)
                    {
                        amChooser.AppConflictOccurred();
                    }
                }
                else
                {
                    setUIInvalid();
                }
            }
            else
            {
                setUIInvalid();
            }
        }

        const int _skeletonCount = 6;
        Skeleton[] mAllSkeletons = new Skeleton[_skeletonCount];

        void mSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            //mFramer.ColorImage2Screen(e);
            mFramer.DepthImage2Screen(e);

            Skeleton skeleton;
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if(skeletonFrame == null)
                    return;

                skeletonFrame.CopySkeletonDataTo(mAllSkeletons);

                skeleton = (from s in mAllSkeletons
                            where s.TrackingState == SkeletonTrackingState.Tracked
                            select s).FirstOrDefault();

                if (skeleton == null)
                    return;

                mAct.RecordFrameData(skeleton, skeletonFrame);
            }
        }

        void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if(sensor.AudioSource != null)
                    sensor.AudioSource.Stop();
                sensor.Stop();
            }
        }

        private void startTimeDisplay()
        {
            mTimer = new System.Timers.Timer();
            mTimer.Interval = 1000;
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
            mTimer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

            mStartTime = DateTime.Now;
        }

        private void stopTimeDisplay()
        {
            mTimer.Enabled = false;
            amTimeDisplay.Text = "--:--:--";
        }

        public delegate void TimerThreadDelegate();

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new TimerThreadDelegate(SetTime));
        }

        public void SetTime()
        {
            TimeSpan span = DateTime.Now - mStartTime;
            amTimeDisplay.Text = span.Hours.ToString("00") + ":" + 
                span.Minutes.ToString("00") + ":" + 
                span.Seconds.ToString("00");
        }

        private void amBtnStart_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save as";
            dlg.Filter = "(*." + QAIF.QAIF.INDEX_EXT + ")|*." + QAIF.QAIF.INDEX_EXT;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {   
                mFramer.StartWriter(dlg.FileName);

                mAct.InitFile(dlg.FileName.Remove(dlg.FileName.Length - 3) + ".act", 0);
                mAct.Start();

                startTimeDisplay();
            }
        }

        private void amBtnStop_Click(object sender, RoutedEventArgs e)
        {
            mFramer.FinishWriter();

            mAct.End();

            stopTimeDisplay();
        }
    }
}
