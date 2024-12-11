using get_a_way_unit_tests.EntitiesTests.AccountsTests;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.ChatTests;

public class MessageTests
{
    private Message _valid;

    private const string ValidText = "Valid text of a message.";

    [SetUp]
    public void SetUpEnvironment()
    {
        Message.ResetExtent();
        Account.ResetExtent();
        ChatRoom.ResetExtent();
        _valid = new Message(ValidText);
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var message = new Message(ValidText);

        Assert.That(message.Text, Is.EqualTo(ValidText));
        Assert.That(message.Timestamp, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));

        // ID is 2 because _valid.ID == 1
        Assert.That(message.ID, Is.EqualTo(2));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var message1 = new Message(ValidText);
        var message2 = new Message(ValidText);

        Assert.That(message2.ID - message1.ID, Is.EqualTo(1));
    }
    
    [Test]
    public void Constructor_WithSender_AssignsCorrectValues()
    {
        var account = new AccountTests.TestAccount("User1", "Password123", "user1@pjwstk.edu.pl");
        var message = new Message(ValidText, account);

        Assert.That(message.Text, Is.EqualTo(ValidText));
        Assert.That(message.Timestamp, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
        Assert.That(message.Sender, Is.EqualTo(account));
        Assert.That(account.Messages.Contains(message));
    }

    [Test]
    public void Constructor_WithNullSender_ThrowsArgumentNullException()
    {
        Assert.That(() => new Message(ValidText, null), Throws.TypeOf<ArgumentNullException>());
    }
    
    [Test]
    public void Setter_ValidText_SetsText()
    {
        _valid.Text = "AnotherValidText";
        Assert.That(_valid.Text, Is.EqualTo("AnotherValidText"));

        _valid.Text = "t";
        Assert.That(_valid.Text, Is.EqualTo("t"));

        _valid.Text = "50";
        Assert.That(_valid.Text, Is.EqualTo("50"));

        _valid.Text = "_";
        Assert.That(_valid.Text, Is.EqualTo("_"));
    }

    [Test]
    public void Setter_InvalidText_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Text = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Text, Is.EqualTo(ValidText));

        Assert.That(() => _valid.Text = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Text, Is.EqualTo(ValidText));

        Assert.That(() => _valid.Text = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Text, Is.EqualTo(ValidText));

        var longText = new string('a', 10001);
        Assert.That(() => _valid.Text = longText, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Text, Is.EqualTo(ValidText));
    }

    [Test]
    public void Setter_NewTimestampValue_ThrowsInvalidOperationException()
    {
        DateTime originalTimestamp = _valid.Timestamp;
        
        Assert.That(() => _valid.Timestamp = DateTime.Now, Throws.TypeOf<InvalidOperationException>());
        Assert.That(_valid.Timestamp, Is.EqualTo(originalTimestamp).Within(TimeSpan.FromSeconds(1)));
    }
    
    [Test]
    public void AssignToChatRoom_ValidChatRoom_AssignsChatRoom()
    {
        var chatRoom = new ChatRoom("Test ChatRoom", "https://valid/photo.png");

        _valid.AssignToChatRoom(chatRoom);

        Assert.That(_valid.ChatRoom, Is.EqualTo(chatRoom));
    }
    
    [Test]
    public void AssignToChatRoom_NullChatRoom_ThrowsArgumentNullException()
    {
        Assert.That(() => _valid.AssignToChatRoom(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void RemoveFromChatRoom_RemovesChatRoomReference()
    {
        var chatRoom = new ChatRoom("Test ChatRoom", "https://valid/photo.png");
        _valid.AssignToChatRoom(chatRoom);

        _valid.RemoveFromChatRoom();

        Assert.That(_valid.ChatRoom, Is.Null);
        Assert.That(chatRoom.Messages.Contains(_valid), Is.False);
    }
    
    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = Message.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestInstance = new Message(ValidText);
        Assert.That(Message.GetExtentCopy().Count, Is.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_OnRemovalOfInstance_DecreasesExtentCount()
    {
        int count = Message.GetExtentCopy().Count;
        Message.RemoveInstanceFromExtent(_valid);
        Assert.That(Message.GetExtentCopy().Count, Is.EqualTo(count - 1));
    }

    [Test]
    public void GetExtentCopy_DoesNotReturnActualExtent()
    {
        // addresses are different
        Assert.True(Message.GetExtentCopy() != Message.GetExtent());
    }

    [Test]
    public void ResetExtent_ClearsExtent()
    {
        Message.ResetExtent();
        Assert.That(Message.GetExtent().Count, Is.EqualTo(0));
    }
}