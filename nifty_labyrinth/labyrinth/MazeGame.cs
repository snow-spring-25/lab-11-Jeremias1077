namespace labyrinth;

public class MazeGame
{
    public string MazeName {get; set;}
    public MazeCell StartingLocation {get; private set;}
    public MazeCell CurrentLocation {get; private set;}

    public void StartNewMaze(string mazeName)
    {
        MazeName = mazeName;
        StartingLocation = MazeUtilities.mazeFor(mazeName);
        CurrentLocation = StartingLocation;
    }
}