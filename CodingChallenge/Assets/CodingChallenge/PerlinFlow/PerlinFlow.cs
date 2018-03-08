using CodingChallenge.PerlinFlow;
using UnityEngine;

public class PerlinFlow : TextureGenerator
{
    [SerializeField]
    private float perlinStep;

    protected void Start()
    {
        InitTexture();
        GenerateTexture();
        ApplyTexture();
    }

    void Update()
    {
        GenerateTexture();
        ApplyTexture();
    }
    
    protected override void GenerateTexture()
    {
        float yOffset = 0;
        float xOffsetStart = 0;
        for (int i = 0; i < textureDim; i++)
        {
            float xOffset = xOffsetStart;

            for (int j = 0; j < textureDim; j++)
            {
                var r = Mathf.PerlinNoise(xOffset, yOffset);
                Color color = new Color(r,r,r);
                colors[i * textureDim + j] = color;
                xOffset += perlinStep;
            }
            
            yOffset += perlinStep;
        }
        

    }
    
    
}