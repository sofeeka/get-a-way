using get_a_way.Entities;
using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests;

public class AccountTests
{
    private class TestAccount : Account
    {
        public TestAccount(long id, string username, string password, string email) : base(id, username, password, email)
        {
        }
    }
    
    
    
    [Test]
    public void Constructor_ValidAttributes_ShouldReturnCorrectInformation()
    {
        var account = new TestAccount(1, "testUser", "testPassword123", "test@pjwstk.edu.pl");

        Assert.That(account.ID, Is.EqualTo(1));
        Assert.That(account.Username, Is.EqualTo("testUser"));
        Assert.That(account.Password, Is.EqualTo("testPassword123"));
        Assert.That(account.Email, Is.EqualTo("test@pjwstk.edu.pl"));
        Assert.That(account.ProfilePictureUrl, Is.EqualTo("static/img/default_profile_img.jpg"));
    }
    
    [Test]
    public void Constructor_InvalidUsername_ShouldThrowInvalidAttributeException()
    {
        Assert.Throws<InvalidAttributeException>(() => new TestAccount(1, "", "testPassword123", "test@pjwstk.edu.pl"));
        Assert.Throws<InvalidAttributeException>(() => new TestAccount(1, "abc", "testPassword123", "test@pjwstk.edu.pl")); // Less than 5 characters
        Assert.Throws<InvalidAttributeException>(() => new TestAccount(1, "verylongusernamethatiswaytoolong", "testPassword123", "test@pjwstk.edu.pl")); // More than 30 characters
    }
    
    [Test]
    public void Constructor_DuplicateUsername_ShouldThrowInvalidAttributeException()
    {
        var account1 = new TestAccount(1, "uniqueUser", "testPassword123", "test@pjwstk.edu.pl");
        Assert.Throws<InvalidAttributeException>(() => new TestAccount(2, "uniqueUser", "anotherPassword123", "test1@pjwstk.edu.pl")); // Duplicate username
    }

    [Test]
    public void Constructor_InvalidPassword_ShouldThrowInvalidAttributeException()
    {
        Assert.Throws<InvalidAttributeException>(() => new TestAccount(1, "validUser", "short", "test@pjwstk.edu.pl")); // Less than 8 characters
    }

    [Test]
    public void Constructor_InvalidEmail_ShouldThrowInvalidAttributeException()
    {
        Assert.Throws<InvalidAttributeException>(() => new TestAccount(1, "validUser", "testPassword123", "")); // Empty email
        Assert.Throws<InvalidAttributeException>(() => new TestAccount(1, "validUser", "testPassword123", "invalidemail")); // Invalid email format
    }

  /*  [Test]
    public void Constructor_AddingInstanceToExtendOnCreation_ShouldStoreCorrectInstances()
    {
        var account1 = new TestAccount(1, "s28299", "password1", "email1@pjwstk.edu.pl");
        var account2 = new TestAccount(2, "s28399", "password2", "email2@pjwstk.edu.pl");

        var extent = account1.GetExtentUnmodifiable();

        Assert.That(extent.Count, Is.EqualTo(2));
        Assert.That(extent[0].Username, Is.EqualTo("s28299"));
        Assert.That(extent[1].Username, Is.EqualTo("s28399"));
    }

    [Test]
    public void RemoveInstanceFromExtent_ShouldRemoveInstances()
    {
        var account1 = new TestAccount(1, "user1", "password1", "email1@pjwstk.edu.pl");
        account1.RemoveInstanceFromExtent(account1);
        var extent = account1.GetExtentUnmodifiable();

        Assert.That(extent.Count, Is.EqualTo(0));
    }
    
        
    [Test]
    public void AddInstanceToExtent_NullInstance_ThrowsAddingNullInstanceException()
    {
        var account = new TestAccount(1, "user1", "password1", "email1@pjwstk.edu.pl");

        Assert.Throws<AddingNullInstanceException>(() => account.AddInstanceToExtent(null));
    } */
    
}