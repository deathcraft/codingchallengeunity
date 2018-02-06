using UnityEngine;

namespace CodingChallenge.FractalTree
{
    public class FractalTreeBranch : MonoBehaviour
    {
        private static int count;
        
        [SerializeField]
        private LineRenderer lineRenderer;
        
        private FractalTree root;

        private Vector3 branchStart;
        private Vector3 branchEnd;

        private FractalTreeBranch right;
        private FractalTreeBranch left;

        public Vector3 BranchStart
        {
            get { return branchStart; }
            set
            {
                branchStart = value; 
                lineRenderer.SetPosition(0, branchStart);
            }
        }

        public Vector3 BranchEnd
        {
            get { return branchEnd; }
            set
            {
                branchEnd = value; 
                lineRenderer.SetPosition(1, branchEnd);
                RedrawChildren();
            }
        }

        private void Awake()
        {
            lineRenderer.positionCount = 2;
        }

        public void DrawLine(Vector3 branchStart, Vector3 branchEnd)
        {
            BranchStart = branchStart;
            BranchEnd = branchEnd;
        }

        public void RedrawChildren()
        {
            if (right != null && left != null)
            {
                right.BranchStart = BranchEnd;
                left.BranchStart = BranchEnd;
                right.BranchEnd = CalculateChildEndVector(false);
                left.BranchEnd = CalculateChildEndVector(true);
            }
        }

        private Vector3 CalculateChildEndVector(bool leftTurn)
        {
            Vector3 direction = BranchEnd - BranchStart;
            int coeff = leftTurn ? -1 : 1;
            Vector3 rotatedDirection = RotateVector(direction, coeff * root.Angle) * root.InitialBranchLength;
            return rotatedDirection + BranchEnd;
        }

        public void Branch(FractalTree root, float depth)
        {
            this.root = root;

            if (depth > 0)
            {
                right = InstantiateBranch(branchEnd, CalculateChildEndVector(false));
                right.Branch(root, depth - 1);
                left = InstantiateBranch(branchEnd, CalculateChildEndVector(true));
                left.Branch(root, depth - 1);                
            }
        }

        private Vector3 RotateVector(Vector3 vector, float angle)
        {
            Vector3 rotatedVector = vector;
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);
            
            rotatedVector.x = vector.x * cos - vector.y * sin;
            rotatedVector.y = vector.x * sin + vector.y * cos;
            return rotatedVector;
        }
        
        public FractalTreeBranch InstantiateBranch(Vector3 start, Vector3 end)
        {
             return InstantiateBranch(start, end, transform);
        }
        
        public FractalTreeBranch InstantiateBranch(Vector3 start, Vector3 end, Transform parent)
        {
            count++;
            GameObject branch = Instantiate(root.BranchPrefab, parent);
            branch.name = count.ToString();
            var fractalTreeBranch = branch.GetComponent<FractalTreeBranch>();
            fractalTreeBranch.DrawLine(start, end);
            return fractalTreeBranch;
        }
        
    }
}