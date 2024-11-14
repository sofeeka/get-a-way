using get_a_way.Entities.Accounts;
using get_a_way.Entities.Trip;

namespace get_a_way_unit_tests.EntitiesTests.TripTests;

public class TripTests
{
    private Trip _valid;

    private static DateTime _validDate = new DateTime().AddDays(-4);

    [SetUp]
    public void SetUpEnvironment()
    {
        Account.ResetExtent();
        _valid = new Trip(new OwnerAccount("ValidName", "Password123", "valid.email@pjwstk.edu.pl"),
            new DateTime().AddDays(-4), TripType.Friends, "Some description");
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var date = new DateTime().AddDays(-4);

        var trip = new Trip(new OwnerAccount("ValidName", "Password123", "valid.email@pjwstk.edu.pl"),
            date, TripType.Friends, "Some description");

        // ID is 2 because _valid.ID == 1
        Assert.That(trip.ID, Is.EqualTo(2));

        Assert.That(trip.Date, Is.EqualTo(date));
        Assert.That(trip.TripType, Is.EqualTo(TripType.Friends));
        Assert.That(trip.Pictures, Is.Empty);
        Assert.That(trip.Description, Is.EqualTo("Some description"));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var test1 = new Trip(new OwnerAccount("ValidName", "Password123", "valid.email@pjwstk.edu.pl"),
            new DateTime().AddDays(-4), TripType.Friends, "Some description");

        var test2 = new Trip(new OwnerAccount("ValidName", "Password123", "valid.email@pjwstk.edu.pl"),
            new DateTime().AddDays(-4), TripType.Friends, "Some description");

        Assert.That(test2.ID - test1.ID, Is.EqualTo(1));
    }
}