using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.AccountsTests;

public class TravelerAccountTests
{
    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var traveler = new TravelerAccount("Username", "Password123", "traveler@pjwstk.edu.pl");
        
        //todo come up with a way to check correct id assignment
        //Assert.That(owner.ID, Is.EqualTo(1));
        Assert.That(traveler.Username, Is.EqualTo("Username"));
        Assert.That(traveler.Password, Is.EqualTo("Password123"));
        Assert.That(traveler.Email, Is.EqualTo("traveler@pjwstk.edu.pl"));
        Assert.That(traveler.ProfilePictureUrl, Is.EqualTo("static/img/default_profile_img.jpg"));
        Assert.That(traveler.Verified, Is.False);
        Assert.That(traveler.Rating, Is.EqualTo(10.0));
    }

    [Test]
    public void Constructor_InvalidUsername_ThrowsInvalidAttributeException()
    {
        Assert.That(() => 
                new TravelerAccount("N", "Password123", "traveler@pjwstk.edu.pl"), 
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => 
                new TravelerAccount("", "Password123", "traveler@pjwstk.edu.pl"), 
            Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void Constructor_DuplicateUsername_ThrowsInvalidAttributeException()
    {
        var owner1 = new TravelerAccount("UniqueName", "Password123", "traveler1@pjwstk.edu.pl");
        Assert.That(() => 
                new TravelerAccount("UniqueName", "AnotherPassword", "traveler2@pjwstk.edu.pl"), 
            Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void Constructor_InvalidPassword_ThrowsInvalidAttributeException()
    {
        Assert.That(() => 
                new TravelerAccount("ValidName", "short", "traveler@example.com"), 
            Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void Constructor_InvalidEmail_ThrowsInvalidAttributeException()
    {
        Assert.That(() => 
                new TravelerAccount("ValidName", "Password123", "invalidemail"), 
            Throws.TypeOf<InvalidAttributeException>());
    }
}