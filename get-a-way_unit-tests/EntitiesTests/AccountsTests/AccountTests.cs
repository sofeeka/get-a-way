using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.AccountsTests;

public class AccountTests
{
    public class TestAccount(string username, string password, string email) : Account(username, password, email);

    private TestAccount _valid;

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
        _valid = new TestAccount(ValidUserName, ValidPassword, ValidEmail);
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        // do not use ValidUserName, will throw DuplicateUsernameException
        var account = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);

        // ID == 2 because _valid.ID == 1
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
        _valid.Username = AnotherValidUserName;
        Assert.That(_valid.Username, Is.EqualTo(AnotherValidUserName));
    }

    [Test]
    public void Setter_InvalidUsername_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Username = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Username, Is.EqualTo(ValidUserName));
        
        Assert.That(() => _valid.Username = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Username, Is.EqualTo(ValidUserName));

        Assert.That(() => _valid.Username = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Username, Is.EqualTo(ValidUserName));
        
        Assert.That(() => _valid.Username = "inv", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Username, Is.EqualTo(ValidUserName));

        Assert.That(() => _valid.Username = "tooLongOfUsernameToBeValidAndSomeMore",
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Username, Is.EqualTo(ValidUserName));
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
        _valid.Password = "NewVal1dPassword";
        Assert.That(_valid.Password, Is.EqualTo("NewVal1dPassword"));
    }

    [Test]
    public void Setter_InvalidPassword_ThrowsInvalidPasswordException()
    {
        Assert.That(() => _valid.Password = null, Throws.TypeOf<NullReferenceException>());
        Assert.That(_valid.Password, Is.EqualTo(ValidPassword));
        
        Assert.That(() => _valid.Password = "", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_valid.Password, Is.EqualTo(ValidPassword));
        
        Assert.That(() => _valid.Password = " ", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_valid.Password, Is.EqualTo(ValidPassword));
        
        Assert.That(() => _valid.Password = "inv", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_valid.Password, Is.EqualTo(ValidPassword));
        
        Assert.That(() => _valid.Password = "nouppercasepassword1", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_valid.Password, Is.EqualTo(ValidPassword));
        
        Assert.That(() => _valid.Password = "NOLOWERCASEPASSWORD1", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_valid.Password, Is.EqualTo(ValidPassword));
        
        Assert.That(() => _valid.Password = "NoDigitPassword", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_valid.Password, Is.EqualTo(ValidPassword));
        
        Assert.That(() => _valid.Password = "SuperMegaLongPasswordThatIsTooLongToBeValid123456", Throws.TypeOf<InvalidPasswordException>());
        Assert.That(_valid.Password, Is.EqualTo(ValidPassword));
    }
    
    [Test]
    public void Setter_ValidEmail_SetsEmail()
    {
        _valid.Email = "NewValidEmail@pjwstk.edu.pl";
        Assert.That(_valid.Email, Is.EqualTo("NewValidEmail@pjwstk.edu.pl"));
    }

    [Test]
    public void Setter_InvalidEmail_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Email = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_valid.Email, Is.EqualTo(ValidEmail));
        
        Assert.That(() => _valid.Email = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_valid.Email, Is.EqualTo(ValidEmail));
        
        Assert.That(() => _valid.Email = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_valid.Email, Is.EqualTo(ValidEmail));
        
        Assert.That(() => _valid.Email = "email.with.no.at", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_valid.Email, Is.EqualTo(ValidEmail));
        
        Assert.That(() => _valid.Email = "email@nodot", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(_valid.Email, Is.EqualTo(ValidEmail));
    }

    [Test]
    public void Setter_ValidProfilePictureUrl_SetsProfilePictureUrl()
    {
        _valid.ProfilePictureUrl = "https://i.pinimg.com/736x/79/a3/16/79a3168cf52edca304ff32db46e0f888.jpg";
        Assert.That(_valid.ProfilePictureUrl, Is.EqualTo("https://i.pinimg.com/736x/79/a3/16/79a3168cf52edca304ff32db46e0f888.jpg"));
    }

    [Test]
    public void Setter_InvalidProfilePictureUrl_SetsDefaultProfilePicture()
    {
        _valid.ProfilePictureUrl = null;
        Assert.That(_valid.ProfilePictureUrl, Is.EqualTo(DefaultProfilePictureUrl));
        
        _valid.ProfilePictureUrl = "";
        Assert.That(_valid.ProfilePictureUrl, Is.EqualTo(DefaultProfilePictureUrl));
        
        _valid.ProfilePictureUrl = " ";
        Assert.That(_valid.ProfilePictureUrl, Is.EqualTo(DefaultProfilePictureUrl));
        
        _valid.ProfilePictureUrl = "invalid.path";
        Assert.That(_valid.ProfilePictureUrl, Is.EqualTo(DefaultProfilePictureUrl));
    }

    [Test]
    public void Setter_ValidRating_SetsRating()
    {
        _valid.Rating = 8.0;
        Assert.That(_valid.Rating, Is.EqualTo(8.0));
    }
    
    [Test]
    public void Setter_InvalidRating_SetsRatingWithinBounds()
    {
        _valid.Rating = -5.0;
        Assert.That(_valid.Rating, Is.EqualTo(0));
        _valid.Rating = 100500;
        Assert.That(_valid.Rating, Is.EqualTo(10.0));
    }

    [Test]
    public void AddLanguage_ValidLanguage_AddsLanguageToList()
    {
        _valid.AddLanguage(Language.English);
        Assert.That(_valid.Languages.Contains(Language.English));
        Assert.That(_valid.Languages.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddLanguage_DuplicateLanguage_DoesNotAddTwice()
    {
        _valid.AddLanguage(Language.Ukrainian);
        _valid.AddLanguage(Language.Ukrainian);
        Assert.That(_valid.Languages.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void RemoveLanguage_ExistingLanguage_RemovesFromLanguages()
    {
        _valid.AddLanguage(Language.English);

        _valid.RemoveLanguage(Language.English);
        
        Assert.That(_valid.Languages.Contains(Language.English), Is.False);
        Assert.That(_valid.Languages.Count, Is.EqualTo(0));
    }

    [Test]
    public void RemoveLanguage_NonExistingLanguage_DoesNothing()
    {
        _valid.RemoveLanguage(Language.Mandarin); //was not added

        Assert.That(_valid.Languages.Contains(Language.Mandarin), Is.False);
        Assert.That(_valid.Languages.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetLanguages_ReturnsCopy()
    {
        _valid.AddLanguage(Language.Hungarian);

        var languages = _valid.Languages;

        //modify copy
        languages.Clear();

        Assert.That(_valid.Languages.Count, Is.EqualTo(1)); //original set unchanged
    }
    
    [Test]
    public void Follow_ValidAccount_AddsToFollowingsAndFollowers()
    {
        var validAccountToFollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);

        _valid.Follow(validAccountToFollow);
        
        Assert.That(_valid.Followings.Contains(validAccountToFollow));
        Assert.That(validAccountToFollow.Followers.Contains(_valid));
    }

    [Test]
    public void Unfollow_ValidAccount_RemovesFromFollowingsAndFollowers()
    {
        var validAccountToUnfollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);

        _valid.Follow(validAccountToUnfollow);

        _valid.Unfollow(validAccountToUnfollow);

        Assert.That(_valid.Followings.Contains(validAccountToUnfollow), Is.False);
        Assert.That(validAccountToUnfollow.Followers.Contains(_valid), Is.False);
    }
    
    [Test]
    public void Follow_SelfFollow_ThrowsInvalidOperationException()
    {
        Assert.That(() => _valid.Follow(_valid), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void Follow_DuplicateAccount_DoesNotAddTwice()
    {
        var validAccountToFollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);

        _valid.Follow(validAccountToFollow);
        _valid.Follow(validAccountToFollow);

        Assert.That(_valid.Followings.Count, Is.EqualTo(1));
        Assert.That(validAccountToFollow.Followers.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void Follow_NullAccount_ThrowsArgumentNullException()
    {
        Assert.That(() => _valid.Follow(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Unfollow_NullAccount_ThrowsArgumentNullException()
    {
        Assert.That(() => _valid.Unfollow(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Unfollow_NonFollowedAccount_DoesNothing()
    {
        var validAccountToUnfollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);

        _valid.Unfollow(validAccountToUnfollow);

        Assert.That(_valid.Followings.Contains(validAccountToUnfollow), Is.False);
        Assert.That(validAccountToUnfollow.Followers.Contains(_valid), Is.False);
    }

    [Test]
    public void GetFollowings_ReturnsCopy()
    {
        var validAccountToFollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);

        _valid.Follow(validAccountToFollow);
        var followings = _valid.Followings;

        //modify the returned copy
        followings.Clear();

        Assert.That(_valid.Followings.Count, Is.EqualTo(1)); //original set is unchanged
    }

    [Test]
    public void GetFollowers_ReturnsCopy()
    {
        var validAccountToFollow = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);

        validAccountToFollow.Follow(_valid);
        var followers = _valid.Followers;

        //modify the returned copy
        followers.Clear();

        Assert.That(_valid.Followers.Count, Is.EqualTo(1)); //original set is unchanged
    }

    [Test]
    public void JoinChatroom_ValidChatroom_AddsAccountToChatroom()
    {
        var validChatRoom = new ChatRoom("TestChat", "static/img/valid_img.jpg");

        _valid.JoinChatroom(validChatRoom);

        Assert.That(_valid.Chatrooms.Contains(validChatRoom));
        Assert.That(validChatRoom.Members.Contains(_valid));
    }
    
    [Test]
    public void LeaveChatroom_ValidChatroom_RemovesAccountFromChatroom()
    {
        var validChatRoom = new ChatRoom("TestChat", "static/img/valid_img.jpg");

        _valid.JoinChatroom(validChatRoom);

        _valid.LeaveChatroom(validChatRoom);

        Assert.That(_valid.Chatrooms.Contains(validChatRoom), Is.False);
        Assert.That(validChatRoom.Members.Contains(_valid), Is.False);
    }

    [Test]
    public void JoinChatroom_DuplicateChatroom_DoesNotAddTwice()
    {
        var chatRoom = new ChatRoom("TestChat", "static/img/valid_img.jpg");

        _valid.JoinChatroom(chatRoom);
        _valid.JoinChatroom(chatRoom);

        Assert.That(_valid.Chatrooms.Count, Is.EqualTo(1));
        Assert.That(chatRoom.Members.Count, Is.EqualTo(1));
    }

    [Test]
    public void LeaveChatroom_NonJoinedChatroom_DoesNothing()
    {
        var chatRoom = new ChatRoom("TestChat", "static/img/valid_img.jpg");

        _valid.LeaveChatroom(chatRoom);

        Assert.That(_valid.Chatrooms.Contains(chatRoom), Is.False);
        Assert.That(chatRoom.Members.Contains(_valid), Is.False);
    }

    [Test]
    public void JoinChatroom_NullChatroom_ThrowsArgumentNullException()
    {
        Assert.That(() => _valid.JoinChatroom(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void LeaveChatroom_NullChatroom_ThrowsArgumentNullException()
    {
        Assert.That(() => _valid.LeaveChatroom(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void GetChatrooms_ReturnsCopy()
    {
        var chatRoom = new ChatRoom("TestChat", "static/img/valid_img.jpg");

        _valid.JoinChatroom(chatRoom);

        var chatrooms = _valid.Chatrooms;

        chatrooms.Clear();

        Assert.That(_valid.Chatrooms.Count, Is.EqualTo(1));
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
        Account.RemoveInstanceFromExtent(_valid);
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