using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class PerlinFlow : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float perlinStep;

    [SerializeField]
    private int cellSize;

    [SerializeField]
    private int interval = 1;

    [SerializeField]
    public ParticleSystem particleSystem;

    public int cols;
    public int rows;

    private Color[,] colArray;
    private Vector3[,] vecArray;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        cols = Screen.width / cellSize;
        rows = Screen.height / cellSize;
        Debug.Log("cols: " + (cols));
        Debug.Log("rows: " + (rows));

        colArray = new Color[rows, cols];
        vecArray = new Vector3[rows, cols];

        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];

        GenerateVectorField();

        particleSystem.Emit(1050);

        GenerateParticles();
    }

    void Update()
    {
        UpdateParticles();
    }

    private void UpdateParticles()
    {
        int numAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numAlive; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            particle = PositionParticle(particle);
            particle.color = ColorFromPerlinVals(particle);
//            particle.velocity = VelFromPerlinVals(particle);
            particles[i] = particle;

//            particle.velocity = Random.insideUnitSphere;
        }

        particleSystem.SetParticles(particles, numAlive);
    }

    

    private void GenerateVectorField()
    {
        UnityEngine.Random.InitState(1);

        float xOffset = 0;

        for (int i = 0; i < rows; i++)
        {
            float yOffset = 0;

            for (int j = 0; j < cols; j++)
            {
                var r = Perlin.Noise(xOffset, yOffset);
                Color color = new Color(r, r, r);
                colArray[i, j] = color;
                vecArray[i, j] = new Vector3(r, r, 0);

                yOffset += perlinStep;
            }

            xOffset += perlinStep;
        }
    }

    private void GenerateParticles()
    {
        int numAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numAlive; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            particle.position = RandomScreenPoistion();
//                        particle.velocity = VelFromPerlinVals(particle);

            particles[i] = particle;
        }

        particleSystem.SetParticles(particles, numAlive);
    }

    private Color32 ColorFromPerlinVals(ParticleSystem.Particle particle)
    {
        var particlePosition = ScreenPos(particle.position);
        int i = (int) (particlePosition.x * Screen.height / cellSize);
        int j = (int) (particlePosition.y * Screen.width / cellSize);
        i = Mathf.Clamp(i, 0, rows - 1);
        j = Mathf.Clamp(j, 0, cols - 1);
        
        var color = colArray[i, j];
        return new Color32((byte) (color.r * 255), (byte) (color.g * 255), (byte) (color.b * 255), 255);
    }
    
    private Vector3 VelFromPerlinVals(ParticleSystem.Particle particle)
    {
        var particlePosition = ScreenPos(particle.position);
        int i = (int) (particlePosition.x * Screen.height / cellSize);
        int j = (int) (particlePosition.y * Screen.width / cellSize);
        i = Mathf.Clamp(i, 0, rows - 1);
        j = Mathf.Clamp(j, 0, cols - 1);
        return vecArray[i, j];
    }


    private ParticleSystem.Particle PositionParticle(ParticleSystem.Particle particle)
    {
        var mainCamera = cam;

        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(particle.position);

        if (viewportPoint.x < 0)
        {
            viewportPoint.x = 1;
        }
        else if (viewportPoint.x > 1)
        {
            viewportPoint.x = 0;
        }
        else if (viewportPoint.y < 0)
        {
            viewportPoint.y = 1;
        }
        else if (viewportPoint.y > 1)
        {
            viewportPoint.y = 0;
        }

        particle.position = mainCamera.ViewportToWorldPoint(viewportPoint);

        return particle;
    }

    private Vector3 ScreenPos(Vector3 worldPos)
    {
        return cam.WorldToViewportPoint(worldPos);
    }

    private Vector3 RandomScreenPoistion()
    {
        Vector3 randomPoint = new Vector3(Random.value, Random.value);
        randomPoint.z = cam.WorldToViewportPoint(Vector3.zero).z;
        var viewportToWorldPoint = cam.ViewportToWorldPoint(randomPoint);
        viewportToWorldPoint.z = 0;
        return viewportToWorldPoint;
    }
}