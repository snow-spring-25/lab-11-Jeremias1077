using MazeWeb.Persistence;

namespace labyrinth;

public class MazeGame
{
    public static MazeGame Instance { get; } = new MazeGame();
    public static MazeGame TwistyInstance { get; } = new MazeGame();

    public string MazeName { get; set; }
    public MazeCell StartingLocation { get; private set; }

    public Dictionary<string, Player> Players { get; } = new();
    public Player? Winner { get; private set; }
    private bool isTwisty = false;

    public void StartNewMaze(string mazeName)
    {
        MazeName = mazeName;
        StartingLocation = MazeUtilities.mazeFor(mazeName);
    }

    public void StartNewTwistyMaze(string mazeName)
    {
        MazeName = mazeName;
        StartingLocation = MazeUtilities.twistyMazeFor(mazeName);
        isTwisty = true;
    }

    //Events
    public event Action UpdateLeaderboard;

    //Try to understand this -
    public (MazeCell, string) Movement(string direction, MazeCell location, string playerName)
    {
        if (Winner != null)
        {
            return (location, $"{Winner.Name} Wins!");
        }
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

                Players[playerName].Location = location;
                if (location.WhatsHere != Item.Nothing && !Players[playerName].Items.Contains(location.WhatsHere))
                {
                    Players[playerName].Items.Add(location.WhatsHere);
                    if (Players[playerName].Items.Count >= 3 && Winner == null)
                    {
                        Winner = Players[playerName];
                        var highScore = new HighScore
                        {
                            MazeType = isTwisty ? "Twist" : "Normal",
                            PlayerName = playerName,
                            Timestamp = DateTime.Now,
                            TotalMoves = Winner.MoveCount
                        };
                        IDataStore.Instance.AddHighScore(highScore);
                    }
                }
                UpdateLeaderboard?.Invoke();

                return (location, $"You moved {direction} to cell {location.Id}.");
            }
        }
        catch
        {
        }

        return (location, $"You can't go {direction} from cell {location?.Id}.");
    }
}
