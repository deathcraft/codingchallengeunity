using System;

namespace CodingChallenge.Maze
{
    public static class MazeUtil
    {
        public static int LeftCellId(int id, int rows, int cols)
        {
            if (id % cols == 0)
            {
                return -1;
            }
            
            var leftCellId = id - 1;
            return leftCellId;
        }
        
        public static int RightCellId(int id, int rows, int cols)
        {
            if (id % cols == cols - 1 || id + 1 >= rows * cols)
            {
                return -1;
            }
            
            var cellId = id + 1;
            return cellId;
        }
        
        public static int TopCellId(int id, int rows, int cols)
        {
            var cellId = id + cols;

            if (cellId >= rows * cols)
            {
                return -1;
            }
            
            return cellId;
        }
        
        public static int BottomCellId(int id, int rows, int cols)
        {
            var cellId = id - cols;

            if (cellId < 0)
            {
                return -1;
            }
            
            return cellId;
        }

        public static int GetRowId(int id, int cols)
        {
            return (int) Math.Floor((double) (id / cols));
        }
        
        public static int GetColId(int id, int cols)
        {
            return id % cols;
        }

    }
}