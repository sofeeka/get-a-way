using System.Numerics;

namespace get_a_way.Entities;

public class OwnerAccount : Account
{
    public OwnerAccount(long id, string username, string password, string email, BigInteger tax) : base(id, username, password, email)
    {
        this.tax = tax;
    }

    public BigInteger tax { get; private set; }

}