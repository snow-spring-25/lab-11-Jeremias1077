namespace labyrinth;
public class MazeGameTwisty
{
    public string MazeName { get; set; }
    public MazeCell StartingLocation { get; private set; }
    public MazeCell CurrentLocation { get; private set; }
    public Item ItemInCurrentLocation { get; private set; }

    public void StartNewMazeTwisty(string mazeName)
    {
        MazeName = mazeName;
        StartingLocation = MazeUtilities.twistyMazeFor(mazeName);
        CurrentLocation = StartingLocation;
        ItemInCurrentLocation = CurrentLocation.WhatsHere;
    }
}