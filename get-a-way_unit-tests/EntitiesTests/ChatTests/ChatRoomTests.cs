using get_a_way.Entities.Chat;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.ChatTests;

public class ChatRoomTests
{
    private ChatRoom _valid;

    private const string ValidName = "ValidName";
    private const string ValidUrl = "https://valid/photo.png";

    [SetUp]
    public void SetUpEnvironment()
    {
        ChatRoom.ResetExtent();
        _valid = new ChatRoom(ValidName, ValidUrl);
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var chatRoom = new ChatRoom(ValidName, ValidUrl);

        Assert.That(chatRoom.Name, Is.EqualTo(ValidName));
        Assert.That(chatRoom.PhotoUrl, Is.EqualTo(ValidUrl));

        // ID is 2 because _valid.ID == 1
        Assert.That(chatRoom.ID, Is.EqualTo(2));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var chatRoom1 = new ChatRoom(ValidName, ValidUrl);
        var chatRoom2 = new ChatRoom(ValidName, ValidUrl);

        Assert.That(chatRoom2.ID - chatRoom1.ID, Is.EqualTo(1));
    }

    [Test]
    public void Setter_ValidName_SetsName()
    {
        _valid.Name = "AnotherValidName";
        Assert.That(_valid.Name, Is.EqualTo("AnotherValidName"));

        _valid.Name = "v";
        Assert.That(_valid.Name, Is.EqualTo("v"));

        _valid.Name = "50";
        Assert.That(_valid.Name, Is.EqualTo("50"));

        _valid.Name = "_";
        Assert.That(_valid.Name, Is.EqualTo("_"));
    }

    [Test]
    public void Setter_InvalidName_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Name = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Name, Is.EqualTo(ValidName));

        Assert.That(() => _valid.Name = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Name, Is.EqualTo(ValidName));

        Assert.That(() => _valid.Name = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Name, Is.EqualTo(ValidName));

        Assert.That(() => _valid.Name = "tooLongOfUsernameToBeValidAndSomeMoreTooLongOfUsernameToBeValidAndSomeMore",
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Name, Is.EqualTo(ValidName));
    }

    [Test]
    public void Setter_ValidUrl_SetsUrl()
    {
        // todo
    }

    [Test]
    public void Setter_InvalidUrl_ThrowsInvalidAttributeException()
    {
        // todo
    }

    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = ChatRoom.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestInstance = new ChatRoom(ValidName, ValidUrl);
        Assert.That(ChatRoom.GetExtentCopy().Count, Is.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_OnRemovalOfInstance_DecreasesExtentCount()
    {
        int count = ChatRoom.GetExtentCopy().Count;
        ChatRoom.RemoveInstanceFromExtent(_valid);
        Assert.That(ChatRoom.GetExtentCopy().Count, Is.EqualTo(count - 1));
    }

    [Test]
    public void GetExtentCopy_DoesNotReturnActualExtent()
    {
        // addresses are different
        Assert.True(ChatRoom.GetExtentCopy() != ChatRoom.GetExtent());
    }

    [Test]
    public void ResetExtent_ClearsExtent()
    {
        ChatRoom.ResetExtent();
        Assert.That(ChatRoom.GetExtent().Count, Is.EqualTo(0));
    }
}