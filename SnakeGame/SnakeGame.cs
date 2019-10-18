using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TUI.Base.Style;

namespace SnakeGame
{
    public class SnakeGame
    {
        public List<Point> points;
        //public SnakeAppearence appearence;
        //public ArenaConfiguration arena;
        public int fieldSize;
        public Direction movementDirection;
        public Action<bool> endCallback;
        public Point food;
        public Random rnd = new Random();

        public SnakeGame(int fieldSize, Action<bool> endCallback = null)
        {
            this.fieldSize = fieldSize;
            this.endCallback = endCallback;
            points = new List<Point> { new Point(0, 0), new Point(1, 0) };
            movementDirection = Direction.Right;

            food = points[0];
            NewFood();
        }

        public bool Move()
        {
            Point next = GetNextPoint();
            if (points.Any(p => next.X == p.X && next.Y == p.Y))
            {
                endCallback?.Invoke(false);
                return false;
            }
            if (next == food)
                NewFood();
            else
                points.Remove(points[0]);
            points.Add(next);

            return true;
        }

        public void NewFood()
        {
            do
                food = new Point(rnd.Next(0, fieldSize), rnd.Next(0, fieldSize));
            while (points.Any(p => food.X == p.X && food.Y == p.Y));
        }

        public Point GetNextPoint()
        {
            Point p = points[points.Count - 1];
            switch (movementDirection)
            {
                case Direction.Down:
                    p.Y += 1;
                    break;
                case Direction.Right:
                    p.X += 1;
                    break;
                case Direction.Left:
                    p.X -= 1;
                    break;
                case Direction.Up:
                    p.Y -= 1;
                    break;
            }
            p.X = (p.X + fieldSize) % fieldSize;
            p.Y = (p.Y + fieldSize) % fieldSize;
            return p;
        }
    }

    class ArenaConfiguration
    {
        
    }

    class SnakeAppearence
    {
    }
}
