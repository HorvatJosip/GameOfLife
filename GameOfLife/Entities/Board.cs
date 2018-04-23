using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    class Board
    {
        #region Fields

        #region Constants

        private const int MinRows = 10;
        private const int MinCols = 10;

        private const char AliveChar = '*';
        private const char DeadChar = '.';

        private const ConsoleColor AliveColor = ConsoleColor.Green;
        private const ConsoleColor DeadColor = ConsoleColor.DarkGray;

        #endregion

        private bool[][] board;
        private int rows, cols, cellRarity;
        private Random rng;

        #endregion

        #region Constructor

        public Board(int rows, int cols, int cellRarity = 7)
        {
            if (rows < MinRows || cols < MinCols || cellRarity < 2)
                throw new ArgumentException($"Min rows: {MinRows}, min cols: {MinCols}, min cell rarity: 2.");

            this.rows = rows;
            this.cols = cols;
            this.cellRarity = cellRarity;

            rng = new Random();

            InitializeBoard(out board);
            SpawnCells();
        }

        #endregion

        #region Methods

        private void SpawnCells()
        {
            for (int i = 0; i < board.Length; i++)
                for (int j = 0; j < board[i].Length; j++)
                    board[i][j] = rng.Next(cellRarity) == 0;
        }

        public void Print()
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (GetCharAt(i, j))
                        Utils.PrintInColor(AliveChar, AliveColor);
                    else
                        Utils.PrintInColor(DeadChar, DeadColor);
                }
                Console.WriteLine();
            }
        }

        public void NextGeneration()
        {
            InitializeBoard(out bool[][] currentGeneration);

            for (int i = 0; i < board.Length; i++)
                for (int j = 0; j < board[i].Length; j++)
                    currentGeneration[i][j] = board[i][j];

            for(int i = 0; i < board.Length; i++)
                for(int j = 0; j < board[i].Length; j++)
                {
                    var cell = GetCharAt(i, j);
                    var neighbours = GetNeighbours(i, j);
                    var numLiveNeighbours = neighbours.Where(neighbour => neighbour).Count();

                    if (cell) //is alive
                    {
                        if (numLiveNeighbours < 2 || numLiveNeighbours > 3)
                            SetCharAt(i, j, false);
                    }
                    else //if cell isn't alive
                    {
                        if (numLiveNeighbours == 3)
                            SetCharAt(i, j, true);
                    }
                }

            bool equal = true;

            for(int i = 0; i < board.Length; i++)
                for(int j = 0; j < board[i].Length; j++)
                    if(board[i][j] != currentGeneration[i][j])
                    {
                        equal = false;
                        break;
                    }

            if (equal)
                SpawnCells();
        }

        #region Helpers

        private void InitializeBoard(out bool[][] board)
        {
            board = new bool[rows][];

            for (int i = 0; i < rows; i++)
                board[i] = new bool[cols];
        }

        private List<bool> GetNeighbours(int row, int col)
        {
            var neighbours = new List<bool>();

            for (int i = col - 1; i <= col + 1; i++)
                for (int j = row - 1; j <= row + 1; j++)
                    if (!(i == col && j == row) && IndexValid(j, i))
                        neighbours.Add(GetCharAt(j, i));

            return neighbours;
        }

        private bool IndexValid(int row, int col) =>
            row >= 0 && row < board.Length &&
            col >= 0 && col < board[0].Length;

        private bool GetCharAt(int row, int col) => board[row][col];
        private void SetCharAt(int row, int col, bool alive) => board[row][col] = alive;

        #endregion

        #endregion Methods
    }
}
