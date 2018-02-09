using UnityEngine;

namespace CodingChallenge
{
    public class RotationDrag : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed = 20f;
        
        [SerializeField]
        private GameObject target;

        void OnMouseDrag()
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;
            
            target.transform.RotateAround(Vector3.up, -rotX);
            target.transform.RotateAround(Vector3.right, rotY);
        }
        
    }
}