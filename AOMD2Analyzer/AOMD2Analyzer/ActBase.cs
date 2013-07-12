using System;
using System.IO;

namespace AOMD2Analyzer
{
    public class ActBase
    {
        //FEATURE: period of data of 20 joints
        //TRACK: each joint`s data

        //count in bytes
        public const int JOINT_LEN = 12;//3 joints * 4 bytes
        public const int FEATURE_LEN = 240;//20 joints * 12 bytes
        public const int HEADER_LEN = 8;//a long
        public const int STAMP_LEN = 8;//long

        public const int JOINTS_COUNT = 20;//20 joints
        public const int JOINT_DATA_COUNT = 3;//x, y, z
        public const int FEATURE_DATA_COUNT = 
                            JOINTS_COUNT * JOINT_DATA_COUNT;

        protected FileStream mFileStream;
        protected bool mOn = false;

        public ActBase()
        {
        }
    }
}
