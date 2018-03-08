using CodingChallenge;
using UnityEngine;

public class Mandelbrot : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer objectRenderer;

    [SerializeField]
    private int textureDim = 1024;

    [SerializeField]
    private int iterations = 100;

    [SerializeField]
    private float width;

    [SerializeField]
    private float height;

    [SerializeField]
    private Color[] palette = {Color.black, Color.blue, Color.green, Color.red, Color.yellow, Color.black};

    private Texture2D texture;

    void Start()
    {
        GenerateTexture();
    }

    private void GenerateTexture()
    {
        texture = new Texture2D(textureDim, textureDim, TextureFormat.ARGB32, false);
        GenerateMandelbrot();
        objectRenderer.material.mainTexture = texture;
    }


    private void GenerateMandelbrot()
    {
        for (int i = 0; i < textureDim; i++)
        {
            for (int j = 0; j < textureDim; j++)
            {
                float a = MathUtil.Map(i, 0, textureDim, -width, width);
                float b = MathUtil.Map(j, 0, textureDim, -height, height);
                float ca = a;
                float cb = b;

                int n = 0;

                while (n < iterations)
                {
                    float aa = a * a - b * b;
                    float bb = 2 * a * b;

                    a = aa + ca;
                    b = bb + cb;

                    if (Mathf.Abs(a + b) > 16)
                    {
                        break;
                    }

                    n++;
                }



                int id = (int) MathUtil.Map(n, 0, iterations, 0, palette.Length - 2);
                Color color = palette[id];
                Color color2 = palette[id + 1];
                color = MathUtil.ColorLerp(color, color2, iterations % 1);
                texture.SetPixel(i, j, color);
            }
        }

        texture.Apply();
    }

}