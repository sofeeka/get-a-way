namespace get_a_way.Entities;

public class Eateries
{
    public bool cusine { get; private set; }

    public List<string> menu { get; private set; }

    public List<string> dietaryOptions { get; private set; }

    public bool reservationRequired { get; private set; }

    public Eateries(bool cusine, List<string> menu, List<string> dietaryOptions, bool reservationRequired)
    {
        this.cusine = cusine;
        this.menu = menu;
        this.dietaryOptions = dietaryOptions;
        this.reservationRequired = reservationRequired;
    }
}