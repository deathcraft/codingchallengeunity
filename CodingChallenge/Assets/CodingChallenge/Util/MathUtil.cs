using System.Xml.Schema;
using UnityEngine;

namespace CodingChallenge
{
    public static class MathUtil
    {
        public  static Vector3 RotateVector(Vector3 vector, float angle)
        {
            Vector3 rotatedVector = vector;
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);
            
            rotatedVector.x = vector.x * cos - vector.y * sin;
            rotatedVector.y = vector.x * sin + vector.y * cos;
            return rotatedVector;
        }

        public static float Map(float val, float min1, float max1, float min2, float max2)
        {
            return min2 + (val - min1) * (max2 - min2) / (max1 - min1);
        }

        public static Color ColorLerp(Color c1, Color c2, float val)
        {
            float r = Mathf.Lerp(c1.r, c2.r, val);
            float g = Mathf.Lerp(c1.g, c2.g, val);
            float b = Mathf.Lerp(c1.b, c2.b, val);
            return new Color(r,g,b);
        }
    }
}