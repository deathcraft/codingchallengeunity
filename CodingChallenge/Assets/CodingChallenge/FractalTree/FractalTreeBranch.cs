using UnityEngine;

namespace CodingChallenge.FractalTree
{
    public class FractalTreeBranch : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        private Vector3 branchStart;
        private Vector3 branchEnd;

        public void DrawLine(Vector3 branchStart, Vector3 branchEnd)
        {
            this.branchStart = branchStart;
            this.branchEnd = branchEnd;
            
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, branchStart);
            lineRenderer.SetPosition(1, branchEnd);
        }

        public void Branch()
        {
            Vector3 direction = branchEnd - branchStart;
            Vector3 directionRight = RotateVector(direction, Mathf.PI / 8);
            Vector3 directionLeft = RotateVector(direction, -Mathf.PI / 8);
            
            FractalTreeBranch right = InstantiateBranch(branchEnd, directionRight + branchEnd);
            FractalTreeBranch left = InstantiateBranch(branchEnd, directionLeft + branchEnd);
            

        }

        private Vector3 RotateVector(Vector3 vector, float angle)
        {
            Vector3 rotatedVector = vector;
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);
            
            //WTF
            rotatedVector.x = vector.x * cos - vector.y * sin;
            rotatedVector.y = vector.y * sin + vector.x * cos;
            return rotatedVector;
        }
        
        private FractalTreeBranch InstantiateBranch(Vector3 start, Vector3 end)
        {
            GameObject branch = Instantiate(gameObject, transform);
            var fractalTreeBranch = branch.GetComponent<FractalTreeBranch>();
            fractalTreeBranch.DrawLine(start, end);
            return fractalTreeBranch;
        }
        
    }
}