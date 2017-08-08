using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverProblem
{
    class Program
    {

        private static string[] ways = { "N", "E", "S", "W" };


        public class MarsRover
        {

            public class Position
            {
                public int x { get; set; }
                public int y { get; set; }
                public int direction { get; set; }


                public Position()
                { }
                public Position(int _x, int _y, string _direction)
                {
                    this.x = _x;
                    this.y = _y;
                    this.direction = findPosition(_direction);
                }
            }
            private int limitOfX, limitOfY;

            Position pos = null;
            public MarsRover()
            {
                pos = new Position();
            }

            public static int findPosition(string direction)
            {
                return Array.IndexOf(ways, direction);
            }

            public void setPosition(int _x, int _y, string _direction)
            {
                pos.x = _x;
                pos.y = _y;
                pos.direction = findPosition(_direction);
            }

            private void setNewPosition(int _x, int _y)
            {
                if ((this.limitOfX < +pos.x + _x) || (this.limitOfY < +pos.y + _y))
                    throw new Exception("Out of Limit!");

                pos.x += _x;
                pos.y += _y;
            }

            public void setLimit(int _limitOfX, int _limitOfY)
            {
                this.limitOfX = _limitOfX;
                this.limitOfY = _limitOfY;
            }

            private void move()
            {
                if (pos.direction == Array.IndexOf(ways, "N"))
                {
                    this.setNewPosition(0, 1);
                }
                else if (pos.direction == Array.IndexOf(ways, "E"))
                {
                    this.setNewPosition(1, 0);
                }
                else if (pos.direction == Array.IndexOf(ways, "S"))
                {
                    this.setNewPosition(0, -1);
                }
                else if (pos.direction == Array.IndexOf(ways, "W"))
                {
                    this.setNewPosition(-1, 0);
                }
            }

            public void printPosition()
            {
                Console.WriteLine(pos.x + " " + pos.y + " " + ways[pos.direction]);
            }

            private void rotate(char position)
            {
                if (position == 'L')
                {
                    if (pos.direction > 0)
                        pos.direction -= 1;
                    else
                        pos.direction = ways.Length - 1;
                }
                else if (position == 'R')
                {
                    if (pos.direction < ways.Length)
                        pos.direction += 1;
                    else
                        pos.direction = 0;
                }
            }

            public void execute(string movements, bool throwEx = false)
            {
                foreach (var move in movements)
                {
                    if (move == 'L' || move == 'R')
                        this.rotate(move);
                    else if (move == 'M')
                        this.move();
                    else
                    {
                        if (!throwEx)
                            Console.WriteLine("'" + move + "' is wrong comment. You can only use 'L', 'R', 'M' characters!");
                        else
                            throw new Exception("Unknown Command!");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            MarsRover rover = new MarsRover();
            //rover.setLimit(5, 5);
            //rover.setPosition(1, 2, "N");
            //rover.execute("LMLMLMLMM");
            //rover.printPosition();
            int x = 0, y = 0, xOfStart = 0, yOfStart;

            #region Limits of Plateau
            Console.WriteLine("Enter x value of the limits of plateau: ");
            string _x = Console.ReadLine();
            if (!Int32.TryParse(_x, out x))
                Console.WriteLine("Use only numbers!");

            Console.WriteLine("Enter y value of the limits of plateau: ");
            string _y = Console.ReadLine();
            if (!Int32.TryParse(_y, out y))
                Console.WriteLine("Use only numbers!");

            #endregion Limits of Plateau

            rover.setLimit(x, y);

            #region Get Positions of Start
            Console.Write("Enter x value of start position: ");
            string _xOfStart = Console.ReadLine();
            if (!Int32.TryParse(_xOfStart, out xOfStart))
                Console.WriteLine("Use only numbers!");

            Console.Write("Enter y value of start position: ");
            string _yOfStart = Console.ReadLine();
            if (!Int32.TryParse(_yOfStart, out yOfStart))
                Console.WriteLine("Use only numbers!");
            if (xOfStart > x || yOfStart > y)
            {
                Console.WriteLine("Rover's start position is out of limits!");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter direction of start position(Only 'N', 'E', 'S', 'W'): ");
            string direction = Console.ReadLine();

            if (string.IsNullOrEmpty(direction) || ways.Where(p => p.Equals(direction.ToUpper())).Count() == 0)
            {
                Console.WriteLine("Rover's start direction is invalid.(Only 'N', 'E', 'S', 'W') !");
                Console.ReadLine();
                return;
            }



            #endregion Get Positions of Start

            rover.setPosition(xOfStart, yOfStart, direction.ToUpper());

            #region Movements
            Console.WriteLine("Enter the rover's movements: ");
            string movements = Console.ReadLine();

            #endregion Movements

            if (!string.IsNullOrEmpty(movements))
                rover.execute(movements.ToUpper());
            else
                Console.WriteLine("You can only use 'L', 'R', 'M' characters!");

            rover.printPosition();


            Console.ReadLine();
        }
    }
}
