using System;
using System.Linq;
using System.Reflection; // ha.
using System.Collections.Generic;
using TerrariaApi.Server;
using TShockAPI;
using Terraria;
using TUI;
using SnakeGame;

[ApiVersion(2, 1)]
public class Plugin : TerrariaPlugin
{

    public override string Author => "Interlocked & ASgo";
    public override string Name => "SnakeGamePlugin";
    public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;
    public override string Description => "A simple snake game using TUI";

    public override void Initialize()
    {
        TUI.TUI.Create(new SnakeGamePanel(10));
    }

    public Plugin(Main game) : base(game)
    {
    }

    ~Plugin()
    {
        
    }
}