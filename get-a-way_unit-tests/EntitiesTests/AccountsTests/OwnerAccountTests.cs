using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.AccountsTests;

public class OwnerAccountTests
{
    [Test]
    public void SetTax_ValidValue_SetsTax()
    {
        OwnerAccount.Tax = 20;
        Assert.That(OwnerAccount.Tax, Is.EqualTo(20));
    }

    [Test]
    public void SetTax_InvalidValue_ThrowsInvalidAttributeException()
    {
        Assert.That(() => OwnerAccount.Tax = -5, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => OwnerAccount.Tax = 105, Throws.TypeOf<InvalidAttributeException>());
    }
}