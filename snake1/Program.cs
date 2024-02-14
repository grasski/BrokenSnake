using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using System.Diagnostics;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    public class Game {

        readonly Random randomNumber = new();

        const int windowWidth = 32;
        const int windowHeight = 16;
        int score = 5;

        Snake Snake { get; set; }
        Pixel Berry { get; set; }

        public Game()
        {
            randomNumber = new();
            Snake = new()
            {
                XPos = windowHeight / 2,
                YPos = windowHeight / 2,
                Color = ConsoleColor.Red,

                Tail = new List<Pixel>()
            };

            Berry = new()
            {
                XPos = randomNumber.Next(1, windowWidth - 2),
                YPos = randomNumber.Next(1, windowHeight - 2),
                Color = ConsoleColor.Cyan
            };
        }


        enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }

        void DrawPixel(Pixel pixel)
        {
            Console.SetCursorPosition(pixel.XPos, pixel.YPos);
            Console.ForegroundColor = pixel.Color;
            Console.Write("■");
            Console.SetCursorPosition(0, 0);
        }
        void DrawBorder()
        {
            for (int i = 0; i < windowWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");

                Console.SetCursorPosition(i, windowHeight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < windowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");

                Console.SetCursorPosition(windowWidth - 1, i);
                Console.Write("■");
            }
        }
        bool CheckGameOver(Snake snake)
        {
            if (snake.XPos == windowWidth - 1 || snake.XPos == 0 || snake.YPos == windowHeight - 1 || snake.YPos == 0)
            {
                return true;
            }
            for (int i=0; i < snake.Tail.Count; i++)
            {
                if (snake.Tail[i].XPos == snake.XPos && snake.Tail[i].YPos == snake.YPos)
                {
                    return true;
                }
            }

            return false;
        }
        Direction ReadDirection(Direction direction)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && direction != Direction.Down)
                {
                    direction = Direction.Up;
                }
                else if (key == ConsoleKey.DownArrow && direction != Direction.Up)
                {
                    direction = Direction.Down;
                }
                else if (key == ConsoleKey.LeftArrow && direction != Direction.Right)
                {
                    direction = Direction.Left;
                }
                else if (key == ConsoleKey.RightArrow && direction != Direction.Left)
                {
                    direction = Direction.Right;
                }
            }

            return direction;
        }

        void HandleBerryCollision()
        {
            if (Berry.XPos == Snake.XPos && Berry.YPos == Snake.YPos)
            {
                score++;
                Berry.XPos = randomNumber.Next(1, windowWidth - 2);
                Berry.YPos = randomNumber.Next(1, windowHeight - 2);
            }
        }

        public void StartGame()
        {
            Console.WindowHeight = windowHeight;
            Console.WindowWidth = windowWidth;

            Direction direction = Direction.Right;
            while (true)
            {
                Console.Clear();
                if (CheckGameOver(Snake))
                {
                    break;
                }
                DrawBorder();

                HandleBerryCollision();


                DrawPixel(Berry);   // Draw berry
                DrawPixel(Snake);   // Draw snake head
                for (int i = 0; i < Snake.Tail.Count; i++)
                {
                    DrawPixel(Snake.Tail[i]);   // Draw snake tail
                }

                
                Stopwatch stopwatch = Stopwatch.StartNew();
                while(stopwatch.ElapsedMilliseconds <= 500)
                {
                    direction = ReadDirection(direction);
                }

                Snake.Tail.Add(new()
                {
                    XPos = Snake.XPos,
                    YPos = Snake.YPos,
                    Color = ConsoleColor.Green
                });
                if (Snake.Tail.Count > score)
                {
                    Snake.Tail.RemoveAt(0);
                }

                switch (direction)
                {
                    case Direction.Up:
                        Snake.YPos--;
                        break;
                    case Direction.Down:
                        Snake.YPos++;
                        break;
                    case Direction.Left:
                        Snake.XPos--;
                        break;
                    case Direction.Right:
                        Snake.XPos++;
                        break;
                }
            }
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2 + 1);
        }


    }
    class Snake: Pixel
    {
        public required List<Pixel> Tail { get; set; }
    }
    class Pixel
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public ConsoleColor Color { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Game game = new();
            game.StartGame();
        }        
    }
}
