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
        private Text textOutput;

        private Dictionary<string, string> rules;

        private string sentence;

        void Awake()
        {
            rules = new Dictionary<string, string> {{"F", "FF+[+F-F-F]-[-F+F+F]"}};
            sentence = axiom;
        }

        //Called from button
        public void Generate()
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
            
            textOutput.text += "\n" + sentence;
        }
        
        

    }
}