using System;
using System.Collections.Generic;
using System.Text;
using sm = System.Math;

namespace MMRO2.Utils
{
    static class Math
    {
        public static double DegreesToRadians(double degrees)
        {
            return degrees * sm.PI / 180;
        }

        public static double RadiansToDegrees(double radians)
        {
            return radians * 180 / sm.PI;
        }
    }
}
