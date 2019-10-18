using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Terraria.ID;
using TShockAPI;
using TUI.Base;
using TUI.Base.Style;
using TUI.Widgets;

namespace SnakeGame
{
    public class SnakeGamePanel : Panel
    {
        public SnakeGame game;
        public Timer timer;

        public SnakeGamePanel(int size)
            : base("SnakeGame", 0, 0, size * 2 + 2, size * 2 + 2, new UIConfiguration()
            {
                UseBegin = true,
                UseEnd = true
            }, new ContainerStyle()
            {
                Wall = WallID.DiamondGemspark,
                WallColor = PaintID.Black
            })
        {
            game = new SnakeGame(size, OnEnd);
            timer = new Timer(500);
            timer.Elapsed += OnTimer;
            SetupGrid(Enumerable.Repeat(new Absolute(2), size), Enumerable.Repeat(new Absolute(2), size),
                new Indent()
                {
                    Left = 1,
                    Up = 1,
                    Right = 1,
                    Down = 1
                });

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    this[x, y].Style = new ContainerStyle()
                    {
                        Wall = (x + y) % 2 == 0 ? WallID.AmethystGemspark : WallID.RubyGemspark,
                        WallColor = PaintID.Gray
                    };
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            if (!game.Move())
                timer.Stop();
            Update().Apply().Draw();
        }

        protected override void UpdateThisNative()
        {
            base.UpdateThisNative();

            for (int x = 0; x < game.fieldSize; x++)
                for (int y = 0; y < game.fieldSize; y++)
                    this[x, y].Style.WallColor = game.points.Any(p => p.X == x && p.Y == y)
                        ? PaintID.DeepBlue
                        : game.food.X == x && game.food.Y == y
                            ? PaintID.DeepRed
                            : PaintID.Gray;
        }

        public override void Invoke(Touch touch)
        {
            if (touch.State == TouchState.End)
            {
                Touch begin = touch.Session.BeginTouch;
                if (touch.X == begin.X && touch.Y == begin.Y)
                {
                    if (timer.Enabled)
                        timer.Stop();
                    else
                        timer.Start();
                }

                if (touch.X > begin.X)
                    game.movementDirection = Direction.Right;
                else if (touch.X < begin.X)
                    game.movementDirection = Direction.Left;
                else if (touch.Y > begin.Y)
                    game.movementDirection = Direction.Down;
                else if (touch.Y < begin.Y)
                    game.movementDirection = Direction.Up;
            }
        }

        public void OnEnd(bool win)
        {
            if (win)
                TSPlayer.All.SendSuccessMessage("Snake win!");
            else
                TSPlayer.All.SendErrorMessage("Snake lose!");
        }
    }
}
