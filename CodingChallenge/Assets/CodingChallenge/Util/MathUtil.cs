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
    }
}