using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CellularAutomata
{
    class Program
    {
        static int WIDTH = 20;
        static int INIT_CELL_COUNT = 300;
        static int SLEEP_INTERVAL = 100;
        static int[,] CELLS = new int[WIDTH, WIDTH];

        static void Main(string[] args)
        {
            Init(INIT_CELL_COUNT);
            Draw();

            while (true)
            {
                Thread.Sleep(SLEEP_INTERVAL);
                Step();
                Draw();
            }
        }

        static void Draw()
        {
            Console.Clear();
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                    Console.Write(CELLS[i, j] == 1 ? "*" : " ");

                Console.WriteLine();
            }
        }

        static void Init(int initCount)
        {
            List<int[]> aliveCells = new List<int[]>();
            for (int i = 0; i < initCount; i++)
                aliveCells.Add(new int[] { new Random().Next(0, WIDTH), new Random().Next(0, WIDTH) });

            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < WIDTH; j++)
                    CELLS[i, j] = 0;

            aliveCells.ForEach(cell => CELLS[cell[0], cell[1]] = 1);
        }

        static int GetALiveCount(int i, int j)
        {
            int liveCount = 0;
            if (i > 0) liveCount += CELLS[i - 1, j];
            if (i + 1 < WIDTH) liveCount += CELLS[i + 1, j];
            if (j > 0) liveCount += CELLS[i, j - 1];
            if (j + 1 < WIDTH) liveCount += CELLS[i, j + 1];

            if (i > 0 && j > 0) liveCount += CELLS[i - 1, j - 1];
            if (i + 1 < WIDTH && j + 1 < WIDTH) liveCount += CELLS[i + 1, j + 1];
            if (i + 1 < WIDTH && j > 0) liveCount += CELLS[i + 1, j - 1];
            if (i > 0 && j + 1 < WIDTH) liveCount += CELLS[i - 1, j + 1];

            return liveCount;
        }

        static void Step()
        {
            int[,] newCells = new int[WIDTH, WIDTH];

            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < WIDTH; j++)
                {
                    int count = GetALiveCount(i, j);
                    if (CELLS[i, j] == 1)
                    {
                        if (count >= 2 && count <= 3) newCells[i, j] = 1;
                        if (count > 3 || count < 2) newCells[i, j] = 0;
                    }
                    else
                    {
                        if (count < 3) newCells[i, j] = 0;
                        if (count == 3) newCells[i, j] = 1;
                    }
                }

            CELLS = newCells;
        }

    }
}
