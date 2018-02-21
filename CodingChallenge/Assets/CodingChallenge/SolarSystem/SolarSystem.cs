using System.Security.Permissions;
using UnityEngine;

namespace CodingChallenge.SolarSystem
{
    public class SolarSystem : MonoBehaviour
    {
        [SerializeField]
        private Planet sun;
        
        [SerializeField]
        private GameObject planetPrototype;
        
        [SerializeField]
        private int numPlanets = 5;
        
        [SerializeField]
        private int maxSatellites = 4;

        void Start()
        {
            sun.SpawnMoons(numPlanets, planetPrototype);

            foreach (var planet in sun.Planets)
            {
                planet.SpawnMoons(Random.Range(0, maxSatellites), planetPrototype);
            }
            
        }
    }
}