using UnityEngine;

namespace CodingChallenge
{
    public class RotationAnimation : MonoBehaviour
    {
        [SerializeField] 
        private Vector3 rotationSpeed;
        
        [SerializeField]
        private GameObject target;

        void Update () {
            target.transform.Rotate(Vector3.up, rotationSpeed.y);
            target.transform.Rotate(Vector3.right, rotationSpeed.x);
            target.transform.Rotate(Vector3.forward, rotationSpeed.z);
        }
        
    }
}