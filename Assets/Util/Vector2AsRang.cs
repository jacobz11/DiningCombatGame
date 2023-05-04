using System;
using UnityEngine;

namespace Assets.Util
{
    internal class Vector2AsRang
    {
        public static float Max(Vector2 v) => v.x;
        public static float Min(Vector2 v) => v.y;

        public static Vector2 PositiveConstruction(Vector2 v)=> PositiveConstruction(v.x, v.y);

        public static Vector2 PositiveConstruction(float x, float y)
        {
            float minVal = Math.Min(0, Math.Max(y, x));
            float maxVal = Math.Max(Math.Abs(y), Math.Abs(x));

            return new Vector2(maxVal, minVal);
        }

        internal static float Clamp(float i_Magnitude, Vector2 i_RangeDamage)
        {
            return Math.Clamp(i_Magnitude, Min(i_RangeDamage), Max(i_RangeDamage));
        }

        internal static float Random(Vector2 v)
        {
            return UnityEngine.Random.Range(Min(v), Max(v));
        }
    }
}
