using System.Collections.Generic;
using UnityEngine;

namespace CodingChallenge.Terrain
{
    public class PerlinTerrain : MonoBehaviour
    {
        [SerializeField]
        private int cols;

        [SerializeField]
        private int rows;

        [SerializeField]
        private float triangleSize = 1;

        [SerializeField]
        private Material meshMaterial;

        private Mesh mesh;

        private List<Vector3> vertices = new List<Vector3>();
        private List<Vector2> uvs = new List<Vector2>();
        private List<int> triangles = new List<int>();

        void Start()
        {
            CreateMesh();
            CreateTerrain();
            transform.position = new Vector3(-cols* triangleSize / 2, transform.position.y , 0);
        }

        private void CreateMesh()
        {
            var meshFilter = gameObject.AddComponent<MeshFilter>();
            var meshRenderer = gameObject.AddComponent<MeshRenderer>();
            mesh = meshFilter.mesh;
            meshRenderer.material = meshMaterial;
        }

        private void CreateTerrain()
        {
            ClearMesh();
            GenerateMesh();
            FillMesh();
        }

        private void ClearMesh()
        {
            mesh.Clear();
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();
        }

        private void GenerateMesh()
        {
            for (int i = 0; i <= rows; i++)
            {
                for (int j = 0; j <= cols; j++)
                {
                    vertices.Add(new Vector3(j * triangleSize, i * triangleSize, 0));
                }
            }

            //quantity of vertices in a row
            int vr = cols + 1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    triangles.Add(j + vr * i);
                    triangles.Add(j + vr * (i + 1));
                    triangles.Add(j + 1 + vr * (i + 1));

                    triangles.Add(j + vr * i);
                    triangles.Add(j + 1 + vr * (i + 1));
                    triangles.Add(j + 1 + vr * i);
                }
            }
        }

        private void FillMesh()
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                var vertex = vertices[i];
                uvs.Add(new Vector2(vertex.x, vertex.y));
            }

            mesh.vertices = vertices.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.triangles = triangles.ToArray();
        }

        void Update()
        {
            var meshVertices = mesh.vertices;
            for (int i = 0; i < meshVertices.Length; i++)
            {
                meshVertices[i].z = Random.Range(-0.3f, 0.3f);
            }

            mesh.vertices = meshVertices;
        }
    }
}