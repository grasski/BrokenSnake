﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    class Game {

        readonly Random randomnummer = new();

        const int windowWidth = 32;
        const int windowHeight = 16;

        enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }

        static void DrawPixel(Pixel pixel)
        {
            Console.SetCursorPosition(pixel.XPos, pixel.YPos);
            Console.ForegroundColor = pixel.Color;
            Console.Write("■");
            Console.SetCursorPosition(0, 0);
        }

        public void StartGame()
        {
            Console.WindowHeight = windowHeight;
            Console.WindowWidth = windowWidth;

            Random randomnummer = new Random();
            int score = 5;
            int gameover = 0;
            Pixel snake = new()
            {
                XPos = windowHeight / 2,
                YPos = windowHeight / 2,
                Color = ConsoleColor.Red
            };

            Pixel berry = new()
            {
                XPos = randomnummer.Next(1, windowWidth - 2),
                YPos = randomnummer.Next(1, windowHeight - 2),
                Color = ConsoleColor.Cyan
            };

            Direction direction = Direction.Right;
            List<int> xposlijf = new List<int>();
            List<int> yposlijf = new List<int>();

            int berryx = randomnummer.Next(windowWidth-2, windowWidth-2);
            int berryy = randomnummer.Next(1, 2);

            DateTime tijd = DateTime.Now;
            DateTime tijd2 = DateTime.Now;
            string buttonpressed = "no";
            while (true)
            {
                Console.Clear();
                if (snake.XPos == windowWidth - 1 || snake.XPos == 0 || snake.YPos == windowHeight - 1 || snake.YPos == 0)
                {
                    gameover = 1;
                }
                for (int i = 0; i < windowWidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }
                for (int i = 0; i < windowWidth; i++)
                {
                    Console.SetCursorPosition(i, windowHeight - 1);
                    Console.Write("■");
                }
                for (int i = 0; i < windowHeight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }
                for (int i = 0; i < windowHeight; i++)
                {
                    Console.SetCursorPosition(windowWidth - 1, i);
                    Console.Write("■");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                if (berryx == snake.XPos && berryy == snake.YPos)
                {
                    score++;
                    berryx = randomnummer.Next(1, windowWidth - 2);
                    berryy = randomnummer.Next(1, windowHeight - 2);
                }
                for (int i = 0; i < xposlijf.Count(); i++)
                {
                    Console.SetCursorPosition(xposlijf[i], yposlijf[i]);
                    Console.Write("■");
                    if (xposlijf[i] == snake.XPos && yposlijf[i] == snake.YPos)
                    {
                        gameover = 1;
                    }
                }
                if (gameover == 1)
                {
                    break;
                }

                Console.SetCursorPosition(snake.XPos, snake.YPos);
                Console.ForegroundColor = snake.Color;
                Console.Write("■");
                Console.SetCursorPosition(berryx, berryy);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                tijd = DateTime.Now;
                buttonpressed = "no";
                while (true)
                {
                    tijd2 = DateTime.Now;
                    if (tijd2.Subtract(tijd).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo toets = Console.ReadKey(true);
                        //Console.WriteLine(toets.Key.ToString());
                        if (toets.Key.Equals(ConsoleKey.UpArrow) && direction != Direction.Down && buttonpressed == "no")
                        {
                            direction = Direction.Up;
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.DownArrow) && direction != Direction.Up && buttonpressed == "no")
                        {
                            direction = Direction.Down;
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.LeftArrow) && direction != Direction.Right && buttonpressed == "no")
                        {
                            direction = Direction.Left;
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.RightArrow) && direction != Direction.Left && buttonpressed == "no")
                        {
                            direction = Direction.Right;
                            buttonpressed = "yes";
                        }
                    }
                }
                xposlijf.Add(snake.XPos);
                yposlijf.Add(snake.YPos);
                switch (direction)
                {
                    case Direction.Up:
                        snake.YPos--;
                        break;
                    case Direction.Down:
                        snake.YPos++;
                        break;
                    case Direction.Left:
                        snake.XPos--;
                        break;
                    case Direction.Right:
                        snake.XPos++;
                        break;
                }
                if (xposlijf.Count() > score)
                {
                    xposlijf.RemoveAt(0);
                    yposlijf.RemoveAt(0);
                }
            }
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2 + 1);
        }
    }

    class Pixel
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public ConsoleColor Color { get; set; }
    }

    class Program
    {
        Random randomnummer = new Random();

        const int windowWidth = 32;
        const int windowHeight = 16;

        void GenerateBerry()
        {
            int berryx = randomnummer.Next(0, windowWidth);
            int berryy = randomnummer.Next(0, windowHeight);

            Console.SetCursorPosition(berryx, berryy);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■");
        }

        

        static void Main(string[] args)
        {
            Game game = new();
            game.StartGame();
        }



        
    }
}
