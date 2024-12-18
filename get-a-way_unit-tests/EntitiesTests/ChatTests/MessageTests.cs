using get_a_way_unit_tests.EntitiesTests.AccountsTests;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.ChatTests;

public class MessageTests
{
    private Message _validMessage;
    private AccountTests.TestAccount _validSender;
    private ChatRoom _validChatRoom;


    private const string ValidText = "Valid text of a message.";

    [SetUp]
    public void SetUpEnvironment()
    {
        Message.ResetExtent();
        Account.ResetExtent();
        ChatRoom.ResetExtent();
        
        _validSender = new AccountTests.TestAccount("ValidUserName", "ValidPassword123", "validemail@pjwstk.edu.pl");
        _validChatRoom = new ChatRoom("Test ChatRoom", "static/img/default_chatroom_img.jpg");
        _validMessage = new Message(ValidText, _validSender, _validChatRoom);
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var message1 = new Message(ValidText, _validSender, _validChatRoom);
        var message2 = new Message(ValidText, _validSender, _validChatRoom);

        Assert.That(message2.ID - message1.ID, Is.EqualTo(1));
    }
    
    [Test]
    public void Constructor_AssignsCorrectValuesToAssociations()
    {
        Assert.That(_validMessage.Sender, Is.EqualTo(_validSender));
        Assert.That(_validMessage.ChatRoom, Is.EqualTo(_validChatRoom));
        Assert.That(_validSender.Messages.Contains(_validMessage));
        Assert.That(_validChatRoom.Messages.Contains(_validMessage));
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        Assert.That(_validMessage.Text, Is.EqualTo(ValidText));
        Assert.That(_validMessage.Timestamp, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
        Assert.That(_validMessage.Edited, Is.False);
    }

    [Test]
    public void Constructor_WithNullSenderAndChatroom_ThrowsArgumentNullException()
    {
        Assert.That(() => new Message(ValidText, null, null), Throws.TypeOf<ArgumentNullException>());
    }
    
    [Test]
    public void Setter_ValidText_SetsText()
    {
        _validMessage.Text = "AnotherValidText";
        Assert.That(_validMessage.Text, Is.EqualTo("AnotherValidText"));

        _validMessage.Text = "t";
        Assert.That(_validMessage.Text, Is.EqualTo("t"));

        _validMessage.Text = "50";
        Assert.That(_validMessage.Text, Is.EqualTo("50"));

        _validMessage.Text = "_";
        Assert.That(_validMessage.Text, Is.EqualTo("_"));
    }

    [Test]
    public void Setter_InvalidText_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validMessage.Text = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validMessage.Text, Is.EqualTo(ValidText));

        Assert.That(() => _validMessage.Text = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validMessage.Text, Is.EqualTo(ValidText));

        Assert.That(() => _validMessage.Text = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validMessage.Text, Is.EqualTo(ValidText));

        var longText = new string('a', 10001);
        Assert.That(() => _validMessage.Text = longText, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validMessage.Text, Is.EqualTo(ValidText));
    }

    [Test]
    public void Setter_NewTimestampValue_ThrowsInvalidOperationException()
    {
        DateTime originalTimestamp = _validMessage.Timestamp;
        
        Assert.That(() => _validMessage.Timestamp = DateTime.Now, Throws.TypeOf<InvalidOperationException>());
        Assert.That(_validMessage.Timestamp, Is.EqualTo(originalTimestamp).Within(TimeSpan.FromSeconds(1)));
    }
    
    [Test]
    public void EditedField_UpdatesWhenTextChanges()
    {
        _validMessage.Text = "Updated text";

        Assert.That(_validMessage.Text, Is.EqualTo("Updated text"));
        Assert.That(_validMessage.Edited, Is.True);
    }

    [Test]
    public void EditedField_DoesNotUpdateIfTextRemainsSame()
    {
        _validMessage.Text = ValidText;

        Assert.That(_validMessage.Text, Is.EqualTo(ValidText));
        Assert.That(_validMessage.Edited, Is.False);
    }

    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = Message.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestInstance = new Message(ValidText, _validSender, _validChatRoom);
        Assert.That(Message.GetExtentCopy().Count, Is.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_OnRemovalOfInstance_DecreasesExtentCount()
    {
        int count = Message.GetExtentCopy().Count;
        Message.RemoveInstanceFromExtent(_validMessage);
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