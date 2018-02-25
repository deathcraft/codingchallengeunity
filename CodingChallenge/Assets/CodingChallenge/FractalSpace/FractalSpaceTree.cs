using System.Collections;
using System.Collections.Generic;
using CodingChallenge.FractalTree;
using UnityEngine;

namespace CodingChallenge.Terrain
{
	public class FractalSpaceTree : MonoBehaviour
	{

		[SerializeField]
		private GameObject leafPrefab;
		
		[SerializeField]
		private GameObject branchPrefab;

		[SerializeField]
		private int leafNumber;
		
		[SerializeField]
		private float speed = 0.5f;
		
		[SerializeField]
		private float growCoeff = 2f;
		
		[SerializeField]
		private Transform rootPosition;
		
		[SerializeField]
		private float maxDistance;
		
		[SerializeField]
		private float minDistance;
		
		private List<FractalSpaceLeaf> leaves = new List<FractalSpaceLeaf>();
		private List<FractalSpaceBranch> branches = new List<FractalSpaceBranch>();


		private float elapsed;
		
		void Start()
		{
			GenerateLeaves();
			CreateRoot();
			GenerateBranches();
		}

		private void Update()
		{
			elapsed += Time.deltaTime;

			if (elapsed > speed)
			{
				Grow();
				DeleteFoundLeaves();
				GenerateNewBranches();
				elapsed = 0;
			}
		}

		private void DeleteFoundLeaves()
		{
			List<FractalSpaceLeaf> newLeaves = new List<FractalSpaceLeaf>();
			
			foreach (var leaf in leaves)
			{
				if (leaf.Reached)
				{
					Destroy(leaf.gameObject);
				}
				else
				{
					newLeaves.Add(leaf);
				}
			}

			leaves = newLeaves;
		}

		private void GenerateNewBranches()
		{
		 	List<FractalSpaceBranch> branchedBranches = new List<FractalSpaceBranch>();
			
			foreach (var branch in branches)
			{
				if (branch.CurrentCount() > 0)
				{
					branchedBranches.Add(branch);
				}
			}
			
			foreach (var branch in branchedBranches)
			{
				var direction = branch.direction * growCoeff/*/ branch.CurrentCount()*/;
				CreateBranch(branch, direction, branch.EndPosition);
				branch.ResetBranch();
			}
		}

		private void Grow()
		{
			for (int i = 0; i < leaves.Count; i++)
			{
				FractalSpaceLeaf leaf = leaves[i];

				FractalSpaceBranch closestBranch = null;
				float smallestDistance = float.MaxValue;
				
				foreach (var branch in branches)
				{
					float distance = Vector3.Distance(leaf.LeafPosition, branch.EndPosition);

					if (distance < minDistance)
					{
						leaf.Reached = true;
						closestBranch = null;
						break;
					}

					if (distance < smallestDistance || closestBranch == null)
					{
						smallestDistance = distance;
						closestBranch = branch;
					}
				}

				if (closestBranch != null)
				{
					if (closestBranch.CurrentCount() == 0)
					{
						closestBranch.direction += (leaf.LeafPosition - closestBranch.EndPosition).normalized;
						closestBranch.Increment();	
					}
					
				}
			}
		}

		private void CreateRoot()
		{
			CreateBranch(null, Vector3.up, rootPosition.position);
		}

		private FractalSpaceBranch CreateBranch(FractalSpaceBranch parent, Vector3 direction, Vector3 startPosition)
		{
			var branch = Instantiate(branchPrefab);
			var fractalSpaceBranch = branch.GetComponent<FractalSpaceBranch>();
			fractalSpaceBranch.parent = parent;
			fractalSpaceBranch.direction = direction;
			fractalSpaceBranch.startPosition = startPosition;
			fractalSpaceBranch.Init();
			branches.Add(fractalSpaceBranch);
			return fractalSpaceBranch;
		}

		private void GenerateBranches()
		{
			FractalSpaceBranch current = branches[0];
			bool found = false;

			while (!found)
			{
				for (int i = 0; i < leaves.Count; i++)
				{
					var leafPosition = leaves[i].LeafPosition;
					var distance = Vector3.Distance(leafPosition, current.EndPosition);
					
					if (distance < minDistance)
					{
						found = true;
					}
				}

				if (!found)
				{
					current = CreateBranch(current, current.direction, current.EndPosition);
				}
			}
		}

		private void GenerateLeaves()
		{
			for (int i = 0; i < leafNumber; i++)
			{
				CreateLeaf();
			}
		}

		private void CreateLeaf()
		{
			var leafGameObject = Instantiate(leafPrefab, transform);
			var leaf = leafGameObject.GetComponent<FractalSpaceLeaf>();
			leaves.Add(leaf);
			leaf.RandomPos();
		}
	}
}
