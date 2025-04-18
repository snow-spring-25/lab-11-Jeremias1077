using SQLite;
namespace MazeWeb.Persistence;

public interface IDataStore
{
    IEnumerable<HighScore> GetHighScores();
    void AddHighScore(HighScore highScore);
    void UpdateScore(HighScore highScores);
    public static IDataStore Instance { get; } = new SQLiteHighScoreManager();
}