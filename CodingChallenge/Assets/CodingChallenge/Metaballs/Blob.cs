using UnityEngine;

namespace CodingChallenge.Metaballs
{
    public class Blob : MonoBehaviour
    {
        public Vector3 vel;
        public bool randomVel;
        public float velCoeff;

        void Start()
        {
            if (randomVel)
            {
                vel = Random.insideUnitSphere * velCoeff;
                vel.z = 0;
            }
            
        }
        
        void Update()
        {
            var transformPosition = transform.position;
            transformPosition += vel;

            transform.position = transformPosition;
            
            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transformPosition);

            if (viewportPoint.x < 0)
            {
                vel.x = -vel.x;
            }
            else if (viewportPoint.x > 1)
            {
                vel.x = -vel.x;
            }
            else if (viewportPoint.y < 0)
            {
                vel.y = -vel.y;
            }
            else if (viewportPoint.y > 1)
            {
                vel.y = -vel.y;
            }

            
            
            
        }
       
    }
}