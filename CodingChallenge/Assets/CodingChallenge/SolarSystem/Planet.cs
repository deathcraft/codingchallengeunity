using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodingChallenge.SolarSystem
{
	public class Planet : MonoBehaviour
	{
		[SerializeField]
		private float planetRadius = 1;

		[SerializeField]
		private float angle = 0;

		[SerializeField]
		private float distance = 1f;
		
		[SerializeField]
		private float orbitSpeed = 0.5f;

		[SerializeField]
		private Planet[] planets;

		public Planet[] Planets
		{
			get { return planets; }
			set { planets = value; }
		}

		void Start()
		{
			transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
		}

		public void SpawnMoons(int n, GameObject prefab)
		{
			planets = new Planet[n];

			float currentDistance = 0;
			
			for (int i = 0; i < planets.Length; i++)
			{
				var planetInstance = Instantiate(prefab, transform);
				var planet = planetInstance.GetComponent<Planet>();
				planet.planetRadius = planetRadius * Random.Range(0.2f, 0.4f);
				currentDistance += Random.Range(0.5f, 1.7f);
				planet.distance = currentDistance;

				float angle = Random.Range(0, Mathf.PI * 2);
				planet.angle = angle;
				planet.SetPositionFromAngle();

				planet.orbitSpeed = Random.Range(-1f, 1f);
				
				planets[i] = planet;

				RandomMaterialColor(planet);
			}
		}

		public void SetPositionFromAngle()
		{
			Vector3 localPoistion = Vector3.zero;
			localPoistion.x = distance * Mathf.Cos(angle);
			localPoistion.y = distance * Mathf.Sin(angle);
			transform.localPosition = localPoistion;
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
			angle += (orbitSpeed * Time.deltaTime) % (Mathf.PI * 2);
			SetPositionFromAngle();
		}
	}
}