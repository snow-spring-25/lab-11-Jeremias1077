namespace MazeWeb.Tests;
using MazeWeb;
using labyrinth;
using MazeWeb.Persistence;

public class UnitTest1
{
    [Fact]
    public void StartNewNormalAndTwistyMaze()
    {
        var maze = new MazeGame();
        maze.StartNewMaze("TestMaze");

        var twistyMaze = new MazeGame();
        twistyMaze.StartNewTwistyMaze("TwistyMaze");

        Assert.Equal(maze.MazeName, "TestMaze");
        Assert.NotNull(maze.StartingLocation);

        Assert.Equal(twistyMaze.MazeName, "TwistyMaze");
        Assert.NotNull(twistyMaze.StartingLocation);
    }

    [Fact]
    public void PlayersCanJoinTheGame()
    {
        var maze = new MazeGame();

        var player1 = new Player { Name = "Player1" };
        var player2 = new Player { Name = "Player2" };
        maze.Players.Add(player1.Name, player1);
        maze.Players.Add(player2.Name, player2);

        Assert.Contains("Player1", maze.Players.Keys);
        Assert.Contains("Player2", maze.Players.Keys);
    }

    [Fact]
    public void CheckForNewPlayerLocation()
    {
        var maze = new MazeGame();
        var startingCell = new MazeCell();
        var expectedCell = new MazeCell();
        var player = new Player { Name = "Player" };
        startingCell.North = expectedCell;
        maze.Players.Add(player.Name, player);

        var (_, message) = maze.Movement("north", startingCell, player.Name);

        Assert.Equal(expectedCell, maze.Players["Player"].Location);
        Assert.Equal($"You moved north to cell {expectedCell.Id}.", message);
    }

    [Fact]
    public void CheckForInvalidMovement()
    {
        var maze = new MazeGame();
        var startingCell = new MazeCell();
        maze.Players.Add("Player", new Player { Location = startingCell });

        var (_, message) = maze.Movement("left", startingCell, "Player");

        Assert.Equal(startingCell, maze.Players["Player"].Location);
        Assert.Equal("You can't go left from cell 0.", message);
    }

    [Fact]
    public void PlayerStartsWithNoItems()
    {
        var player = new Player();
        Assert.Empty(player.Items);

        player.Items.Add(Item.Spellbook);
        Assert.Single(player.Items);
    }

    [Fact]
    public void PlayerCanCollectItems()
    {
        var maze = new MazeGame();
        var player = new Player { Name = "Player" };
        maze.Players.Add(player.Name, player);
        var cell1 = new MazeCell { WhatsHere = Item.Spellbook };
        var cell2 = new MazeCell { WhatsHere = Item.Wand };
        var cell3 = new MazeCell { WhatsHere = Item.Potion };

        player.Location = cell1;
        player.Items.Add(player.Location.WhatsHere);
        player.Location = cell2;
        player.Items.Add(player.Location.WhatsHere);
        player.Location = cell3;
        player.Items.Add(player.Location.WhatsHere);

        Assert.Equal(3, player.Items.Count);
    }

    [Fact]
    public void PlayerMoveCountIncreases()
    {
        var maze = new MazeGame();
        var player = new Player { Name = "Player" };
        maze.Players.Add(player.Name, player);
        var startingCell = new MazeCell();
        player.Location = startingCell;

        player.MoveCount++;
        Assert.Equal(1, player.MoveCount);
    }

    [Fact]
    public void KeepTrackOfPlayerInventory()
    {
        var player = new Player { Name = "Player" };

        player.Items.Add(Item.Potion);
        player.Items.Add(Item.Wand);

        Assert.Contains(Item.Potion, player.Items);
        Assert.Contains(Item.Wand, player.Items);
    }
}