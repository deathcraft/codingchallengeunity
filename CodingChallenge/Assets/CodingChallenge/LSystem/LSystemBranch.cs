using UnityEngine;

namespace CodingChallenge.LSystem
{
    public class LSystemBranch : MonoBehaviour
    {
        [SerializeField] 
        private AbstractLine line;

        private Vector3 LineEnd
        {
            set { line.LineEnd = value; }
        }

        private Vector3 LineStart
        {
            set { line.LineStart = value; }
        }

        public Vector3 InitialStart { get; set; }
        public Vector3 InitialEnd { get; set; }

        public void DrawLine(Vector3 startPostion, Vector3 endPosition)
        {
            line.LineStart = startPostion;
            line.LineEnd = endPosition;

            InitialStart = startPostion;
            InitialEnd = endPosition;
        }

        public void Displace(Vector3 startDisplacement, Vector3 endDisplacement)
        {
            if (startDisplacement != Vector3.zero)
            {
                LineStart = InitialStart + startDisplacement;
            }

            if (endDisplacement != Vector3.zero)
            {
                LineEnd = InitialEnd + endDisplacement;
            }
        }
    }
}