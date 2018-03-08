using CodingChallenge.PerlinFlow;
using UnityEngine;

public class PerlinFlow : TextureGenerator
{
    [SerializeField]
    private float perlinStep;

    [SerializeField]
    private float scale = 20;

    private int cols;
    private int rows;

    protected void Start()
    {
        cols = (int) (textureDim / scale);
        rows = cols;
        
        InitTexture();
        GenerateTexture();
        ApplyTexture();
    }

    protected override void GenerateTexture()
    {
        float yOffset = 0;
        float xOffsetStart = 0;
        for (int i = 0; i < rows; i++)
        {
            float xOffset = xOffsetStart;

            for (int j = 0; j < cols; j++)
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