using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamLibrary
{

    public static class MathLibrary
    {

        public static double my_pow(double num, int x)
        {

            double res = 1;
            while ((x >= 1))
            {
                if ((x & 1) >= 1)
                    res *= num;
                x >>= 1;
                num *= num;
            }

            return res;

        }

    }

}
