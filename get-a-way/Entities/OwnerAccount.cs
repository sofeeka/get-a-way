using System.Numerics;

namespace get_a_way.Entities;

[Serializable]
public class OwnerAccount : Account
{
    public OwnerAccount() : this(0)
    {
    }

    public OwnerAccount(long id, string username = "", string password = "", string email = "", double tax = 0.0) :
        base(id, username, password, email)
    {
        Tax = tax;
    }

    public double Tax { get; set; }
}