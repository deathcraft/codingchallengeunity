using System;
using System.Diagnostics;
using CodingChallenge;
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
    private int velMultiplier = 1;

    [SerializeField]
    private int particleNum = 2000;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    public ParticleSystem particleSystem;

    public int cols;
    public int rows;

    private Color[,] colArray;
    private Vector3[,] vecArray;
    private ParticleSystem.Particle[] particles;

    private float startXOffset;
    private float startYOffset;

    private float zOffset;

    void Start()
    {
        cols = Screen.width / cellSize;
        rows = Screen.height / cellSize;

        startXOffset = Random.value;
        startYOffset = Random.value;

        colArray = new Color[rows, cols];
        vecArray = new Vector3[rows, cols];

        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];

        GenerateVectorField();

        particleSystem.Emit(particleNum);

        GenerateParticles();
    }

    void Update()
    {
        UpdateParticles();
        UpdateVectors();
    }

    private void UpdateParticles()
    {
        int numAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numAlive; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            particle.color = ColorFromPerlinVals(particle);
            particle.velocity = VelFromPerlinVals(particle) * velMultiplier;
            particle = PositionParticle(particle);
            particles[i] = particle;
        }

        particleSystem.SetParticles(particles, numAlive);
    }

    private void ShiftStartOffsets()
    {
        startXOffset += perlinStep;
        startYOffset += perlinStep;
    }

    private void UpdateVectors()
    {
        float xOffset = startXOffset;

        for (int i = 0; i < rows; i++)
        {
            float yOffset = startYOffset;

            for (int j = 0; j < cols; j++)
            {
                var noise = Noise(xOffset, yOffset, zOffset);
                FillVector(noise, i, j);
                yOffset += perlinStep;
            }

            xOffset += perlinStep;
        }

        zOffset += perlinStep;
    }


    private void GenerateVectorField()
    {
        float xOffset = startXOffset;

        for (int i = 0; i < rows; i++)
        {
            float yOffset = startYOffset;

            for (int j = 0; j < cols; j++)
            {
                var noise = Noise(xOffset, yOffset, 0);

                FillColor(noise, i, j);
                FillVector(noise, i, j);

                yOffset += perlinStep;
            }

            xOffset += perlinStep;
        }
    }

    private float Noise(float x, float y, float z)
    {
        var keijiroNoise = Perlin.Noise(x, y, z);
        keijiroNoise = MathUtil.Map(keijiroNoise, -1, 1, 0, 1);
        return keijiroNoise;
        return Mathf.PerlinNoise(x, y);
    }

    private void FillVector(float noise, int i, int j)
    {
        float angle = noise * 360;
        Vector3 vec = Quaternion.Euler(0, 0, angle) * Vector3.up;
        vecArray[i, j] = vec;
    }

    private void FillVectorStaright(float noise, int i, int j)
    {
        vecArray[i, j] = new Vector3(noise, noise, 0);
    }


    private void FillColor(float noise, int i, int j)
    {
        var color = gradient.Evaluate(noise);
        colArray[i, j] = color;
    }

    private void GenerateParticles()
    {
        int numAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numAlive; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            particle.position = RandomScreenPoistion();
            particles[i] = particle;
        }

        particleSystem.SetParticles(particles, numAlive);
    }

    private Color32 ColorFromPerlinVals(ParticleSystem.Particle particle)
    {
        var particlePosition = MathUtil.WorldToScreenPos(particle.position, cam);
        int i = (int) (particlePosition.y * (Screen.height - rows) / cellSize);
        int j = (int) (particlePosition.x * (Screen.width - cols) / cellSize);

        var color = colArray[i, j];
        return new Color32((byte) (color.r * 255), (byte) (color.g * 255), (byte) (color.b * 255), 255);
    }

    private Vector3 VelFromPerlinVals(ParticleSystem.Particle particle)
    {
        var particlePosition = MathUtil.WorldToScreenPos(particle.position, cam);
        int i = (int) (particlePosition.y * (Screen.height - rows) / cellSize);
        int j = (int) (particlePosition.x * (Screen.width - cols) / cellSize);
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


    

    private Vector3 RandomScreenPoistion()
    {
        Vector3 randomPoint = new Vector3(Random.value, Random.value);
        randomPoint.z = cam.WorldToViewportPoint(Vector3.zero).z;
        var viewportToWorldPoint = cam.ViewportToWorldPoint(randomPoint);
        viewportToWorldPoint.z = 0;
        return viewportToWorldPoint;
    }
}