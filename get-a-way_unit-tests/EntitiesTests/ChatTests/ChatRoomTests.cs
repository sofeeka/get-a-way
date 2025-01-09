using get_a_way_unit_tests.EntitiesTests.AccountsTests;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.ChatTests;

public class ChatRoomTests
{
    private ChatRoom _validChatroom;

    private AccountTests.TestAccount _member1;
    private AccountTests.TestAccount _member2;
    private AccountTests.TestAccount _member3;
    readonly HashSet<Account> _members = new HashSet<Account>();

    private Message _validMessage;

    private const string ValidName = "ValidName";
    private const string DefaultPhotoUrl = "static/img/default_chatroom_img.jpg";

    [SetUp]
    public void SetUpEnvironment()
    {
        _member1 = new AccountTests.TestAccount("Member1", "ValidPassword123", "validemail1@pjwstk.edu.pl");
        _member2 = new AccountTests.TestAccount("Member2", "ValidPassword123", "validemail2@pjwstk.edu.pl");
        _member3 = new AccountTests.TestAccount("Member3", "ValidPassword123", "validemail3@pjwstk.edu.pl");

        // at least two members are required for creation of a chatroom
        _members.Add(_member1);
        _members.Add(_member2);

        _validChatroom = new ChatRoom(_members, ValidName, DefaultPhotoUrl);
        _validMessage = new Message("Valid message text", _member1, _validChatroom);
    }

    [TearDown]
    public void TearDownEnvironment()
    {
        ChatRoom.ResetExtent();
        Account.ResetExtent();
        Message.ResetExtent();
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var chatRoom = new ChatRoom(_members, ValidName, DefaultPhotoUrl);

        Assert.That(chatRoom.Name, Is.EqualTo(ValidName));
        Assert.That(chatRoom.PhotoUrl, Is.EqualTo(DefaultPhotoUrl));
        Assert.That(chatRoom.Members, Does.Contain(_member1));
        Assert.That(chatRoom.Members, Does.Contain(_member2));
        Assert.That(chatRoom.Members, Does.Not.Contain(_member3));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var chatRoom1 = new ChatRoom(_members, ValidName, DefaultPhotoUrl);
        var chatRoom2 = new ChatRoom(_members, ValidName, DefaultPhotoUrl);

        Assert.That(chatRoom2.ID - chatRoom1.ID, Is.EqualTo(1));
    }

    [Test]
    public void Constructor_LessThenTwoMembers_ThrowsInvalidOperationException()
    {
        var members = new HashSet<Account>();
        members.Add(_member3);
        Assert.That(()=>new ChatRoom(members, ValidName, DefaultPhotoUrl), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void Constructor_NullMembers_ThrowsArgumentNullException()
    {
        Assert.That(()=>new ChatRoom(null, ValidName, DefaultPhotoUrl), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Constructor_NullMember_ThrowsArgumentNullException()
    {
        var members = new HashSet<Account>();
        members.Add(null);
        members.Add(_member3);
        
        Assert.That(()=>new ChatRoom(members, ValidName, DefaultPhotoUrl), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Setter_ValidName_SetsName()
    {
        _validChatroom.Name = "AnotherValidName";
        Assert.That(_validChatroom.Name, Is.EqualTo("AnotherValidName"));

        _validChatroom.Name = "v";
        Assert.That(_validChatroom.Name, Is.EqualTo("v"));

        _validChatroom.Name = "50";
        Assert.That(_validChatroom.Name, Is.EqualTo("50"));

        _validChatroom.Name = "_";
        Assert.That(_validChatroom.Name, Is.EqualTo("_"));
    }

    [Test]
    public void Setter_InvalidName_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validChatroom.Name = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validChatroom.Name, Is.EqualTo(ValidName));

        Assert.That(() => _validChatroom.Name = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validChatroom.Name, Is.EqualTo(ValidName));

        Assert.That(() => _validChatroom.Name = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validChatroom.Name, Is.EqualTo(ValidName));

        Assert.That(
            () => _validChatroom.Name = "tooLongOfUsernameToBeValidAndSomeMoreTooLongOfUsernameToBeValidAndSomeMore",
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validChatroom.Name, Is.EqualTo(ValidName));
    }

    [Test]
    public void Setter_ValidPhotoUrl_SetsPhoto()
    {
        _validChatroom.PhotoUrl = "https://i.pinimg.com/736x/79/a3/16/79a3168cf52edca304ff32db46e0f888.jpg";
        Assert.That(_validChatroom.PhotoUrl,
            Is.EqualTo("https://i.pinimg.com/736x/79/a3/16/79a3168cf52edca304ff32db46e0f888.jpg"));
    }

    [Test]
    public void Setter_InvalidProtoURL_SetsDefaultPhoto()
    {
        _validChatroom.PhotoUrl = null;
        Assert.That(_validChatroom.PhotoUrl, Is.EqualTo(DefaultPhotoUrl));

        _validChatroom.PhotoUrl = "";
        Assert.That(_validChatroom.PhotoUrl, Is.EqualTo(DefaultPhotoUrl));

        _validChatroom.PhotoUrl = " ";
        Assert.That(_validChatroom.PhotoUrl, Is.EqualTo(DefaultPhotoUrl));

        _validChatroom.PhotoUrl = "invalid.path";
        Assert.That(_validChatroom.PhotoUrl, Is.EqualTo(DefaultPhotoUrl));
    }

    [Test]
    public void AddMember_ValidAccount_AddsToMembers()
    {
        _validChatroom.AddMember(_member3);
        Assert.That(_validChatroom.Members.Contains(_member3));
    }

    [Test]
    public void AddMember_NullAccount_ThrowsArgumentNullException()
    {
        Assert.That(() => _validChatroom.AddMember(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void AddMember_DuplicateAccount_DoesNotAddTwice()
    {
        var count = _validChatroom.Members.Count;
        _validChatroom.AddMember(_member1);
        Assert.That(_validChatroom.Members, Has.Count.EqualTo(count));
    }

    [Test]
    public void RemoveMember_ExistingAccount_RemovesFromMembers()
    {
        _validChatroom.RemoveMember(_member1);
        Assert.That(_validChatroom.Members, Does.Not.Contain(_member1));
    }

    [Test]
    public void RemoveMember_NonExistingAccount_DoesNothing()
    {
        _validChatroom.RemoveMember(_member3);
        Assert.That(_validChatroom.Members, Does.Not.Contain(_member3));
    }

    [Test]
    public void GetParticipants_ReturnsCopy()
    {
        var count = _validChatroom.Members.Count;

        var members = _validChatroom.Members;
        members.Clear();

        Assert.That(_validChatroom.Members, Has.Count.EqualTo(count));
    }

    [Test]
    public void AddMessage_ValidMessage_AddsToMessages()
    {
        Assert.That(_validChatroom.Messages, Does.Contain(_validMessage));
        Assert.That(_validMessage.ChatRoom, Is.EqualTo(_validChatroom));
    }

    [Test]
    public void AddMessage_NullMessage_ThrowsArgumentNullException()
    {
        Assert.That(() => _validChatroom.AddMessage(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void RemoveMessage_ValidMessage_RemovesFromMessages()
    {
        _validChatroom.RemoveMessage(_validMessage);

        Assert.That(_validChatroom.Messages.Contains(_validMessage), Is.False);
        Assert.That(Message.GetExtent().Count == 0);
    }

    [Test]
    public void RemoveMessage_NonExistingMessage_DoesNothing()
    {
        var count = _validChatroom.Messages.Count;

        var newMessage = new Message("Non-existing message", _member1, _validChatroom);
        _validChatroom.RemoveMessage(newMessage);

        Assert.That(_validChatroom.Messages.Count, Is.EqualTo(count));
    }

    [Test]
    public void RemoveMessage_NullMessage_ThrowsArgumentNullException()
    {
        Assert.That(() => _validChatroom.RemoveMessage(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = ChatRoom.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestInstance = new ChatRoom(_members, ValidName, DefaultPhotoUrl);
        Assert.That(ChatRoom.GetExtentCopy(), Has.Count.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_RemovesChatRoomAndDeletesItsMessages()
    {
        var message1 = new Message("Message 1", _member1, _validChatroom);
        var message2 = new Message("Message 2", _member2, _validChatroom);
        _validChatroom.AddMessage(message1);
        _validChatroom.AddMessage(message2);

        ChatRoom.RemoveInstanceFromExtent(_validChatroom);

        Assert.That(ChatRoom.GetExtent().Contains(_validChatroom), Is.False);
        Assert.That(Message.GetExtent().Contains(message1), Is.False);
        Assert.That(Message.GetExtent().Contains(message2), Is.False);
    }


    [Test]
    public void GetExtentCopy_DoesNotReturnActualExtent()
    {
        // addresses are different
        Assert.True(ChatRoom.GetExtentCopy() != ChatRoom.GetExtent());
    }

    [Test]
    public void ResetExtent_DeletesAllChatRoomsAndTheirMessages()
    {
        var message1 = new Message("Message 1", _member1, _validChatroom);
        var message2 = new Message("Message 2", _member2, _validChatroom);
        // _validChatroom.AddMessage(message1);
        // _validChatroom.AddMessage(message2);

        ChatRoom.ResetExtent();

        Assert.That(ChatRoom.GetExtent(), Is.Empty);
        Assert.That(Message.GetExtent(), Is.Empty);
    }
}