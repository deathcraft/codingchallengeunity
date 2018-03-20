using System.Collections.Generic;
using UnityEngine;

namespace CodingChallenge.Sphere
{
    public class SphereGenerator : MonoBehaviour
    {
        [SerializeField]
        private int total;

        [SerializeField]
        private float r;

        [SerializeField]
        private Material meshMaterial;
        
        [SerializeField]
        private Gradient gradient;

        private Mesh mesh;

        public Mesh Mesh
        {
            get { return mesh; }
        }

        private List<Vector3> vertices = new List<Vector3>();
        private List<Color> colors = new List<Color>();
        private List<Vector2> uvs = new List<Vector2>();
        private List<int> triangles = new List<int>();

        void Awake()
        {
            CreateMesh();
            CreateSphere();
        }

        private void CreateMesh()
        {
            var meshFilter = gameObject.AddComponent<MeshFilter>();
            var meshRenderer = gameObject.AddComponent<MeshRenderer>();
            mesh = meshFilter.mesh;
            meshRenderer.material = meshMaterial;
        }

        private void CreateSphere()
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
            for (int i = 0; i <= total; i++)
            {
                float lat = MathUtil.Map(i, 0, total, 0, Mathf.PI);
                float col = MathUtil.Map(i, 0, total, 0, 1);
                for (int j = 0; j <= total; j++)
                {
                    float lon = MathUtil.Map(j, 0, total, 0, Mathf.PI * 2);
                    float x = r * Mathf.Sin(lat) * Mathf.Cos(lon);
                    float y = r * Mathf.Sin(lat) * Mathf.Sin(lon);
                    float z = r * Mathf.Cos(lat);
                    vertices.Add(new Vector3(x, y, z));
                    colors.Add(gradient.Evaluate(col));
                }
            }

            //quantity of vertices in a row
            int vr = total + 1;
            for (int i = 0; i < total; i++)
            {
                for (int j = 0; j < total; j++)
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
            mesh.colors = colors.ToArray();
        }
    }
}