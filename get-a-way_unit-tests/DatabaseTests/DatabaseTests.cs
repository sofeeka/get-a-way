using get_a_way;
using get_a_way.Services;

namespace get_a_way_unit_tests.DatabaseTests;

public class DatabaseTests
{
    [SetUp]
    public void SetUp()
    {
        Database.Reset();
    }

    [Test]
    public void DatabaseReset_EmptiesDatabase()
    {
        var db = DummyDataGenerator.CreateInitialDatabase();

        Assert.True(db.Accounts.Count > 0);
        Assert.True(db.ChatRooms.Count > 0);
        Assert.True(db.Messages.Count > 0);
        Assert.True(db.Places.Count > 0);
        Assert.True(db.Trips.Count > 0);
        Assert.True(db.Reviews.Count > 0);
        
        Database.Reset();

        Assert.True(db.Accounts.Count == 0);
        Assert.True(db.ChatRooms.Count == 0);
        Assert.True(db.Messages.Count == 0);
        Assert.True(db.Places.Count == 0);
        Assert.True(db.Trips.Count == 0);
        Assert.True(db.Reviews.Count == 0);
    }
}