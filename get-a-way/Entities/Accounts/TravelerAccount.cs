namespace get_a_way.Entities.Accounts;

[Serializable]
public class TravelerAccount : Account
{
    public TravelerAccount()
    {
    }

    public TravelerAccount(string username, string password, string email) : base(username,
        password, email)
    {
    }

    public override string ToString()
    {
        return base.ToString();
    }
}