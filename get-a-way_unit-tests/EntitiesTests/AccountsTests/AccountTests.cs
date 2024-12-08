using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.AccountsTests;

public class AccountTests
{
    private class TestAccount(string username, string password, string email) : Account(username, password, email);

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
    public void Setter_InvalidProfilePictureUrl_ReturnsDefaultProfilePicture()
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
    public void Setter_Languages_SetsCorrectValues()
    {
        var languages = new HashSet<Language> { Language.English, Language.Ukrainian, Language.Hungarian };
        _valid.Languages = languages;
        Assert.That(_valid.Languages, Is.EqualTo(languages));
    }

    [Test]
    public void Setter_Languages_IgnoresDuplicateValues()
    {
        var languagesWithDuplicates = new HashSet<Language> { Language.English, Language.Spanish, Language.English };
        _valid.Languages = languagesWithDuplicates;

        Assert.That(_valid.Languages.Count, Is.EqualTo(2)); // only two unique entries should be present
        Assert.That(_valid.Languages, Does.Contain(Language.English));
        Assert.That(_valid.Languages, Does.Contain(Language.Spanish));
    }

    [Test]
    public void AddLanguage_AddsLanguageToList()
    {
        _valid.AddLanguage(Language.English);
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