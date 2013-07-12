using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Kinect;
using Coding4Fun.Kinect.Wpf;

namespace AOMD2Collector
{
    class ActOutputer : ActBase
    {
        private BinaryWriter mBW;
        public static String EXTEND_NAME = "act";

        //constructor
        public ActOutputer()
        { 
        
        }

        private void write_header(long id)
        {
            mBW.Write(id);
        }

        public void InitFile(string path, long id)
        {
            if(!File.Exists(path))
            {
                mFileStream = new FileStream(path, FileMode.CreateNew);
                mBW = new BinaryWriter(mFileStream);
                
                write_header(id);

                //mOn = true;
            }
            else
            {
                throw new Exception("file existes");
            }
        }

        public void Start()
        {
            mOn = true;
        }

        //record real time data stream
        public void RecordFrameData(Skeleton _skeleton, SkeletonFrame _frame)
        {
            if (mOn)
            {
                //time stamp
                //8 bytes

                mBW.Write(_frame.Timestamp);
                //mBW.Write(((long)1));//test

                for (JointType Tlooper = JointType.HipCenter;
                    Tlooper <= JointType.FootRight; Tlooper++)
                {
                    //one joint, (12 bytes = (3 * 4 bytes)) * 20 == 240bytes
                    mBW.Write(_skeleton.Joints[Tlooper].Position.X);
                    mBW.Write(_skeleton.Joints[Tlooper].Position.Y);
                    mBW.Write(_skeleton.Joints[Tlooper].Position.Z);
                }
            }
        }



        public void End()
        {
            if (mBW != null)
            {
                mBW.Flush();
                mBW.Close();
                mFileStream.Close();
                mOn = false;
                mBW = null;
            }
        }
    }
}
