using UnityEngine;

namespace CodingChallenge.Terrain
{
    public class TerrainSin : MonoBehaviour
    {
        [SerializeField]
        private TerrainGenerator terrainGenerator;
        
        void Update()
        {
            var meshVertices = terrainGenerator.Mesh.vertices;
            for (int i = 0; i < meshVertices.Length; i++)
            {
                meshVertices[i].z = Mathf.Sin(Time.time + i);
            }

            terrainGenerator.Mesh.vertices = meshVertices;
        }
    }
}