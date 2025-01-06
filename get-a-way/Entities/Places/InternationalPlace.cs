using get_a_way.Entities.Accounts;

namespace get_a_way.Entities.Places;

[Serializable]
public class InternationalPlace : Place
{
    public InternationalPlace()
    {
    }

    public InternationalPlace(HashSet<OwnerAccount> owners, string name, string location, DateTime openTime, DateTime closeTime,
        PriceCategory priceCategory, bool petFriendly)
        : base(owners, name, location, openTime, closeTime, priceCategory, petFriendly)
    {
    }

    public override string ToString()
    {
        return base.ToString();
    }
}