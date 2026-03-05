using Avalonia.Media;

namespace ColorWar.Models;

public class Player
{
    public string Name { get; set; }
    public IBrush PlayerColor { get; set; }
    
    public Player(string name, IBrush color)
    {
        Name = name;
        PlayerColor = color;
    }
}