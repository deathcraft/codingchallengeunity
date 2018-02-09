using System.Collections.Generic;
using UnityEngine;

namespace CodingChallenge.MengerSponge
{
    public class MengerBox : MonoBehaviour
    {
        private float radius;
        private float offset;
        
        public List<MengerBox> Generate()
        {
            var mengerBoxes = new List<MengerBox>();

            float radCoeff = radius / 3f;
            transform.localScale = new Vector3(radCoeff, radCoeff, radCoeff);

            Quaternion rotation = transform.parent.rotation;
            transform.parent.rotation = Quaternion.identity;
            
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    for (int z = -1; z < 2; z++)
                    {

                        if (Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z) <= 1)
                        {
                            continue;
                        }
                        
                        GameObject child = Instantiate(gameObject, transform.parent);

                        var childX = transform.position.x + x * radCoeff;
                        var childY = transform.position.y + y * radCoeff;
                        var childZ = transform.position.z + z * radCoeff;

                        var localPosition = new Vector3(childX , childY, childZ);

                        child.transform.position = localPosition;
                        
                        child.transform.SetParent(transform.parent, true);
                        var mengerBox = child.GetComponent<MengerBox>();
                        mengerBox.SetRadius(radCoeff, offset);
                        
                        mengerBoxes.Add(mengerBox);
                    }
                }
            }

            transform.parent.rotation = rotation;
                        
            Destroy(gameObject);

            return mengerBoxes;
        }

        public void SetRadius(float radius, float offset)
        {
            this.radius = radius;
            this.offset = offset;
            transform.localScale = new Vector3(radius - offset, radius - offset,radius - offset);
        }
    }
}