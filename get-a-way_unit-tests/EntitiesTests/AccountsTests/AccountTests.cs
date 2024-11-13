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
    private const string ValidPassword = "ValidPassword";
    private const string ValidEmail = "validemail@pjwstk.edu.pl";

    [SetUp]
    public void SetUpEnvironment()
    {
        Account.Reset();
        _valid = new TestAccount(ValidUserName, ValidPassword, ValidEmail);
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        // todo
        // do not use ValidUserName, will throw DuplicateUsernameException
        var traveler = new TestAccount(AnotherValidUserName, ValidPassword, ValidEmail);

        Assert.That(traveler.Username, Is.EqualTo(AnotherValidUserName));
        Assert.That(traveler.Password, Is.EqualTo(ValidPassword));
        Assert.That(traveler.Email, Is.EqualTo(ValidEmail));

        // todo add placeholder for empty pfp
        Assert.That(traveler.ProfilePictureUrl, Is.EqualTo("static/img/default_profile_img.jpg"));

        Assert.That(traveler.ID, Is.EqualTo(2));

        Assert.That(traveler.Verified, Is.False);
        Assert.That(traveler.Rating, Is.EqualTo(10.0));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var test1 = new TestAccount("username1", "password", "traveler1@pjwstk.edu.pl");
        var test2 = new TestAccount("username2", "password", "2traveler@pjwstk.edu.pl");

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
        Assert.That(() => _valid.Username = "inv", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Username, Is.EqualTo(ValidUserName));

        Assert.That(() => _valid.Username = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Username, Is.EqualTo(ValidUserName));

        Assert.That(() => _valid.Username = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Username, Is.EqualTo(ValidUserName));

        Assert.That(() => _valid.Username = "tooLongOfUsernameToBeValidAndSomeMore",
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Username, Is.EqualTo(ValidUserName));
    }

    [Test]
    public void Constructor_DuplicateUsername_ThrowsInvalidAttributeException()
    {
        // todo create exception
        Assert.That(() =>
                new TestAccount(ValidUserName, ValidPassword, ValidEmail),
            Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void Constructor_InvalidPassword_ThrowsInvalidAttributeException()
    {
        // todo add more tests for upper / lower case + special characters
        Assert.That(() =>
                new TestAccount("ValidName", "short", "traveler@example.com"),
            Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void Constructor_InvalidEmail_ThrowsInvalidAttributeException()
    {
        // todo check for duplicate emails
        Assert.That(() =>
                new TestAccount("ValidName", "Password123", "invalidemail"),
            Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void Setter_InvalidAttributes_ThrowsInvalidAttributeException()
    {
        // todo move to separate tests
        // Assert.That(() => _valid.Username = "inv", Throws.TypeOf<InvalidAttributeException>());
        // Assert.That(_valid.Username, Is.EqualTo("ValidName"));
        //
        // Assert.That(() => _valid.Password = "inv", Throws.TypeOf<InvalidAttributeException>());
        // Assert.That(_valid.Password, Is.EqualTo("Password123"));
        //
        // Assert.That(() => _valid.Email = "inv", Throws.TypeOf<InvalidAttributeException>());
        // Assert.That(_valid.Email, Is.EqualTo("traveler@example.com"));
        //
        // _valid.Rating = -2;
        // Assert.That(_valid.Rating, Is.EqualTo(0));
        //
        // _valid.Rating = 100;
        // Assert.That(_valid.Rating, Is.EqualTo(10));
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
        Account.Reset();
        Assert.That(Account.GetExtent().Count, Is.EqualTo(0));
    }
}