using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodingChallenge.Maze
{
    public class MazeGenerator : MonoBehaviour
    {
        private readonly List<int> neighbours = new List<int>();

        [SerializeField]
        private int cols;

        [SerializeField]
        private int rows;

        [SerializeField]
        private float xOffset;

        [SerializeField]
        private float yOffset;

        [SerializeField]
        private GameObject mazeCellPrefab;
        
        [SerializeField]
        private float delaySec = 0.5f;

        private MazeCell[] mazeCells;
        private bool[] visitedCells;

        private int currentCellId;
        private float elapsed;
        
        private Stack<int> cellStack = new Stack<int>();

        void Start()
        {
            CreateMaze();
            OffsetToFitScreen();

            currentCellId = 0;
            SetVisited(currentCellId);
        }

        void Update()
        {
            elapsed += Time.deltaTime;

            if (elapsed >= delaySec)
            {
                elapsed = 0;
                MoveNextNeighbour();
            }
        }

        private void MoveNextNeighbour()
        {
            var neigbourId = PickNeighbour(currentCellId);

            if (neigbourId != -1)
            {
                SetVisited(neigbourId);
                RemoveWall(neigbourId);
                cellStack.Push(currentCellId);
                currentCellId = neigbourId;
            }
            else if (cellStack.Count > 0)
            {
                currentCellId = cellStack.Pop();
            }
        }

        private void RemoveWall(int neigbourId)
        {
            if (neigbourId == MazeUtil.LeftCellId(currentCellId, rows, cols))
            {
                mazeCells[neigbourId].RemoveRightWall();
                mazeCells[currentCellId].RemoveLeftWall();
            } else if (neigbourId == MazeUtil.RightCellId(currentCellId, rows, cols))
            {
                mazeCells[neigbourId].RemoveLeftWall();
                mazeCells[currentCellId].RemoveRightWall();
            } else if (neigbourId == MazeUtil.TopCellId(currentCellId, rows, cols))
            {
                mazeCells[neigbourId].RemoveBottomWall();
                mazeCells[currentCellId].RemoveTopWall();
            } else 
            {
                mazeCells[neigbourId].RemoveTopWall();
                mazeCells[currentCellId].RemoveBottomWall();
            }
        }

        private void SetVisited(int id)
        {
            MazeCell cell = mazeCells[id];
            visitedCells[id] = true;
            cell.ChangeColor(Color.blue);
        }

        private void OffsetToFitScreen()
        {
            var transformPosition = transform.position;
            transformPosition.x -= cols / 2f + xOffset * cols + 1;
            transformPosition.y -= rows / 2f + yOffset * rows + 1;

            transform.position = transformPosition;
        }

        private int PickNeighbour(int id)
        {
            neighbours.Clear();

            FillNeighbour(MazeUtil.LeftCellId(id, rows, cols));
            FillNeighbour(MazeUtil.RightCellId(id, rows, cols));
            FillNeighbour(MazeUtil.TopCellId(id, rows, cols));
            FillNeighbour(MazeUtil.BottomCellId(id, rows, cols));

            if (neighbours.Count > 0)
            {
                return neighbours[Random.Range(0, neighbours.Count)];
            }

            return -1;
        }

        private void FillNeighbour(int cellId)
        {
            if (cellId != -1 && visitedCells[cellId] == false)
            {
                neighbours.Add(cellId);
            }
        }

        private void CreateMaze()
        {
            var numberCells = cols * rows;
            mazeCells = new MazeCell[numberCells];
            visitedCells = new bool[numberCells];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var cellInstance = Instantiate(mazeCellPrefab);
                    var mazeCell = cellInstance.GetComponent<MazeCell>();
                    mazeCells[i * cols + j] = mazeCell;

                    Vector3 pos = Vector3.zero;
                    pos.y = i * (1 + yOffset);
                    pos.x = j * (1 + xOffset);
                    cellInstance.transform.position = pos;

                    mazeCell.transform.parent = transform;
                }
            }
        }
    }
}