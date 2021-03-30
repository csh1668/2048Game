using System;
using System.Text;

namespace _2048Game
{
    public class GameBoard
    {
        /*
         * TODO
         * 게임 보드에는 무엇이 필요한가?
         * 게임판을 초기화하는 생성자
         * 2048 게임의 게임판을 저장하는 필드
         * 입력이 들어오면 보드를 움직이는 정적 메소드
         * 게임판에 무작위로 Unit을 생성하는 정적 메소드
         */

        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// 2의 지수를 저장함. 예) 2 => 1, 8 => 3, 없음 => 0,
        /// x, y의 형식으로 저장
        /// </summary>
        public int[,] Units { get; set; }

        public GameBoard(int width, int height)
        {
            Width = width;
            Height = height;

            Units = InsertUnitToRandomEmptyPos(new int[width, height], width, height, out _, out _, out _);
        }

        public GameBoard(int width, int height, int[,] units)
        {
            Width = width;
            Height = height;

            Units = units;
        }

        public static int[,] InsertUnitToRandomEmptyPos(int[,] units, int width, int height, out int x, out int y, out bool isFulled)
        {
            Random r = new Random();

            isFulled = true;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (units[i, j] == 0)
                    {
                        isFulled = false;
                        break;
                    }
                }
            }

            if (isFulled)
            {
                x = -1;
                y = -1;
                return units;
            }

            // TODO: 빈 좌표 찾기 알고리즘 개선
            while (true)
            {
                int rX = r.Next(0, width), rY = r.Next(0, height);
                if (units[rX, rY] == 0)
                {
                    x = rX;
                    y = rY;
                    break;
                }
            }

            double twoOrFour = r.NextDouble();
            if (twoOrFour <= 0.75) units[x, y] = 1;
            else units[x, y] = 2;

            return units;
        }

        // input => 0 = 위쪽, 1 = 아래쪽, 2 = 왼쪽, 3 = 오른쪽
        public static int[,] MoveBoard(int[,] units, int width, int height, int input, out bool isFulled, out int addedScore)
        {
            isFulled = false;
            addedScore = 0;
            // input이 0일 경우만
            if (input == 0)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = height - 1; j >= 1; j--)
                    {
                        if (units[i, j] == 0)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (units[i, k] != 0)
                                {
                                    units[i, j] = units[i, k];
                                    units[i, k] = 0;
                                    break;
                                }
                            }
                        }

                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (units[i, j] == units[i, k] && units[i, k] != 0)
                            {
                                units[i, j]++;
                                units[i, k] = 0;
                                addedScore += (int)Math.Pow(2, units[i, j]);
                                break;
                            }
                            if (units[i, k] != 0)
                                break;
                        }
                    }
                }
            }
            else if (input == 1)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height - 1; j++)
                    {
                        if (units[i, j] == 0)
                        {
                            for (int k = j + 1; k < height; k++)
                            {
                                if (units[i, k] != 0)
                                {
                                    units[i, j] = units[i, k];
                                    units[i, k] = 0;
                                    break;
                                }
                            }
                        }

                        for (int k = j + 1; k < height; k++)
                        {
                            if (units[i, j] == units[i, k] && units[i, k] != 0)
                            {
                                units[i, j]++;
                                units[i, k] = 0;
                                addedScore += (int)Math.Pow(2, units[i, j]);
                                break;
                            }

                            if (units[i, k] != 0)
                                break;
                        }
                    }
                }
            }
            else if (input == 2)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width - 1; j++)
                    {
                        if (units[j, i] == 0)
                        {
                            for (int k = j + 1; k < width; k++)
                            {
                                if (units[k, i] != 0)
                                {
                                    units[j, i] = units[k, i];
                                    units[k, i] = 0;
                                    break;
                                }
                            }
                        }

                        for (int k = j + 1; k < width; k++)
                        {
                            if (units[j, i] == units[k, i] && units[k, i] != 0)
                            {
                                units[j, i]++;
                                units[k, i] = 0;
                                addedScore += (int)Math.Pow(2, units[j, i]);
                                break;
                            }
                            if (units[k, i] != 0)
                                break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = width - 1; j >= 1; j--)
                    {
                        if (units[j, i] == 0)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (units[k, i] != 0)
                                {
                                    units[j, i] = units[k, i];
                                    units[k, i] = 0;
                                    addedScore += (int)Math.Pow(2, units[j, i]);
                                    break;
                                }
                            }
                        }

                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (units[j, i] == units[k, i] && units[k, i] != 0)
                            {
                                units[j, i]++;
                                units[k, i] = 0;
                                break;
                            }
                            if (units[k, i] != 0)
                                break;
                        }
                    }
                }
            }

            units = InsertUnitToRandomEmptyPos(units, width, height, out int _, out int _, out isFulled);

            return units;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int j = Height - 1; j >= 0; j--)
            {
                for (int i = 0; i < Width; i++)
                {
                    sb.Append($"{((Units[i, j] == 0) ? 0 : (int)Math.Pow(2, Units[i, j])),5}, ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}