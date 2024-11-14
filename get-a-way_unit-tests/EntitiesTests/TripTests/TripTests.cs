using get_a_way.Entities.Accounts;
using get_a_way.Entities.Trip;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.TripTests;

public class TripTests
{
    private static Trip _valid;

    private static readonly Account ValidAccount =
        new OwnerAccount("ValidName", "Password123", "valid.email@pjwstk.edu.pl");

    private static readonly DateTime Now = DateTime.Now;
    private static readonly DateTime ValidDate = new DateTime(Now.Year - 1, Now.Month, Now.Day);

    [SetUp]
    public void SetUpEnvironment()
    {
        Trip.ResetExtent();
        Account.ResetExtent();
        _valid = new Trip(ValidAccount, ValidDate, TripType.Friends, "Some description");
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var trip = new Trip(ValidAccount, ValidDate, TripType.Friends, "Some description");

        // ID is 2 because _valid.ID == 1
        Assert.That(trip.ID, Is.EqualTo(2));

        Assert.That(trip.Date, Is.EqualTo(ValidDate));
        Assert.That(trip.TripType, Is.EqualTo(TripType.Friends));
        Assert.That(trip.Pictures, Is.Empty);
        Assert.That(trip.Description, Is.EqualTo("Some description"));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var test1 = new Trip(ValidAccount, ValidDate, TripType.Friends, "Some description");
        var test2 = new Trip(ValidAccount, ValidDate, TripType.Friends, "Some description");

        Assert.That(test2.ID - test1.ID, Is.EqualTo(1));
    }

    [Test]
    public void Setter_ValidDate_SetsDate()
    {
        var anotherValidDate = new DateTime(Now.Year - 2, Now.Month, Now.Day);
        _valid.Date = anotherValidDate;
        Assert.That(_valid.Date, Is.EqualTo(anotherValidDate));
    }

    [Test]
    public void Setter_InvalidDate_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Date = new DateTime(Now.Year + 1, Now.Month, Now.Day),
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Date, Is.EqualTo(ValidDate));
    }
}