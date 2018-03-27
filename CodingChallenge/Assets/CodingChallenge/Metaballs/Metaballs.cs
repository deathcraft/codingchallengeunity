using System.Collections;
using System.Collections.Generic;
using CodingChallenge;
using CodingChallenge.Metaballs;
using UnityEngine;

public class Metaballs : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    public ParticleSystem particleSystem;

    [SerializeField]
    public Blob[] blobs;

    [SerializeField]
    public float distanceCoeff = 50;

    [SerializeField]
    public int ballCount;

    [SerializeField]
    public GameObject ballPrefab;

    private ParticleSystem.Particle[] particles;

    private int columns;
    private int rows;
    
    void Start()
    {
        blobs = new Blob[ballCount];
        
        for (int i = 0; i < ballCount; i++)
        {
            blobs[i] = Instantiate(ballPrefab).GetComponent<Blob>();
        }
        
        
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
		var maxParticles = particleSystem.main.maxParticles; // == columns * rows
        particleSystem.Emit(maxParticles);

        rows = (int) Mathf.Sqrt(maxParticles * Screen.height / Screen.width);
        columns = maxParticles / rows;
        
        GenerateParticles();
    }

    private void GenerateParticles()
    {
        int numAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var id = j + i * rows;
                ParticleSystem.Particle particle = particles[id];
                float x = MathUtil.Map(i, 0, columns, 0, Screen.width);
                float y = MathUtil.Map(j, 0, rows, 0, Screen.height);
                particle.position = MathUtil.PositionFromScreenPixel(x, y, cam);

                float sum = 0;
                foreach (Blob blob in blobs)
                {
                    float distance = Vector3.Distance(blob.transform.position, particle.position);
                    var r = 1 / (distance  * distanceCoeff);
                    sum += r;
                }

                particle.color = Color.HSVToRGB(sum % 1, 1, 1);
                
                particles[id] = particle;
            }
        }

        //hide leftovers
        for (int i = columns * rows; i < particleSystem.main.maxParticles; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            var color = new Color();
            color.a = 0;
            particle.color = color;
            particles[i] = particle;

        }

        particleSystem.SetParticles(particles, numAlive);
    }

    void Update()
    {
        GenerateParticles();
    }
}