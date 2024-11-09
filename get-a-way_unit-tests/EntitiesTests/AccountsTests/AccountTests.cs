using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.AccountsTests;

public class AccountTests
{
    private class TestAccount(string username, string password, string email) : Account(username, password, email);

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var traveler = new TestAccount("Username", "Password", "traveler@pjwstk.edu.pl");

        //todo come up with a way to check correct id assignment
        //Assert.That(owner.ID, Is.EqualTo(1));

        Assert.That(traveler.Username, Is.EqualTo("Username"));
        Assert.That(traveler.Password, Is.EqualTo("Password"));
        Assert.That(traveler.Email, Is.EqualTo("traveler@pjwstk.edu.pl"));

        // todo add placeholder for empty pfp
        // Assert.That(traveler.ProfilePictureUrl, Is.EqualTo("static/img/default_profile_img.jpg"));

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
    public void Constructor_InvalidUsername_ThrowsInvalidAttributeException()
    {
        Assert.That(() =>
                new TestAccount("N", "Password123", "traveler@pjwstk.edu.pl"),
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() =>
                new TestAccount("", "Password123", "traveler@pjwstk.edu.pl"),
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() =>
                new TestAccount(null, "Password123", "traveler@pjwstk.edu.pl"),
            Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void Constructor_DuplicateUsername_ThrowsInvalidAttributeException()
    {
        var owner1 = new TestAccount("UniqueName", "Password123", "traveler1@pjwstk.edu.pl");
        Assert.That(() =>
                new TestAccount("UniqueName", "AnotherPassword", "traveler2@pjwstk.edu.pl"),
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
        var valid = new TestAccount("ValidName", "Password123", "traveler@example.com");

        Assert.That(() => valid.Username = "inv", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(valid.Username, Is.EqualTo("ValidName"));

        Assert.That(() => valid.Password = "inv", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(valid.Password, Is.EqualTo("Password123"));
        
        Assert.That(() => valid.Email = "inv", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(valid.Email, Is.EqualTo("traveler@example.com"));

        valid.Rating = -2;
        Assert.That(valid.Rating, Is.EqualTo(0));

        valid.Rating = 100;
        Assert.That(valid.Rating, Is.EqualTo(10));
    }
}