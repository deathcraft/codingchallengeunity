using System.Collections;
using System.Collections.Generic;
using CodingChallenge.MengerSponge;
using UnityEngine;

namespace CodingChallenge.MengerSponge
{
    public class MengerSponge : MonoBehaviour
    {
        [SerializeField]
        private GameObject boxPrefab;

        [SerializeField]
        private float boxRadius;
        
        [SerializeField]
        private float offset;

        private MengerBox box;

        private List<MengerBox> mengerBoxes = new List<MengerBox>();

        // Use this for initialization
        void Start()
        {
            CreateBox();
        }

        public void Generate()
        {
            var newList = new List<MengerBox>();

            foreach (var mengerBox in mengerBoxes)
            {
                var generatedBoxes = mengerBox.Generate();
                newList.AddRange(generatedBoxes);
            }

            mengerBoxes = newList;
        }

        private void CreateBox()
        {
            box = Instantiate(boxPrefab, transform).GetComponent<MengerBox>();
            box.SetRadius(boxRadius, offset);
            mengerBoxes.Add(box);
        }
    }
}