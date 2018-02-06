using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodingChallenge.FractalTree
{
    public class FractalTree : MonoBehaviour
    {
        [SerializeField]
        private GameObject branchPrefab;
        
        [SerializeField]
        private float initialBranchLength;            
        
        [SerializeField]
        private float angle;
        
        [SerializeField]
        private float depth;
        
        [SerializeField]
        private float updateSpeed;
        
        private bool play;

        public GameObject BranchPrefab
        {
            get { return branchPrefab; }
            set { branchPrefab = value; }
        }

        public float InitialBranchLength
        {
            get { return initialBranchLength; }
        }

        public float Angle
        {
            get { return angle; }
        }

        private List<FractalTreeBranch> tree = new List<FractalTreeBranch>();
        
        void Awake()
        {
            Vector3 initialEnd = transform.position + new Vector3(0, initialBranchLength, 0);
            var branch = InstantiateBranch(transform.position, initialEnd);
            tree.Add(branch);
            
            branch.Branch(this, depth);
        }

        private FractalTreeBranch InstantiateBranch(Vector3 branchStart, Vector3 branchEnd)
        {
            GameObject branch = Instantiate(branchPrefab, transform);
            var fractalTreeBranch = branch.GetComponent<FractalTreeBranch>();
            fractalTreeBranch.DrawLine(branchStart, branchEnd);
            return fractalTreeBranch;
        }

        public void RedrawChildren()
        {
            angle = FindObjectOfType<Slider>().value;
            if (tree[0] != null)
            {
                tree[0].RedrawChildren();
            }
        }

        public void StartAnim()
        {
            play = !play;
        }
        
        void Update()
        {
            if (!play)
            {
                return;
            }
            
            var slider = FindObjectOfType<Slider>();
            slider.value += updateSpeed * Time.deltaTime;
            RedrawChildren();

            if (slider.value >= slider.maxValue)
            {
                slider.value = slider.minValue;
            }
        }


    }
}