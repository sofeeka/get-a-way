namespace get_a_way.Entities.Accounts;

[Serializable]
public class TravelerAccount : Account
{
    public TravelerAccount() : this(0)
    {
    }

    public TravelerAccount(long id, string username = "", string password = "", string email = "") : base(id, username,
        password, email)
    {
    }
}