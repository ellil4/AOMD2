using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows.Controls;

namespace AOMD2Collector
{
    public class TiltCtrl
    {
        Button mBtn;
        TextBox mTextBox;
        KinectSensor mSensor;

        public TiltCtrl(Button button, TextBox textBox)
        {
            mBtn = button;
            mTextBox = textBox;
            mBtn.Click += new System.Windows.RoutedEventHandler(mBtn_Click);
        }

        public void SetSensor(KinectSensor sensor)
        {
            mSensor = sensor;
            mTextBox.Text = sensor.ElevationAngle.ToString();
        }

        void mBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SetAngle();
        }

        public void SetAngle()
        {
            int angle = Int32.MinValue;
            try
            {
                angle = Int32.Parse(mTextBox.Text);
            }
            catch(FormatException)
            {
                mTextBox.Text = mSensor.ElevationAngle.ToString();
            }

            if (angle != Int32.MinValue)
            {
                if (mSensor != null && mSensor.IsRunning)
                {
                    try
                    {
                        mSensor.ElevationAngle = angle;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
 
                    }

                    mTextBox.Text = mSensor.ElevationAngle.ToString();
                }
            }
        }

    }
}
