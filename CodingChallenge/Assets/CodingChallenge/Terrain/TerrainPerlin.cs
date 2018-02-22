using Boo.Lang;
using UnityEngine;

namespace CodingChallenge.Terrain
{
    public class TerrainPerlin : MonoBehaviour
    {
        [SerializeField]
        private TerrainGenerator terrainGenerator;

        [SerializeField]
        private float perlinStep;
        
        [SerializeField]
        private float minVal;
        
        [SerializeField]
        private float maxVal;
        
        [SerializeField]
        private float flySpeed;

        private float flying;
        
        void Update()
        {
            flying += flySpeed;
            
            List<float> randomZ = new List<float>();

            float yOffset = flying;
            for (int i = 0; i <= terrainGenerator.Rows; i++)
            {
                float xOffset = 0;
                for (int j = 0; j <= terrainGenerator.Cols; j++)
                {
                    float val = Mathf.PerlinNoise(xOffset, yOffset);
                    val = MathUtil.Map(val, 0, 1, minVal, maxVal);
                    randomZ.Add(val);
                    xOffset += perlinStep;
                }

                yOffset += perlinStep;
            }
            
            var meshVertices = terrainGenerator.Mesh.vertices;
            for (int i = 0; i < meshVertices.Length; i++)
            {
                meshVertices[i].z = randomZ[i];
            }
            terrainGenerator.Mesh.vertices = meshVertices;
        }
    }
}