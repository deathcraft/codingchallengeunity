using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodingChallenge.LSystem
{
    public class LSystem : MonoBehaviour
    {

        [SerializeField] 
        private string axiom = "F";
        
        [SerializeField] 
        private LRule[] ruleArray;
        
        [SerializeField] 
        private Text textOutput;
        
        [SerializeField] 
        private GameObject lSystemBranch;
        
        [SerializeField] 
        private float lenghtCoeff = 1;
        
        [SerializeField] 
        private float lineLength = 1;
        
        [SerializeField] 
        private float angleDegrees = 25;
        
        [SerializeField] 
        private bool animate;
        
        [SerializeField] 
        private float animationCoeff = 0.01f;

        private Dictionary<string, string> rules;

        private string sentence;

        private LVector currentPosition;

        private List<LSystemBranch> branches = new List<LSystemBranch>();
        
        
        private Stack<LVector> stack = new Stack<LVector>();

        void Awake()
        {
            rules = new Dictionary<string, string>();

            foreach (LRule rule in ruleArray)
            {
                rules.Add(rule.predecessor, rule.successor);
            }
            
            sentence = axiom;
            currentPosition.StartPosition = transform.position;
            currentPosition.EndPosition = new Vector3(0, transform.position.y + lineLength, 0);
            currentPosition.Angle = 0;
            
            DrawLSystem();
        }

        //Called from button
        public void Generate()
        {
            DeleteOldLines();
            GenerateSentence();
            DrawLSystem();
            textOutput.text += "\n" + sentence;
        }

        private void GenerateSentence()
        {
            string nextSentence = "";
            for (int i = 0; i < sentence.Length; i++)
            {
                string curChar = sentence[i].ToString();

                if (rules.ContainsKey(curChar))
                {
                    string val = rules[curChar];
                    nextSentence += val;
                }
                else
                {
                    nextSentence += curChar;
                }
            }

            sentence = nextSentence;
        }

        private void DrawLSystem()
        {
            for (int i = 0; i < sentence.Length; i++)
            {
                string curChar = sentence[i].ToString();
                DrawLine(curChar);
            }
            
            branches.Sort(new LBranchComparer());
        }

        private void DrawLine(string curChar)
        {
            if (curChar == "+")
            {
                RotateDirectionRight();
            } else if (curChar == "-")
            {
                RotateDirectionLeft();
            } else if (curChar == "[")
            {
                SavePosition();
            } else if (curChar == "]")
            {
                RestorePosition();
            }
            else
            {
                CreateLine();
            }
    }

        private void RestorePosition()
        {
            currentPosition = stack.Pop();
        }

        private void SavePosition()
        {
            stack.Push(currentPosition);
        }

        private void RotateDirectionLeft()
        {
            currentPosition.Angle += -angleDegrees * Mathf.Deg2Rad;
        }

        private void RotateDirectionRight()
        {
            currentPosition.Angle += angleDegrees * Mathf.Deg2Rad;
        }

        private void CreateLine()
        {
            Vector3 rotatedEnd = MathUtil.RotateVector(Vector3.up, currentPosition.Angle) * lenghtCoeff;
            Vector3 newEndPosition = rotatedEnd + currentPosition.EndPosition;

            DrawLine(currentPosition.EndPosition, newEndPosition);

            currentPosition.StartPosition = currentPosition.EndPosition;
            currentPosition.EndPosition = newEndPosition;
        }

        private void DrawLine(Vector3 startPosition, Vector3 endPosition)
        {
            GameObject branchObj = Instantiate(lSystemBranch, transform);
            var branch = branchObj.GetComponent<LSystemBranch>();
            branch.DrawLine(startPosition, endPosition);
            branches.Add(branch);
        }
        
        private void DeleteOldLines()
        {
            branches.Clear();
            
            currentPosition.StartPosition = transform.position;
            currentPosition.EndPosition = new Vector3(0, transform.position.y + lineLength, 0);
            currentPosition.Angle = 0;
            
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        void Update()
        {
            if (!animate)
            {
                return;
            }

            for (int i = 0; i < branches.Count; i++)
            {
                float val = Mathf.Sin(Time.time) * i * animationCoeff;
                var displacement = new Vector3(val, 0, 0);
                branches[i].Displace(Vector3.zero, displacement);

                if (i < branches.Count - 1)
                {
                    branches[i+1].Displace(displacement, Vector3.zero);
                }
            }
            
        }
    }

    internal class LBranchComparer : IComparer<LSystemBranch>
    {
        public int Compare(LSystemBranch x, LSystemBranch y)
        {
            return x.InitialStart.y.CompareTo(y.InitialStart.y);
        }
    }
}