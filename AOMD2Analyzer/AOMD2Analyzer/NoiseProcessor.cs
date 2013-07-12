using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AOMD2Analyzer
{
    public class NPBound
    {
        public int xStart;
        public int xEnd;

        public NPBound()
        { }

        public NPBound(int start, int end)
        {
            xStart = start;
            xEnd = end;
        }

        public bool Inside(int x)
        {
            if (x >= xStart && x <= xEnd)
                return true;
            else
                return false;
        }
    }

    public class NPPoint
    {
        public int x = 0;
        public int y = 0;

        public NPPoint()
        { }

        public NPPoint(int xPos, int yPos)
        {
            x = xPos;
            y = yPos;
        }
    }

    public class NoiseProcessor
    {
        public Dictionary<int, NPPoint> mMapCurArea;
        public List<NPPoint> mSeeds;
        public Kernel mKernel;

        public NoiseProcessor(Kernel kernel)
        {
            mMapCurArea = new Dictionary<int, NPPoint>();
            
            mSeeds = new List<NPPoint>();

            mKernel = kernel;
        }

        //public static bool HasUp(int x, int y, ref Bitmap bmp, Color blockColor)
        //{
        //    bool retval = false;

        //    if (y != 0)
        //        if (bmp.GetPixel(x, y - 1).ToArgb() == blockColor.ToArgb())
        //            retval = true;

        //    return retval;
        //}

        //public static bool HasDown(int x, int y, ref Bitmap bmp, Color blockColor)
        //{
        //    bool retval = false;

        //    if (y != Kernel.PIXLINECOUNT - 1)
        //        if (bmp.GetPixel(x, y + 1).ToArgb() == blockColor.ToArgb())
        //            retval = true;

        //    return retval;
        //}

        public bool HasRight(int x, int y, Color blockColor)
        {
            bool retval = false;
            if (x != Kernel.PIXLINELEN - 1)
            {
                if (mKernel.cppGetPxColor(x + 1, y).ToArgb() == blockColor.ToArgb())
                    retval = true;
            }

            return retval;
        }

        public bool HasLeft(int x, int y, Color blockColor)
        {
            bool retval = false;

            if (x != 0)
            {
                if (mKernel.cppGetPxColor(x - 1, y).ToArgb() == blockColor.ToArgb())
                    retval = true;
            }

            return retval;
        }

        public static int GetKey(int x, int y)
        {
            return x + y * (int)Kernel.PIXLINELEN;
        }

        private void scanLineSeeds(int y, NPBound bound, Color blockColor)
        {
            if (y >= 0 && y < Kernel.PIXLINECOUNT)
            {
                int xPos = bound.xStart;
                bool isIn = false;

                while (xPos <= bound.xEnd)
                {
                    if (mKernel.cppGetPxColor(xPos, y).ToArgb() == blockColor.ToArgb())
                    {
                        if (!isIn)
                        {
                            if (!mMapCurArea.ContainsKey(GetKey(xPos, y)))
                            {
                                mSeeds.Add(new NPPoint(xPos, y));
                            }

                            isIn = true;
                        }
                    }
                    else
                    {
                        if (isIn)
                            isIn = false;
                    }

                    xPos++;
                }
            }
        }

        private NPBound fillLineSpan(int x, int y, Color blockColor)
        {
            int lBound = x, rBound = x;

            int detectX = x;

            //left
            while (true)
            {
                int key = GetKey(detectX, y);

                if(!mMapCurArea.ContainsKey(key))
                    mMapCurArea.Add(key, new NPPoint(detectX, y));

                if (!HasLeft(detectX, y, blockColor))
                    break;
                
                detectX--;
                lBound--;
            }

            detectX = x;

            //right
            while (true)
            {
                int key = GetKey(detectX, y);

                if (!mMapCurArea.ContainsKey(key))
                    mMapCurArea.Add(key, new NPPoint(detectX, y));

                if (!HasRight(detectX, y, blockColor))
                    break;

                detectX++;
                rBound++;
            }

            return new NPBound(lBound, rBound);
        }

        public void DoBlockStatistics(int x, int y, Color blockColor)
        {
            mSeeds.Add(new NPPoint(x, y));
            int count = mSeeds.Count;

            //order: FILO
            while (count != 0)
            {
                NPBound bound = fillLineSpan(mSeeds[count - 1].x, mSeeds[count - 1].y, blockColor);
                scanLineSeeds(mSeeds[count - 1].y - 1, bound, blockColor);
                scanLineSeeds(mSeeds[count - 1].y + 1, bound, blockColor);                

                mSeeds.RemoveAt(count - 1);
                count = mSeeds.Count;
            }
        }

        public void ClearBlockInfo()
        {
            mMapCurArea.Clear();
        }

        public static bool ValueCompare(
            short framePre, short frameThis, short framePost, short tolerance)
        {
            bool retval = false;
            if (Math.Abs(frameThis - framePre) > tolerance &&
                Math.Abs(frameThis - framePost) > tolerance)//3D
            {
                retval = true;
            }
            else if (framePre == -1)//2D
            {
                retval = true;
            }
            
            return retval;
        }
    }
}
