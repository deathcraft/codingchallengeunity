using System.Xml.Schema;
using UnityEngine;

namespace CodingChallenge
{
    public static class MathUtil
    {
        public  static Vector3 RotateVector(Vector3 vector, float angle)
        {
            Vector3 rotatedVector = vector;
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);
            
            rotatedVector.x = vector.x * cos - vector.y * sin;
            rotatedVector.y = vector.x * sin + vector.y * cos;
            return rotatedVector;
        }

        public static float Map(float val, float min1, float max1, float min2, float max2)
        {
            return min2 + (val - min1) * (max2 - min2) / (max1 - min1);
        }

        public static Color ColorLerp(Color c1, Color c2, float val)
        {
            float r = Mathf.Lerp(c1.r, c2.r, val);
            float g = Mathf.Lerp(c1.g, c2.g, val);
            float b = Mathf.Lerp(c1.b, c2.b, val);
            return new Color(r,g,b);
        }

        public static Vector3 PositionFromScreenPixel(float x, float y, Camera cam)
        {
            Vector3 screenPos = new Vector3(x, y, -cam.transform.position.z);
            Vector3 worldPoint = cam.ScreenToWorldPoint(screenPos);
            return worldPoint;
        }
        
        public static Vector3 WorldToScreenPos(Vector3 worldPos, Camera cam)
        {
            var worldToViewportPoint = cam.WorldToViewportPoint(worldPos);
            if (worldToViewportPoint.x < 0)
            {
                worldToViewportPoint.x = 0;
            }

            if (worldToViewportPoint.y < 0)
            {
                worldToViewportPoint.y = 0;
            }

            return worldToViewportPoint;
        }
        
         
        public static Vector3 BoundPositionByCamera(Vector3 pos)
        {
            var mainCamera = Camera.main;

            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(pos);

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

           return mainCamera.ViewportToWorldPoint(viewportPoint);
        }
    }
}