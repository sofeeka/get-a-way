using get_a_way.Exceptions;

namespace get_a_way.Entities.Accounts;

[Serializable]
public class OwnerAccount : Account
{
    public static double Tax { get; set; } = 15;
    
    public OwnerAccount()
    {
    }

    public OwnerAccount(string username, string password, string email) :
        base(username, password, email)
    {
    }
    
    public static void SetTax(double newTax)
    {
        if (newTax < 0)
        {
            throw new InvalidAttributeException("Tax value cannot be negative.");
        }
        Tax = newTax;
    }

}