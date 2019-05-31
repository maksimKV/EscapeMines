using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace GameSimulation
{
    public class Simulation
    {
        private int numRows;
        private int numColumns;

        private List<Point> mines = new List<Point>();

        private Point exit;
        private Point turtlePosition;

        private string turtleDirectionArrow = ">";
        private Enums.Directions turtleDirection = Enums.Directions.East;

        /// <summary>
        /// Initializes a new game.
        /// </summary>
        /// <param name="rows">Length of the field.</param>
        /// <param name="height">Height of the field.</param>
        public Simulation(int rows, int columns)
        {
            numRows = rows;
            numColumns = columns;

            GenerateField();
            DrawGame();

            Console.CursorVisible = false;
        }

        /// <summary>
        /// Moves turtle checks if mine or exit hit.
        /// </summary>
        public Enums.Result MoveTurtle()
        {
            Enums.Result result = Enums.Result.Continue;

            if(turtleDirection.Equals(Enums.Directions.North) && turtlePosition.y > 0)
            {
                turtlePosition = new Point(turtlePosition.x, turtlePosition.y - 1);
            }
            else if(turtleDirection.Equals(Enums.Directions.West) && turtlePosition.x > 0)
            {
                turtlePosition = new Point(turtlePosition.x - 1, turtlePosition.y);
            }
            else if(turtleDirection.Equals(Enums.Directions.South) && turtlePosition.y < numColumns - 1)
            {
                turtlePosition = new Point(turtlePosition.x, turtlePosition.y + 1);
            }
            else if (turtleDirection.Equals(Enums.Directions.East) && turtlePosition.x < numRows - 1)
            {
                turtlePosition = new Point(turtlePosition.x + 1, turtlePosition.y);
            }

            DrawGame();

            foreach (Point mine in mines)
            {
                if(turtlePosition.Equals(mine))
                {
                    result = Enums.Result.Loss;
                }
            }

            if(turtlePosition.Equals(exit))
            {
                result = Enums.Result.Win;
            }

            return result;
        }

        /// <summary>
        /// Rotates turtle.
        /// </summary>
        /// <param name="left">Boolean used for rotating the turtle left or right.</param>
        public void RotateTurtle(bool left)
        {
            if(left)
            {
                turtleDirection = GetPreviousDirection(turtleDirection);
            }
            else
            {
                turtleDirection = GetNextDirection(turtleDirection);
            }

            SetTurtleDirectionArrow();
            DrawGame();
        }

        /// <summary>
        /// Changes the direction arrow for the turtle based on the current direction.
        /// </summary>
        private void SetTurtleDirectionArrow()
        {
            if(turtleDirection.Equals(Enums.Directions.North))
            {
                turtleDirectionArrow = "^";
            }
            else if(turtleDirection.Equals(Enums.Directions.West))
            {
                turtleDirectionArrow = "<";
            }
            else if(turtleDirection.Equals(Enums.Directions.East))
            {
                turtleDirectionArrow = ">";
            }
            else if(turtleDirection.Equals(Enums.Directions.South))
            {
                turtleDirectionArrow = "v";
            }
        }

        /// <summary>
        /// Gets next direction.
        /// </summary>
        /// <param name="direction">Current direction.</param>
        private Enums.Directions GetNextDirection(Enums.Directions direction)
        {
            switch (direction)
            {
                case Enums.Directions.North:
                    return Enums.Directions.East;
                case Enums.Directions.East:
                    return Enums.Directions.South;
                case Enums.Directions.South:
                    return Enums.Directions.West;
                case Enums.Directions.West:
                    return Enums.Directions.North;
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets previous direction.
        /// </summary>
        /// <param name="direction">Current direction.</param>
        private Enums.Directions GetPreviousDirection(Enums.Directions direction)
        {
            switch (direction)
            {
                case Enums.Directions.North:
                    return Enums.Directions.West;
                case Enums.Directions.East:
                    return Enums.Directions.North;
                case Enums.Directions.South:
                    return Enums.Directions.East;
                case Enums.Directions.West:
                    return Enums.Directions.South;
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Draws or re-draws the game.
        /// </summary>
        private void DrawGame()
        {
            for (int i = 0; i < numColumns; i++)
            {
                for (int n = 0; n < numRows; n++)
                {
                    Point currentPoint = new Point(n, i);

                    if (mines.Contains(currentPoint))
                    {
                        Console.Write("x");

                        if (n == numRows - 1)
                        {
                            Console.WriteLine("\r");
                        }
                    }
                    else if (turtlePosition.Equals(currentPoint))
                    {
                        Console.Write(turtleDirectionArrow);

                        if (n == numRows - 1)
                        {
                            Console.WriteLine("\r");
                        }
                    }
                    else if (exit.Equals(currentPoint))
                    {
                        Console.Write("e");

                        if (n == numRows - 1)
                        {
                            Console.WriteLine("\r");
                        }
                    }
                    else if (n == numRows - 1)
                    {
                        Console.WriteLine("\r");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
        }

        /// <summary>
        /// Generates the mine field as well as the starting point for the turtle and the exit point for the game.
        /// </summary>
        private void GenerateField()
        {
            GenerateMines();
            turtlePosition = GeneratePoint(0, 0);

            int exitPosition = numRows - 1;
            exit = GeneratePoint(exitPosition, exitPosition);
        }

        /// <summary>
        /// Generates the mines.
        /// </summary>
        private void GenerateMines()
        {
            int numMines = (int)((numRows + numColumns) / 5);

            do
            {
                Point mine = GeneratePoint();
                mines.Add(mine);
                numMines--;

            } while (numMines > 0);
        }

        /// <summary>
        /// Generates a point on the map without overlapping the existing mines.
        /// </summary>
        private Point GeneratePoint()
        {
            Random random = new Random();

            while(true)
            {
                int x = random.Next(0, numRows);
                int y = random.Next(0, numColumns);

                Point point = new Point(x, y);

                if (!mines.Contains(point))
                {
                    return point;
                }
            }
        }

        /// <summary>
        /// Generates a point on the map without overlapping the existing mines.
        /// </summary>
        /// <param name="positionLeft">Left position for the point generation.</param>
        /// <param name="positionRight">Right position for the point generation.</param>
        private Point GeneratePoint(int positionLeft, int positionRight)
        {
            Random random = new Random();
            int x = random.Next(positionLeft, positionRight);
            int y = random.Next(0, numColumns);

            Point point = new Point(x, y);

            if (mines.Contains(point))
            {
                return GeneratePoint();
            }
            else
            {
                return point;
            }
        }
    }
}
