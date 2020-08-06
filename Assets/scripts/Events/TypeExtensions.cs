using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
    static class TypeExtensions
    {
        public static bool Compare(this float value, float other, float epsilon = 0.0001f)
        {
            float difference = Math.Abs(value * epsilon);

            return Math.Abs(value - other) <= difference;
        }

        public static bool ContainsDelegate(this Delegate[] delegates, Delegate @delegate)
        {
            if (delegates.Contains(@delegate))
            {
                return true;
            }

            return false;
        }
    }
}
