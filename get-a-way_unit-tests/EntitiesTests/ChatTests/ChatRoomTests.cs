using get_a_way_unit_tests.EntitiesTests.AccountsTests;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.ChatTests;

public class ChatRoomTests
{
    private ChatRoom _validChatroom;
    private AccountTests.TestAccount _validAccount;
    private Message _validMessage;

    private const string ValidName = "ValidName";
    private const string DefaultPhotoUrl = "static/img/default_chatroom_img.jpg";

    [SetUp]
    public void SetUpEnvironment()
    {
        ChatRoom.ResetExtent();
        Account.ResetExtent();
        Message.ResetExtent();
        _validChatroom = new ChatRoom(ValidName, DefaultPhotoUrl);
        _validAccount = new AccountTests.TestAccount("ValidUserName", "ValidPassword123", "validemail@pjwstk.edu.pl");
        _validMessage = new Message("Valid message text");
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var chatRoom = new ChatRoom(ValidName, DefaultPhotoUrl);

        Assert.That(chatRoom.Name, Is.EqualTo(ValidName));
        Assert.That(chatRoom.PhotoUrl, Is.EqualTo(DefaultPhotoUrl));

        // ID is 2 because _valid.ID == 1
        Assert.That(chatRoom.ID, Is.EqualTo(2));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var chatRoom1 = new ChatRoom(ValidName, DefaultPhotoUrl);
        var chatRoom2 = new ChatRoom(ValidName, DefaultPhotoUrl);

        Assert.That(chatRoom2.ID - chatRoom1.ID, Is.EqualTo(1));
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

        Assert.That(() => _validChatroom.Name = "tooLongOfUsernameToBeValidAndSomeMoreTooLongOfUsernameToBeValidAndSomeMore",
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validChatroom.Name, Is.EqualTo(ValidName));
    }

    [Test]
    public void Setter_ValidPhotoUrl_SetsPhoto()
    {
        _validChatroom.PhotoUrl = "https://i.pinimg.com/736x/79/a3/16/79a3168cf52edca304ff32db46e0f888.jpg";
        Assert.That(_validChatroom.PhotoUrl, Is.EqualTo("https://i.pinimg.com/736x/79/a3/16/79a3168cf52edca304ff32db46e0f888.jpg"));
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
        _validChatroom.AddMember(_validAccount);

        Assert.That(_validChatroom.Members.Contains(_validAccount));
    }
    
    [Test]
    public void AddMember_NullAccount_ThrowsArgumentNullException()
    {
        Assert.That(() => _validChatroom.AddMember(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void AddMember_DuplicateAccount_DoesNotAddTwice()
    {
        _validChatroom.AddMember(_validAccount);
        _validChatroom.AddMember(_validAccount);

        Assert.That(_validChatroom.Members.Count, Is.EqualTo(1));
    }

    [Test]
    public void RemoveMember_ExistingAccount_RemovesFromMembers()
    {
        _validChatroom.AddMember(_validAccount);

        _validChatroom.RemoveMember(_validAccount);

        Assert.That(_validChatroom.Members.Contains(_validAccount), Is.False);
    }
    
    [Test]
    public void RemoveMember_NonExistingAccount_DoesNothing()
    {
       _validChatroom.RemoveMember(_validAccount);

        Assert.That(_validChatroom.Members.Contains(_validAccount), Is.False);
    }

    [Test]
    public void GetParticipants_ReturnsCopy()
    {
        _validChatroom.AddMember(_validAccount);

        var participants = _validChatroom.Members;

        participants.Clear();

        Assert.That(_validChatroom.Members.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void AddMessage_ValidMessage_AddsToChatRoomAndSetsChatRoomReference()
    {
        _validChatroom.AddMessage(_validMessage);

        Assert.That(_validChatroom.Messages.Contains(_validMessage));
        Assert.That(_validMessage.ChatRoom, Is.EqualTo(_validChatroom));
    }

    [Test]
    public void AddMessage_MessageAlreadyInAnotherChatRoom_ThrowsInvalidOperationException()
    {
        var anotherChatRoom = new ChatRoom("Another ChatRoom", DefaultPhotoUrl);
        anotherChatRoom.AddMessage(_validMessage);

        Assert.That(() => _validChatroom.AddMessage(_validMessage), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void RemoveMessage_ValidMessage_RemovesFromChatRoomAndClearsChatRoomReference()
    {
        _validChatroom.AddMessage(_validMessage);

        _validChatroom.RemoveMessage(_validMessage);

        Assert.That(_validChatroom.Messages.Contains(_validMessage), Is.False);
        Assert.That(_validMessage.ChatRoom, Is.Null);
    }

    [Test]
    public void RemoveMessage_MessageNotInChatRoom_DoesNothing()
    {
        _validChatroom.RemoveMessage(_validMessage);

        Assert.That(_validChatroom.Messages.Contains(_validMessage), Is.False);
        Assert.That(_validMessage.ChatRoom, Is.Null);
    }

    
    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = ChatRoom.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestInstance = new ChatRoom(ValidName, DefaultPhotoUrl);
        Assert.That(ChatRoom.GetExtentCopy().Count, Is.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_RemovesChatRoomAndDeletesItsMessages()
    {
        var message1 = new Message("Message 1");
        var message2 = new Message("Message 2");
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
        var message1 = new Message("Message 1");
        var message2 = new Message("Message 2");
        _validChatroom.AddMessage(message1);
        _validChatroom.AddMessage(message2);

        ChatRoom.ResetExtent();

        Assert.That(ChatRoom.GetExtent().Count, Is.EqualTo(0));
        Assert.That(Message.GetExtent().Count, Is.EqualTo(1)); //bcs of _validMessage
    }
}