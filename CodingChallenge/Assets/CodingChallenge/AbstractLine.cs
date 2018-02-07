using UnityEngine;

namespace CodingChallenge
{
    public class AbstractLine : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer;
        
        private void Awake()
        {
            lineRenderer.positionCount = 2;
        }
        
        private Vector3 lineStart;
        private Vector3 lineEnd;
        
        public Vector3 LineStart
        {
            get { return lineStart; }
            set
            {
                lineStart = value; 
                lineRenderer.SetPosition(0, lineStart);
            }
        }

        public Vector3 LineEnd
        {
            get { return lineEnd; }
            set
            {
                lineEnd = value; 
                lineRenderer.SetPosition(1, lineEnd);
            }
        }
        
        
    }
}