using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.AccountsTests;

public class OwnerAccountTests
{

    private OwnerAccount _ownerAccount = new OwnerAccount(ValidUserName, ValidPassword, ValidEmail);
        
    private const string ValidUserName = "ValidUserName";
    private const string ValidPassword = "ValidPassword123";
    private const string ValidEmail = "validemail@pjwstk.edu.pl";

    [Test]
    public void SetTax_ValidValue_SetsTax()
    {
        _ownerAccount.Tax = 20;
        Assert.That(_ownerAccount.Tax, Is.EqualTo(20));
    }

    [Test]
    public void SetTax_InvalidValue_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _ownerAccount.Tax = -5, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _ownerAccount.Tax = 105, Throws.TypeOf<InvalidAttributeException>());
    }
}