using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.AccountsTests;

public class AccountTests
{
    public class TestAccount(string username, string password, string email) : Account(username, password, email);

    private TestAccount _validAccount;
    private Message _validMessage;
    private ChatRoom _validChatRoom;

    // cant have 2 accounts with same username
    private const string ValidUserName = "ValidUserName";
    private const string AnotherValidUserName = "AnotherValidUserName";
    private const string ValidPassword = "ValidPassword123";
    private const string ValidEmail = "validemail@pjwstk.edu.pl";
    private const string DefaultProfilePictureUrl = "static/img/default_profile_img.jpg";

    [SetUp]
    public void SetUpEnvironment()
    {
        Account.ResetExtent();
        Message.ResetExtent();
        ChatRoom.ResetExtent();
        _validAccount = new TestAccount(ValidUserName, ValidPassword, ValidEmail);
        _validChatRoom = new ChatRoom("Test ChatRoom", "static/img/default_chatroom_img.jpg");
        _validMessage = new Message("Some text", _validAccount, _validChatRoom);
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        // do not use ValidUserName, will throw DuplicateUsernameException
        var account = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);

        // ID == 2 because _validAccount.ID == 1
        Assert.That(account.ID, Is.EqualTo(2));

        Assert.That(account.Username, Is.EqualTo(AnotherValidUserName));
        Assert.That(account.Password, Is.EqualTo(ValidPassword));
        Assert.That(account.Email, Is.EqualTo(ValidEmail));

        Assert.That(account.ProfilePictureUrl, Is.EqualTo(DefaultProfilePictureUrl));

        Assert.That(account.Verified, Is.False);
        Assert.That(account.Rating, Is.EqualTo(10.0));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var test1 = new TestAccount("username1", ValidPassword, ValidEmail);
        var test2 = new TestAccount("username2", ValidPassword, ValidEmail);

        Assert.That(test2.ID - test1.ID, Is.EqualTo(1));
    }

    [Test]
    public void Setter_ValidUsername_SetsUsername()
    {
        _validAccount.Username = AnotherValidUserName;
        Assert.That(_validAccount.Username, Is.EqualTo(AnotherValidUserName));
    }

    [Test]
    public void Setter_InvalidUsername_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validAccount.Username = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validAccount.Username, Is.EqualTo(ValidUserName));

        Assert.That(() => _validAccount.Username = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validAccount.Username, Is.EqualTo(ValidUserName));

        Assert.That(() => _validAccount.Username = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validAccount.Username, Is.EqualTo(ValidUserName));

        Assert.That(() => _validAccount.Username = "inv", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validAccount.Username, Is.EqualTo(ValidUserName));

        Assert.That(() => _validAccount.Username = "tooLongOfUsernameToBeValidAndSomeMore",
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _validAccount.Username, Is.EqualTo(ValidUserName));
    }

    [Test]
    public void Setter_DuplicateUsername_ThrowsDuplicateUsernameException()
    {
        Assert.That(() =>
                new TestAccount(ValidUserName, ValidPassword, ValidEmail),
            Throws.TypeOf<DuplicateUsernameException>());
    }

    [Test]
    public void Setter_ValidPassword_SetsPassword()
    {
        _validAccount.Password = "NewVal1dPassword";
        Assert.That(_validAccount.Password, Is.EqualTo("NewVal1dPassword"));
    }

    [Test]
    public void Setter_InvalidPassword_ThrowsInvalidPasswordException()
    {
        Assert.That(() => _validAccount.Password = null, Throws.TypeOf<NullReferenceException>());
        Assert.That(_validAccount.Password, Is.EqualTo(ValidPassword));

        Assert.That(() => _validAccount.Password = "", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_validAccount.Password, Is.EqualTo(ValidPassword));

        Assert.That(() => _validAccount.Password = " ", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_validAccount.Password, Is.EqualTo(ValidPassword));

        Assert.That(() => _validAccount.Password = "inv", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_validAccount.Password, Is.EqualTo(ValidPassword));

        Assert.That(() => _validAccount.Password = "nouppercasepassword1", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_validAccount.Password, Is.EqualTo(ValidPassword));

        Assert.That(() => _validAccount.Password = "NOLOWERCASEPASSWORD1", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_validAccount.Password, Is.EqualTo(ValidPassword));

        Assert.That(() => _validAccount.Password = "NoDigitPassword", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_validAccount.Password, Is.EqualTo(ValidPassword));

        Assert.That(() => _validAccount.Password = "SuperMegaLongPasswordThatIsTooLongToBeValid123456",
            Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_validAccount.Password, Is.EqualTo(ValidPassword));
    }

    [Test]
    public void Setter_ValidEmail_SetsEmail()
    {
        _validAccount.Email = "NewValidEmail@pjwstk.edu.pl";
        Assert.That(_validAccount.Email, Is.EqualTo("NewValidEmail@pjwstk.edu.pl"));
    }

    [Test]
    public void Setter_InvalidEmail_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _validAccount.Email = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_validAccount.Email, Is.EqualTo(ValidEmail));

        Assert.That(() => _validAccount.Email = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_validAccount.Email, Is.EqualTo(ValidEmail));

        Assert.That(() => _validAccount.Email = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_validAccount.Email, Is.EqualTo(ValidEmail));

        Assert.That(() => _validAccount.Email = "email.with.no.at", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_validAccount.Email, Is.EqualTo(ValidEmail));

        Assert.That(() => _validAccount.Email = "email@nodot", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_validAccount.Email, Is.EqualTo(ValidEmail));
    }

    [Test]
    public void Setter_ValidProfilePictureUrl_SetsProfilePictureUrl()
    {
        _validAccount.ProfilePictureUrl = "https://i.pinimg.com/736x/79/a3/16/79a3168cf52edca304ff32db46e0f888.jpg";
        Assert.That(_validAccount.ProfilePictureUrl,
            Is.EqualTo("https://i.pinimg.com/736x/79/a3/16/79a3168cf52edca304ff32db46e0f888.jpg"));
    }

    [Test]
    public void Setter_InvalidProfilePictureUrl_SetsDefaultProfilePicture()
    {
        _validAccount.ProfilePictureUrl = null;
        Assert.That(_validAccount.ProfilePictureUrl, Is.EqualTo(DefaultProfilePictureUrl));

        _validAccount.ProfilePictureUrl = "";
        Assert.That(_validAccount.ProfilePictureUrl, Is.EqualTo(DefaultProfilePictureUrl));

        _validAccount.ProfilePictureUrl = " ";
        Assert.That(_validAccount.ProfilePictureUrl, Is.EqualTo(DefaultProfilePictureUrl));

        _validAccount.ProfilePictureUrl = "invalid.path";
        Assert.That(_validAccount.ProfilePictureUrl, Is.EqualTo(DefaultProfilePictureUrl));
    }

    [Test]
    public void Setter_ValidRating_SetsRating()
    {
        _validAccount.Rating = 8.0;
        Assert.That(_validAccount.Rating, Is.EqualTo(8.0));
    }

    [Test]
    public void Setter_InvalidRating_SetsRatingWithinBounds()
    {
        _validAccount.Rating = -5.0;
        Assert.That(_validAccount.Rating, Is.EqualTo(0));
        _validAccount.Rating = 100500;
        Assert.That(_validAccount.Rating, Is.EqualTo(10.0));
    }

    [Test]
    public void AddLanguage_ValidLanguage_AddsLanguageToList()
    {
        _validAccount.AddLanguage(Language.English);
        Assert.That(_validAccount.Languages.Contains(Language.English));
        Assert.That(_validAccount.Languages.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddLanguage_DuplicateLanguage_DoesNotAddTwice()
    {
        _validAccount.AddLanguage(Language.Ukrainian);
        _validAccount.AddLanguage(Language.Ukrainian);
        Assert.That(_validAccount.Languages.Count, Is.EqualTo(1));
    }

    [Test]
    public void RemoveLanguage_ExistingLanguage_RemovesFromLanguages()
    {
        _validAccount.AddLanguage(Language.English);

        _validAccount.RemoveLanguage(Language.English);

        Assert.That(_validAccount.Languages.Contains(Language.English), Is.False);
        Assert.That(_validAccount.Languages.Count, Is.EqualTo(0));
    }

    [Test]
    public void RemoveLanguage_NonExistingLanguage_DoesNothing()
    {
        _validAccount.RemoveLanguage(Language.Mandarin); //was not added

        Assert.That(_validAccount.Languages.Contains(Language.Mandarin), Is.False);
        Assert.That(_validAccount.Languages.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetLanguages_ReturnsCopy()
    {
        _validAccount.AddLanguage(Language.Hungarian);

        var languages = _validAccount.Languages;

        //modify copy
        languages.Clear();
        Assert.That(_validAccount.Languages.Count, Is.EqualTo(1)); //original set unchanged
    }

    [Test]
    public void Follow_ValidAccount_AddsToFollowingsAndFollowers()
    {
        var validAccountToFollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);
        _validAccount.Follow(validAccountToFollow);

        Assert.That(_validAccount.Followings.Contains(validAccountToFollow));
        Assert.That(validAccountToFollow.Followers.Contains(_validAccount));
    }

    [Test]
    public void Unfollow_ValidAccount_RemovesFromFollowingsAndFollowers()
    {
        var validAccountToUnfollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);
        _validAccount.Follow(validAccountToUnfollow);

        _validAccount.Unfollow(validAccountToUnfollow);

        Assert.That(_validAccount.Followings.Contains(validAccountToUnfollow), Is.False);
        Assert.That(validAccountToUnfollow.Followers.Contains(_validAccount), Is.False);
    }

    [Test]
    public void Follow_SelfFollow_ThrowsInvalidOperationException()
    {
        Assert.That(() => _validAccount.Follow(_validAccount), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void Follow_DuplicateAccount_DoesNotAddTwice()
    {
        var validAccountToFollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);

        _validAccount.Follow(validAccountToFollow);
        _validAccount.Follow(validAccountToFollow);

        Assert.That(_validAccount.Followings.Count, Is.EqualTo(1));
        Assert.That(validAccountToFollow.Followers.Count, Is.EqualTo(1));
    }

    [Test]
    public void Follow_NullAccount_ThrowsArgumentNullException()
    {
        Assert.That(() => _validAccount.Follow(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Unfollow_NullAccount_ThrowsArgumentNullException()
    {
        Assert.That(() => _validAccount.Unfollow(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Unfollow_NonFollowedAccount_DoesNothing()
    {
        var validAccountToUnfollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);
        _validAccount.Unfollow(validAccountToUnfollow);

        Assert.That(_validAccount.Followings.Contains(validAccountToUnfollow), Is.False);
        Assert.That(validAccountToUnfollow.Followers.Contains(_validAccount), Is.False);
    }

    [Test]
    public void GetFollowings_ReturnsCopy()
    {
        var validAccountToFollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);
        _validAccount.Follow(validAccountToFollow);
        var followings = _validAccount.Followings;

        //modify the returned copy
        followings.Clear();
        Assert.That(_validAccount.Followings.Count, Is.EqualTo(1)); //original set is unchanged
    }

    [Test]
    public void GetFollowers_ReturnsCopy()
    {
        var validAccountToFollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);
        validAccountToFollow.Follow(_validAccount);
        var followers = _validAccount.Followers;

        //modify the returned copy
        followers.Clear();
        Assert.That(_validAccount.Followers.Count, Is.EqualTo(1)); //original set is unchanged
    }

    [Test]
    public void JoinChatroom_ValidChatroom_AddsAccountToChatroom()
    {
        _validAccount.JoinChatroom(_validChatRoom);

        Assert.That(_validAccount.Chatrooms.Contains(_validChatRoom));
        Assert.That(_validChatRoom.Members.Contains(_validAccount));
    }

    [Test]
    public void LeaveChatroom_ValidChatroom_RemovesAccountFromChatroom()
    {
        _validAccount.JoinChatroom(_validChatRoom);

        _validAccount.LeaveChatroom(_validChatRoom);

        Assert.That(_validAccount.Chatrooms.Contains(_validChatRoom), Is.False);
        Assert.That(_validChatRoom.Members.Contains(_validAccount), Is.False);
    }

    [Test]
    public void JoinChatroom_DuplicateChatroom_DoesNotAddTwice()
    {
        _validAccount.JoinChatroom(_validChatRoom);
        _validAccount.JoinChatroom(_validChatRoom);

        Assert.That(_validAccount.Chatrooms.Count, Is.EqualTo(1));
        Assert.That(_validChatRoom.Members.Count, Is.EqualTo(1));
    }

    [Test]
    public void LeaveChatroom_NonJoinedChatroom_DoesNothing()
    {
        _validAccount.LeaveChatroom(_validChatRoom);

        Assert.That(_validAccount.Chatrooms.Contains(_validChatRoom), Is.False);
        Assert.That(_validChatRoom.Members.Contains(_validAccount), Is.False);
    }

    [Test]
    public void JoinChatroom_NullChatroom_ThrowsArgumentNullException()
    {
        Assert.That(() => _validAccount.JoinChatroom(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void LeaveChatroom_NullChatroom_ThrowsArgumentNullException()
    {
        Assert.That(() => _validAccount.LeaveChatroom(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void GetChatrooms_ReturnsCopy()
    {
        _validAccount.JoinChatroom(_validChatRoom);

        var chatrooms = _validAccount.Chatrooms;

        chatrooms.Clear();

        Assert.That(_validAccount.Chatrooms.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddMessage_ValidMessage_AddsToMessages()
    {
        Assert.That(_validAccount.Messages.Contains(_validMessage));
        Assert.That(_validMessage.Sender, Is.EqualTo(_validAccount));
    }

    [Test]
    public void AddMessage_NullMessage_ThrowsArgumentNullException()
    {
        Assert.That(() => _validAccount.AddMessage(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void RemoveMessage_ExistingMessage_RemovesFromMessages()
    {
        _validAccount.RemoveMessage(_validMessage);

        Assert.That(_validAccount.Messages.Contains(_validMessage), Is.False);
    }

    [Test]
    public void Messages_ReturnsCopy()
    {
        var messagesCopy = _validAccount.Messages;

        messagesCopy.Clear();

        Assert.That(_validAccount.Messages.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = Account.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestAccount = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);
        Assert.That(Account.GetExtentCopy().Count, Is.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_OnRemovalOfInstance_DecreasesExtentCount()
    {
        int count = Account.GetExtentCopy().Count;
        Account.RemoveInstanceFromExtent(_validAccount);
        Assert.That(Account.GetExtentCopy().Count, Is.EqualTo(count - 1));
    }
    
    [Test]
    public void GetExtentCopy_DoesNotReturnActualExtent()
    {
        // addresses are different
        Assert.True(Account.GetExtentCopy() != Account.GetExtent());
    }

    [Test]
    public void ResetExtent_ClearsExtent()
    {
        Account.ResetExtent();
        Assert.That(Account.GetExtent().Count, Is.EqualTo(0));
    }
}