namespace get_a_way.Entities.Accounts;

[Serializable]
public class OwnerAccount : Account
{
    public OwnerAccount()
    {
    }

    public OwnerAccount(long id, string username, string password, string email, double tax) :
        base(id, username, password, email)
    {
        Tax = tax;
    }

    public double Tax { get; set; }
}