namespace labyrinth;

public class Player
{
    public string Name {get; set;}
    public MazeCell Location {get; set;}
    public List<Item> Items {get;} = new();
    public int MoveCount { get; set; }

}