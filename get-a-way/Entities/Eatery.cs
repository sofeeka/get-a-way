namespace get_a_way.Entities;

[Serializable]
public class Eatery : Place
{
    public bool Cuisine { get; set; }
    public List<string> Menu { get; set; }
    public List<string> DietaryOptions { get; set; }
    public bool ReservationRequired { get; set; }

    public Eatery()
    {
    }

    // TODO initialise class Place fields
    public Eatery(bool cuisine, List<string> menu, List<string> dietaryOptions, bool reservationRequired)
    {
        this.Cuisine = cuisine;
        this.Menu = menu;
        this.DietaryOptions = dietaryOptions;
        this.ReservationRequired = reservationRequired;
    }
}