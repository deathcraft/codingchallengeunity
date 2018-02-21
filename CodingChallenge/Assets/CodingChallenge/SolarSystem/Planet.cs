using UnityEngine;

namespace CodingChallenge.SolarSystem
{
    public class Planet : MonoBehaviour
    {
        [SerializeField]
        private float planetRadius = 1;

        [SerializeField]
        private float distance = 1f;

        [SerializeField]
        private float orbitSpeed = 0.5f;

        [SerializeField]
        private Planet[] planets;

        [SerializeField]
        private LineRenderer lineRenderer;


        private Vector3 distanceVector;
        private Vector3 rotationAxisLocal;

        public Planet[] Planets
        {
            get { return planets; }
        }

        void Start()
        {
            transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
        }

        public void SpawnMoons(int n, GameObject prefab)
        {
            planets = new Planet[n];

            float currentDistance = 0.5f;

            for (int i = 0; i < planets.Length; i++)
            {
                var planetInstance = Instantiate(prefab, transform);
                var planet = planetInstance.GetComponent<Planet>();
                planet.planetRadius = planetRadius * Random.Range(0.2f, 0.4f);
                currentDistance += Random.Range(1.5f, 2.7f) / (1 + planetRadius);
                planet.distance = currentDistance;

                planet.SetPositionFromAngle();
                planet.rotationAxisLocal = Vector3.Cross(planet.transform.localPosition, Random.insideUnitSphere);

                planet.orbitSpeed = Random.Range(-360f, 360f);

                planets[i] = planet;

                RandomMaterialColor(planet);
            }
        }

        private void SetPositionFromAngle()
        {
            distanceVector = Random.insideUnitSphere * distance;
            transform.localPosition = distanceVector;
        }

        private void RandomMaterialColor(Planet planet)
        {
            var meshRenderer = planet.GetComponent<MeshRenderer>();
            var material = meshRenderer.materials[0];
            var materialInstance = Instantiate(material);
            meshRenderer.materials[0] = materialInstance;
            meshRenderer.materials[0].color = Random.ColorHSV();
        }

        void Update()
        {
            if (transform.parent != null)
            {
                Vector3 worldRotationAxis = transform.parent.TransformDirection(rotationAxisLocal);

                transform.RotateAround(transform.parent.position, worldRotationAxis, orbitSpeed * Time.deltaTime);

                lineRenderer.SetPosition(0, transform.parent.position);
                lineRenderer.SetPosition(1, worldRotationAxis * 100);
            }
        }
    }
}