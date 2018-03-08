using UnityEngine;

namespace CodingChallenge.PerlinFlow
{
    public class TextureGenerator : MonoBehaviour
    {
        [SerializeField]
        protected MeshRenderer objectRenderer;
        
        [SerializeField]
        protected int textureDim = 1024;
        
        protected Texture2D texture;

        protected Color[] colors;
        
        protected void InitTexture()
        {
            colors = new Color[textureDim * textureDim];
            texture = new Texture2D(textureDim, textureDim, TextureFormat.ARGB32, false);
        }

        protected void ApplyTexture()
        {
            texture.SetPixels(colors);
            texture.Apply();
            objectRenderer.material.mainTexture = texture;
        }

        protected virtual void GenerateTexture()
        {
           
        }
    }
}