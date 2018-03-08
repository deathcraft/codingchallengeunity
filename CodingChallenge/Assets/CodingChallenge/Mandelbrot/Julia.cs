using UnityEngine;

namespace CodingChallenge.Mandelbrot
{
    public class Julia : MonoBehaviour
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
        private float cReal;
        
        [SerializeField]
        private float cImagine;
        
        [SerializeField]
        private float angleSpeed;
        
        [SerializeField]
        private float cRealMin;
        
        [SerializeField]
        private float cRealMax;
        
        [SerializeField]
        private float cImagineMin;
        
        [SerializeField]
        private float cImagineMax;

        [SerializeField]
        private Color[] palette = {Color.black, Color.blue, Color.green, Color.red, Color.yellow, Color.black};

        private Texture2D texture;

        private float angle;

        void Start()
        {
            GenerateTexture();
        }

        void Update()
        {
            angle += angleSpeed * Time.deltaTime;
            float ca = Mathf.Cos(angle);
            float cb = Mathf.Sin(angle);
            
            cReal = MathUtil.Map(ca, -1, 1, cRealMin, cRealMax);
            cImagine = MathUtil.Map(cb, -1, 1, cImagineMin, cImagineMax);
            
            GenerateTexture();
        }

        private void GenerateTexture()
        {
            texture = new Texture2D(textureDim, textureDim, TextureFormat.ARGB32, false);
            GenerateJulia();
            objectRenderer.material.mainTexture = texture;
        }

        private void GenerateJulia()
        {

            float dx = 2 * width / textureDim;
            float dy = 2 * height / textureDim;
            
            float x = -width;
            for (int i = 0; i < textureDim; i++)
            {
                float y = -height;
                for (int j = 0; j < textureDim; j++)
                {
                    float a = x;
                    float b = y;

                    int n = 0;

                    while (n < iterations)
                    {
                        float aa = a * a;
                        float bb = b * b;

                        if (Mathf.Abs(aa + bb) > 4)
                        {
                            break;
                        }
                        
                        float twoab = 2f * a * b;
                        
                        a = aa - bb + cReal;
                        b = twoab + cImagine;

                        n++;
                    }

                    int id = (int) MathUtil.Map(n, 0, iterations, 0, palette.Length - 2);
                    Color color = palette[id];
                    Color color2 = palette[id + 1];
                    color = MathUtil.ColorLerp(color, color2, iterations % 1);
                    texture.SetPixel(i, j, color);
                    y += dy;

                }
                x += dx;


            }

            texture.Apply();
        }
    }
}