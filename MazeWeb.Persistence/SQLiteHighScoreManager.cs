using SQLite;
namespace MazeWeb.Persistence;


class SQLiteHighScoreManager : IDataStore
{
    private SQLiteConnection _connection;

    public SQLiteHighScoreManager()
    {
            _connection = new SQLiteConnection("HighScores.db");
            _connection.CreateTable<HighScore>();
    }
    public void AddHighScore(HighScore highScore)
    {
        _connection.Insert(highScore);
    }

    public IEnumerable<HighScore> GetHighScores()
    {
        return _connection.Table<HighScore>().ToList();
    }

    public void UpdateScore(HighScore highScores)
    {
        _connection.Update(highScores);
    }
}