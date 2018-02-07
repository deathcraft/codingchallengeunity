using UnityEngine;

namespace CodingChallenge.FractalTree
{
    public class FractalTreeBranch : MonoBehaviour
    {
        private static int count;
        
        [SerializeField]
        private AbstractLine abstractLine;

        private FractalTree root;

        private FractalTreeBranch right;
        private FractalTreeBranch left;

        public void DrawLine(Vector3 branchStart, Vector3 branchEnd)
        {
            abstractLine.LineStart = branchStart;
            abstractLine.LineEnd = branchEnd;
            RedrawChildren();
        }

        public void RedrawChildren()
        {
            if (right != null && left != null)
            {
                right.DrawLine(abstractLine.LineEnd, CalculateChildEndVector(false));
                left.DrawLine(abstractLine.LineEnd, CalculateChildEndVector(true));
            }
        }

        private Vector3 CalculateChildEndVector(bool leftTurn)
        {
            Vector3 direction = abstractLine.LineEnd - abstractLine.LineStart;
            int coeff = leftTurn ? -1 : 1;
            Vector3 rotatedDirection = VectorUtil.RotateVector(direction, coeff * root.Angle) * root.InitialBranchLength;
            return rotatedDirection + abstractLine.LineEnd;
        }

        public void Branch(FractalTree root, float depth)
        {
            this.root = root;

            if (depth > 0)
            {
                right = InstantiateBranch(abstractLine.LineEnd, CalculateChildEndVector(false));
                right.Branch(root, depth - 1);
                left = InstantiateBranch(abstractLine.LineEnd, CalculateChildEndVector(true));
                left.Branch(root, depth - 1);                
            }
        }

        private FractalTreeBranch InstantiateBranch(Vector3 start, Vector3 end)
        {
             return InstantiateBranch(start, end, transform);
        }

        private FractalTreeBranch InstantiateBranch(Vector3 start, Vector3 end, Transform parent)
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