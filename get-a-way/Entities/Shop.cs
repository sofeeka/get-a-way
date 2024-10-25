namespace get_a_way.Entities;

[Serializable]
public class Shop : Place
{
    public bool OnlineOrderAvailability { get; set; }
    public List<string> HolidaySpecials { get; set; }

    public Shop()
    {
    }

    public Shop(bool onlineOrderAvailability, List<string> holidaySpecials)
    {
        OnlineOrderAvailability = onlineOrderAvailability;
        HolidaySpecials = holidaySpecials;
    }
}