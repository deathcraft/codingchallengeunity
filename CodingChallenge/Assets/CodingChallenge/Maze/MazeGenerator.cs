using UnityEngine;

namespace CodingChallenge.Maze
{
    public class MazeGenerator : MonoBehaviour
    {
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

        private MazeCell[] mazeCells;
        private bool[] visitedCells;

        private MazeCell currentCell;

        void Start()
        {
            CreateMaze();
            OffsetToFitScreen();

            SetVisited(0);
        }
        
        private void SetVisited(int id)
        {
            currentCell = mazeCells[id];
            visitedCells[id] = true;
            currentCell.ChangeColor(Color.blue);
        }

        private void OffsetToFitScreen()
        {
            var transformPosition = transform.position;
            transformPosition.x -= cols / 2f + xOffset * cols + 1;
            transformPosition.y -= rows / 2f + yOffset * rows + 1;

            transform.position = transformPosition;
        }

        private void CreateMaze()
        {
            var numberCells = cols + rows;
            mazeCells = new MazeCell[numberCells];
            visitedCells = new bool[numberCells];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var cellInstance = Instantiate(mazeCellPrefab);
                    var mazeCell = cellInstance.GetComponent<MazeCell>();
                    mazeCells[i + j] = mazeCell;

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