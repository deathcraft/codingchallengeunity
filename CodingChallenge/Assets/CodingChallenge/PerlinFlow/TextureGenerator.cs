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
        
        
        protected void InitTexture()
        {
            texture = new Texture2D(textureDim, textureDim, TextureFormat.ARGB32, false);
        }

        protected void ApplyTexture()
        {
            texture.Apply();
            objectRenderer.material.mainTexture = texture;
        }

        protected virtual void GenerateTexture()
        {
           
        }
    }
}