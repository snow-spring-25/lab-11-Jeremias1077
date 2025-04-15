namespace labyrinth;

public class MazeGame
{
    public string MazeName { get; set; }
    public MazeCell StartingLocation { get; private set; }
    public MazeCell CurrentLocation { get; private set; }
    public Item ItemInCurrentLocation { get; private set; }

    public void StartNewMaze(string mazeName)
    {
        MazeName = mazeName;
        StartingLocation = MazeUtilities.mazeFor(mazeName);
        CurrentLocation = StartingLocation;
        ItemInCurrentLocation = CurrentLocation.WhatsHere;

    }

    //Try to understand this -
    public string Movement(string direction)
    {
        var nextCell = direction switch
        {
            "north" => CurrentLocation.North,
            "south" => CurrentLocation.South,
            "east" => CurrentLocation.East,
            "west" => CurrentLocation.West,

            _ => null
        };

        if (nextCell != null)
        {
            CurrentLocation = nextCell;
            ItemInCurrentLocation = CurrentLocation.WhatsHere;
            return $"You moved {direction} to cell {CurrentLocation.Id}.";
        }
        else
        {
            return $"You can't go {direction} from cell {CurrentLocation.Id}.";
        }
    }
}

public class MazeInstance : MazeGame
{
    public static MazeInstance Instance { get; } = new MazeInstance();
    public MazeInstance()
    {

    }
}