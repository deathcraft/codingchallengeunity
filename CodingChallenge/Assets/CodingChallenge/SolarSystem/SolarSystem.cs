using UnityEngine;

namespace CodingChallenge.SolarSystem
{
    public class SolarSystem : MonoBehaviour
    {
        [SerializeField]
        private Planet sun;
        
        [SerializeField]
        private GameObject planetPrototype;

        void Start()
        {
            sun.SpawnMoons(5, planetPrototype);

            foreach (var planet in sun.Planets)
            {
                planet.SpawnMoons(Random.Range(0, 4), planetPrototype);
            }
        }
    }
}