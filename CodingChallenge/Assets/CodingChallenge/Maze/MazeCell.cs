using UnityEngine;

namespace CodingChallenge.Maze
{
	public class MazeCell : MonoBehaviour
	{
		[SerializeField]
		private GameObject leftWall;
		
		[SerializeField]
		private GameObject rightWall;
		
		[SerializeField]
		private GameObject topWall;
		
		[SerializeField]
		private GameObject bottomWall;

		private Material material;

		void Awake()
		{
			var meshRenderer = leftWall.GetComponent<MeshRenderer>();
			var oldMat = meshRenderer.material;
			material = Instantiate(oldMat);
			
			SetMaterial(leftWall, material);
			SetMaterial(rightWall, material);
			SetMaterial(topWall, material);
			SetMaterial(bottomWall, material);
		}

		public void RemoveLeftWall()
		{
			leftWall.SetActive(false);
		}
		
		public void RemoveRightWall()
		{
			rightWall.SetActive(false);
		}

		public void RemoveTopWall()
		{
			topWall.SetActive(false);
		}

		public void RemoveBottomWall()
		{
			bottomWall.SetActive(false);
		}

		private void SetMaterial(GameObject obj, Material material)
		{
			var meshRenderer = obj.GetComponent<MeshRenderer>();
			meshRenderer.material = material;
		}
		
		public void ChangeColor(Color color)
		{
			material.color = color;
			material.name = "Color Updated";
		}
	}
}