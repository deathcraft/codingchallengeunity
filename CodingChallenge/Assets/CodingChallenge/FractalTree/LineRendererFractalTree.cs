using System.Collections.Generic;
using UnityEngine;

namespace CodingChallenge.FractalTree
{
    public class LineRendererFractalTree : MonoBehaviour
    {
        [SerializeField]
        private GameObject branchPrefab;
        
        [SerializeField]
        private int treeDepth;
        
        [SerializeField]
        private float initialBranchLength;            
        
        [SerializeField]
        private float angle;

        private List<FractalTreeBranch> tree = new List<FractalTreeBranch>();
        
        void Awake()
        {
            var branch = InstantiateBranch(Vector3.zero, new Vector3(0, initialBranchLength, 0));
            tree.Add(branch);
            
            branch.Branch();
        }

        private FractalTreeBranch InstantiateBranch(Vector3 branchStart, Vector3 branchEnd)
        {
            GameObject branch = Instantiate(branchPrefab, transform);
            var fractalTreeBranch = branch.GetComponent<FractalTreeBranch>();
            fractalTreeBranch.DrawLine(branchStart, branchEnd);
            return fractalTreeBranch;
        }

        

        void Update()
        {
        
        }
    }
}