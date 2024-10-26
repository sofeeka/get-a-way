namespace get_a_way.Entities.Places.Eatery;

[Serializable]
public class Eatery : Place
{
    public EateryType Type { get; set; }
    public Cuisine Cuisine { get; set; }
    public List<string> Menu { get; set; }
    public HashSet<DietaryOptions> DietaryOptions { get; set; }
    public bool ReservationRequired { get; set; }

    public Eatery()
    {
    }

    // TODO initialise class Place fields
    public Eatery(EateryType type, Cuisine cuisine, bool reservationRequired)
    {
        Type = type;
        Cuisine = cuisine;
        Menu = new List<string>();
        DietaryOptions = new HashSet<DietaryOptions>();
        ReservationRequired = reservationRequired;
    }
}