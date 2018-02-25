using UnityEngine;
using VolumetricLines;

namespace CodingChallenge.Terrain
{
    public class FractalSpaceBranch : MonoBehaviour
    {
        public FractalSpaceBranch parent;

        public VolumetricLineBehavior volumetricLine;

        public Vector3 direction;
        
        public Vector3 startPosition;

        public float sizeCoeff;
        public float colorCoeff;

        public Vector3 EndPosition
        {
            get { return volumetricLine.EndPos;  }
        }

        private int count;
        private Vector3 originalDirection;
        
        public void Increment()
        {
            count++;
        }

        public int CurrentCount()
        {
            return count;
        }

        public void Init()
        {
            originalDirection = direction;
            volumetricLine.StartPos = startPosition;
            volumetricLine.EndPos = parent != null ? parent.EndPosition + direction : startPosition + direction;

            if (parent != null)
            {
                volumetricLine.LineWidth = parent.volumetricLine.LineWidth * sizeCoeff;
                var volumetricLineLineColor = parent.volumetricLine.LineColor;
                volumetricLineLineColor.r -= colorCoeff;
                volumetricLineLineColor.b += colorCoeff;
                volumetricLine.LineColor = volumetricLineLineColor;
            }
        }

        public void ResetBranch()
        {
            count = 0;
            direction = originalDirection;
        }
    }
}