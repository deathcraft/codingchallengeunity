using UnityEngine;

namespace CodingChallenge
{
    public static class VectorUtil
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
    }
}