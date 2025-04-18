using SQLite;
namespace MazeWeb.Persistence;

public class HighScore
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string MazeType { get; set;}
    public string PlayerName { get; set;}
    public int TotalMoves { get; set; }
    public DateTime Timestamp { get; set; }
}
