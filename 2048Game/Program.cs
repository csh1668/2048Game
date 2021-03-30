using System;

namespace _2048Game
{
    internal class Program
    {
        // 테스트용
        private static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Title = "2048 게임";
            Console.SetWindowSize(32, 10);

            GameBoard gb = new GameBoard(4, 4);
            bool isGameOver = false;
            int score = 0;

            Console.WriteLine("=====2048 게임=====");
            Console.WriteLine("조작법: w, a, s, d\n");

            while (isGameOver == false)
            {
                Console.Write(gb.ToString());
                char input = Console.ReadKey().KeyChar;
                int i = -1;
                switch (input)
                {
                    case 'w':
                        i = 0;
                        break;

                    case 's':
                        i = 1;
                        break;

                    case 'a':
                        i = 2;
                        break;

                    case 'd':
                        i = 3;
                        break;
                }

                if (i == -1) break;
                gb.Units = GameBoard.MoveBoard(gb.Units, gb.Width, gb.Height, i, out isGameOver, out int tmpScore);
                score += tmpScore;
                Console.WriteLine($"{$"현재 점수: {score,5}",20}\n{" ",40}\n{" ",40}\n{" ",40}\n{" ",40}");
                Console.SetCursorPosition(0, 0);
            }
            Console.Write("게임 오버");

            Console.ReadLine();
        }
    }
}