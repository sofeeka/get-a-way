using get_a_way;
using get_a_way.Services;

namespace get_a_way_unit_tests.DatabaseTests;

public class DatabaseSerializationTests
{
    private const string TestFilePath = "test-db.xml";
    private Database _originalDatabase;
    
    [SetUp]
    public void SetUp()
    {
        _originalDatabase = new Database();
        _originalDatabase.Reset();
        //todo initialize db
    }
    
    [Test]
    public void Serialization_StoresDataCorrectly()
    {
        Serialisation.saveDB(_originalDatabase, TestFilePath);
        Assert.That(File.Exists(TestFilePath), Is.True);
    }

    [Test]
    public void Deserialization_RestoresDataCorrectly()
    {
        Serialisation.saveDB(_originalDatabase, TestFilePath);

        var loadedDatabase = Serialisation.loadDB(TestFilePath);

        Assert.That(loadedDatabase.Accounts.Count, Is.EqualTo(_originalDatabase.Accounts.Count));
        Assert.That(loadedDatabase.ChatRooms.Count, Is.EqualTo(_originalDatabase.ChatRooms.Count));
        Assert.That(loadedDatabase.Messages.Count, Is.EqualTo(_originalDatabase.Messages.Count));
        Assert.That(loadedDatabase.Places.Count, Is.EqualTo(_originalDatabase.Places.Count));
        Assert.That(loadedDatabase.Trips.Count, Is.EqualTo(_originalDatabase.Trips.Count));
        Assert.That(loadedDatabase.Reviews.Count, Is.EqualTo(_originalDatabase.Reviews.Count));
        
        Assert.That(loadedDatabase.Accounts[0].Username, Is.EqualTo(_originalDatabase.Accounts[0].Username));
        Assert.That(loadedDatabase.Messages[0].Text, Is.EqualTo(_originalDatabase.Messages[0].Text));
    }

    [TearDown]
    public void Cleanup()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }
}
