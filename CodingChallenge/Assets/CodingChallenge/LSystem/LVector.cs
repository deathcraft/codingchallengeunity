using UnityEngine;

namespace CodingChallenge.LSystem
{
    public struct LVector
    {
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
        public float Angle { get; set; }

        public override string ToString()
        {
            return "StartPosition: " + StartPosition + " EndPosition: " + EndPosition + " Angle: " + Angle;
        }
    }
}