using GameSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace EscapeMines
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isGameRunning = false;
            Simulation game = null;
            Enums.Result result = Enums.Result.Continue;

            string welcome = "Welcome to the game Escape Mines. You have to help the hero in this case a turtle escape the mine field. The turtle is represented by an arrow pointing in whichever direction it is currently going to move forward. You can rotate the direction left or right using the left and right arrows on your keyboard. You can move by pressing the letter 'm'. Your goal is to reach the exit which is represented by the letter 'e', careful not to hit the mines though. The mines are represented by an 'x'. To start the game you need to configure the difficulty of the game by entering height and width of the mine field. When you are ready, please press the spacebar. Enjoy!";

            Console.Write(WordWrap(welcome));

            Console.Write("Height: ");
            string height = Console.ReadLine();
            int validatedHeight = 0;

            ValidateUserInput(ref height, ref validatedHeight);

            Console.Write("Length: ");
            string length = Console.ReadLine();
            int validatedLength = 0;

            ValidateUserInput(ref length, ref validatedLength);

            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.M:
                        result = MoveTurtle(isGameRunning, ref game);
                        CheckGame(ref isGameRunning, ref game, result);
                        break;

                    case ConsoleKey.RightArrow:
                        RotateTurtle(isGameRunning, ref game, false);
                        break;

                    case ConsoleKey.LeftArrow:
                        RotateTurtle(isGameRunning, ref game, true);
                        break;

                    case ConsoleKey.Spacebar:
                        StartGame(validatedLength, validatedHeight, ref isGameRunning, ref game);
                        break;
                }
            }
        }

        /// <summary>
        /// Wraps a string into paragraph.
        /// </summary>
        /// <param name="paragraph">String to wrap.</param>
        private static string WordWrap(string paragraph)
        {
            paragraph = new Regex(@" {2,}").Replace(paragraph.Trim(), @" ");
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            List<string> lines = new List<string>();

            for (var i = 0; paragraph.Length > 0; i++)
            {
                lines.Add(paragraph.Substring(0, Math.Min(Console.WindowWidth, paragraph.Length)));
                int length = lines[i].LastIndexOf(" ", StringComparison.Ordinal);
                if (length > 0) lines[i] = lines[i].Remove(length);
                paragraph = paragraph.Substring(Math.Min(lines[i].Length + 1, paragraph.Length));
                Console.SetCursorPosition(left, top + i); Console.WriteLine(lines[i]);
            }

            return paragraph;
        }

        /// <summary>
        /// Validates user input.
        /// </summary>
        /// <param name="value">User input.</param>
        /// <param name="validatedValue">Validated user input.</param>
        private static void ValidateUserInput(ref string value, ref int validatedValue)
        {
            while (!int.TryParse(value, out validatedValue) || validatedValue < 9 || validatedValue > 50)
            {
                Console.WriteLine("Please Enter a whole number!");
                Console.WriteLine("Please Enter a bigger number than 10! And don't get over 50");
                value = Console.ReadLine();
            }
        }

        /// <summary>
        /// Start game.
        /// </summary>
        /// <param name="length">Width of the mine field.</param>
        /// <param name="height">Height of the mine field.</param>
        /// <param name="isGameRunning">Flag if game instance created.</param>
        /// <param name="game">Instance of the game, if existing.</param>
        private static void StartGame(int length, int height, ref bool isGameRunning, ref Simulation game)
        {
            if(!isGameRunning)
            {
                Console.Clear();
                game = new Simulation(length, height);
                isGameRunning = true;
            }
        }

        /// <summary>
        /// Rotate direction of turtle.
        /// </summary>
        /// <param name="isGameRunning">Flag if game instance created.</param>
        /// <param name="game">Instance of the game, if existing.</param>
        /// <param name="left">Flag if the direction of the rotaion is left.</param>
        private static void RotateTurtle(bool isGameRunning, ref Simulation game, bool left)
        {
            if (isGameRunning)
            {
                Console.Clear();
                game.RotateTurtle(left);
            }
        }

        /// <summary>
        /// Move turtle.
        /// </summary>
        /// <param name="isGameRunning">Flag if game instance created.</param>
        /// <param name="game">Instance of the game, if existing.</param>
        private static Enums.Result MoveTurtle(bool isGameRunning, ref Simulation game)
        {
            Enums.Result result = Enums.Result.Continue;

            if (isGameRunning)
            {
                Console.Clear();
                result = game.MoveTurtle();
            }

            return result;
        }

        /// <summary>
        /// Check if turtle has moved succesfully without hitting a mine or if it has reached the goal.
        /// </summary>
        /// <param name="isGameRunning">Flag if game instance created.</param>
        /// <param name="game">Instance of the game, if existing.</param>
        /// <param name="result">Result with the outcome of the game.</param>
        private static void CheckGame(ref bool isGameRunning, ref Simulation game, Enums.Result result)
        {
            if(isGameRunning && result.Equals(Enums.Result.Win))
            {
                Console.Clear();
                Console.WriteLine("Congratulations! You have done it!");
                isGameRunning = false;
            }
            else if(isGameRunning && result.Equals(Enums.Result.Loss))
            {
                Console.Clear();
                Console.WriteLine("You have lost :( Better luck next time.");
                isGameRunning = false;
            }
        }
    }
}
