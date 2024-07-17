using System;
using System.Threading;

namespace ConsolePong
{
    class Program
    {
        // Game variables
        static int ballX = 0, ballY = 0;
        static int ballDirectionX = 1, ballDirectionY = 1;
        static int player1Y = 0, player2Y = 0;
        static int player1Score = 0, player2Score = 0;
        const int windowWidth = 40, windowHeight = 20;
        private const int paddleHeight = 4;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            InitializeGame();

            while (true)
            {
                Draw();
                Input();
                Logic();
                Thread.Sleep(100); // control the game speed
            }
        }

        static void InitializeGame()
        {
            ballX = windowWidth / 2;
            ballY = windowHeight / 2;
            player1Y = windowHeight / 2 - paddleHeight / 2;
            player2Y = windowHeight / 2 - paddleHeight / 2;
        }

        static void Draw()
        {
            Console.Clear();

            // Draw the ball
            Console.SetCursorPosition(ballX, ballY);
            Console.Write("O");

            // Draw the paddles
            for (int i = 0; i < paddleHeight; i++)
            {
                Console.SetCursorPosition(1, player1Y + i);
                Console.Write("|");
                Console.SetCursorPosition(windowWidth - 2, player2Y + i);
                Console.Write("|");
            }

            // Draw the scores
            Console.SetCursorPosition(windowWidth / 2 - 4, 0);
            Console.Write($"{player1Score} - {player2Score}");
        }

        static void Input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.W && player1Y > 0) player1Y--;
                if (keyInfo.Key == ConsoleKey.S && player1Y < windowHeight - paddleHeight) player1Y++;
                if (keyInfo.Key == ConsoleKey.UpArrow && player2Y > 0) player2Y--;
                if (keyInfo.Key == ConsoleKey.DownArrow && player2Y < windowHeight - paddleHeight) player2Y++;
            }
        }

        static void Logic()
        {
            ballX += ballDirectionX;
            ballY += ballDirectionY;

            // Ball collision with top and bottom walls
            if (ballY <= 0 || ballY >= windowHeight - 1)
            {
                ballDirectionY *= -1;
            }

            // Ball collision with paddles
            if (ballX == 2 && ballY >= player1Y && ballY <= player1Y + paddleHeight - 1)
            {
                ballDirectionX *= -1;
            }
            if (ballX == windowWidth - 3 && ballY >= player2Y && ballY <= player2Y + paddleHeight - 1)
            {
                ballDirectionX *= -1;
            }

            // Ball goes out of bounds
            if (ballX <= 0)
            {
                player2Score++;
                InitializeGame();
            }
            else (ballX >= windowWidth - 1)
            {
                player1Score++;
                InitializeGame();
            }
        }
    }
}
