using System;
using System.IO;
using Microsoft.Kinect;
using Coding4Fun.Kinect.WinForm;

namespace AOMD2Analyzer
{
    public class ActInputer : ActBase
    {
        private BinaryReader mBR;
        private string mPath;

        //constructor
        public ActInputer()
        {
            
        }

        ~ActInputer()
        {
            End();
        }

        //class op
        public void OpenFile(string path)
        {
            if (mOn)
                End();
            
            if (File.Exists(path))
            {
                mFileStream = new FileStream(path, FileMode.Open);
                mPath = path;
                mBR = new BinaryReader(mFileStream);
                mOn = true;
            }
            else
            {
                throw new Exception("file does not exist");
            }
        }

        public void End()
        {
            if(mBR != null)
                mBR.Close();
            if(mFileStream != null)
                mFileStream.Close();
            mOn = false;
        }

        public bool IsOn()
        {
            return mOn;
        }

        //ID
        public long ReadHeader()
        {
            mBR.BaseStream.Seek(0, SeekOrigin.Begin);
            return mBR.ReadInt64();
        }

        //about stamp
        public long ReadStamp(long _th)
        {
            mBR.BaseStream.Seek(
                HEADER_LEN + _th * (STAMP_LEN + FEATURE_LEN), SeekOrigin.Begin);
            return mBR.ReadInt64();
        }

        //about tracks
        public FOT[] ReadTrack(JointType _type, 
            long _beg_idx, long _count)
        {
            FOT[] retval = new FOT[_count];

            long offset = TrackBaseOffset(_type) + (FEATURE_LEN + STAMP_LEN) * _beg_idx;
            
            for (int looper = 0; looper < _count; looper++)
            {
                retval[looper].stamp = ReadStamp(looper);

                mBR.BaseStream.Seek(offset, SeekOrigin.Begin);
                retval[looper].x = mBR.ReadSingle();
                retval[looper].y = mBR.ReadSingle();
                retval[looper].z = mBR.ReadSingle();

                offset += FEATURE_LEN + STAMP_LEN;
            }

            return retval;
        }

        private static long TrackBaseOffset(JointType _type)
        {
            return HEADER_LEN + JOINT_LEN * (long)_type;
        }

        //about features
        private static long FeatureOffset(long _featureAt)
        {
            return _featureAt * (FEATURE_LEN + STAMP_LEN) + HEADER_LEN;
        }

        public float[] ReadFeature(long _featureIndex)
        {
            float[] copy2 = new float[FEATURE_DATA_COUNT];
            mBR.BaseStream.Seek(FeatureOffset(_featureIndex), SeekOrigin.Begin);
            for(int looper = 0; looper < FEATURE_DATA_COUNT; looper++)//60 loops
            {
                copy2[looper] = mBR.ReadSingle();
            }

            return copy2;
        }

        public long GetFeaturesCount()
        {
            long count = -1;

            if(mOn)
            {
                FileInfo fi = new FileInfo(mPath);
                long len = fi.Length;
                count = 
                    (len - HEADER_LEN) / (FEATURE_LEN + STAMP_LEN);

            }

            return count;
        }
 
    }
}
