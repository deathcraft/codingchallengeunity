using UnityEngine;
using VolumetricLines;

namespace CodingChallenge.Terrain
{
    public class FractalSpaceLeaf : MonoBehaviour
    {
        [SerializeField]
        private VolumetricLineBehavior volumetricLine;

        public bool Reached
        {
            get { return reached; }
            set { reached = value; }
        }

        private Vector2 startPos;
        private Vector2 endPos;
        private bool reached;

        public Vector3 LeafPosition
        {
            get { return volumetricLine.StartPos; }
            
        }

        private void Awake()
        {
            volumetricLine.LineColor = Color.blue;
        }

        void Start()
        {
            RandomPos();

            volumetricLine.LineColor = Random.ColorHSV();
        }
        
        public void RandomPos()
        {
            var start = Random.insideUnitSphere * 5;
            var end = start + Random.insideUnitSphere / 10;
            start.z = 0;
            
            end.z = 0;

            volumetricLine.StartPos = start;
            volumetricLine.EndPos = end;
        }

        public void MovePerlin(float speed, float coeff)
        {
            var volumetricLineStartPos = volumetricLine.StartPos;
            var volumetricLineEndPos = volumetricLine.EndPos;

            volumetricLineStartPos.x = Mathf.PerlinNoise(startPos.x, startPos.y) * coeff;
            volumetricLineStartPos.y = Mathf.PerlinNoise(startPos.y + startPos.x, startPos.x) * coeff;
            startPos.x += speed;
            startPos.y += speed;
            
            volumetricLineEndPos.x = Mathf.PerlinNoise(endPos.x, endPos.y) * coeff;
            volumetricLineEndPos.y = Mathf.PerlinNoise(endPos.y + endPos.x, endPos.x) * coeff;
            endPos.x += speed;
            endPos.y += speed;

            volumetricLine.StartPos = volumetricLineStartPos;
            volumetricLine.EndPos = volumetricLineEndPos;
        }

        
    }
}