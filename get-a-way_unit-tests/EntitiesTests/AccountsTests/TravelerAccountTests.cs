using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.AccountsTests;

public class TravelerAccountTests
{
    [Test]
    public void Constructor_InstanceAddedToExtent()
    {
        var traveler = new TravelerAccount("Username", "Password123", "traveler@pjwstk.edu.pl");
        Assert.That(Account.GetExtent().Contains(traveler));
    }
}