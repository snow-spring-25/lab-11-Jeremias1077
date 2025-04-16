namespace labyrinth;

public class MazeGame
{
    public static MazeGame Instance { get; } = new MazeGame();
    public static MazeGame TwistyInstance { get; } = new MazeGame();

    public string MazeName { get; set; }
    public MazeCell StartingLocation { get; private set; }

    public void StartNewMaze(string mazeName)
    {
        MazeName = mazeName;
        StartingLocation = MazeUtilities.mazeFor(mazeName);
    }
    
    public void StartNewTwistyMaze(string mazeName)
    {
        MazeName = mazeName;
        StartingLocation = MazeUtilities.twistyMazeFor(mazeName);
    }

    //Try to understand this -
    public (MazeCell, string) Movement(string direction, MazeCell location)
    {
        try
        {
            var nextCell = direction switch
            {
                "north" => location.North,
                "south" => location.South,
                "east" => location.East,
                "west" => location.West,

                _ => null
            };

            if (nextCell != null)
            {
                location = nextCell;
                return (location, $"You moved {direction} to cell {location.Id}.");
            }
        }
        catch
        {
        }

        return (location, $"You can't go {direction} from cell {location?.Id}.");
    }

}