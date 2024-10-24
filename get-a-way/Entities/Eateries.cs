namespace get_a_way.Entities;

[Serializable]
public class Eateries
{
    public bool cusine { get;   set; }

    public List<string> menu { get;   set; }

    public List<string> dietaryOptions { get;   set; }

    public bool reservationRequired { get;   set; }

    public Eateries()
    {
    }

    public Eateries(bool cusine, List<string> menu, List<string> dietaryOptions, bool reservationRequired)
    {
        this.cusine = cusine;
        this.menu = menu;
        this.dietaryOptions = dietaryOptions;
        this.reservationRequired = reservationRequired;
    }
}