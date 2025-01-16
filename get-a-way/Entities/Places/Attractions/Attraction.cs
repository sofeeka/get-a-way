using System.Xml.Serialization;
using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;

namespace get_a_way.Entities.Places.Attractions;

[Serializable]
public class Attraction : Place
{
    private int _entryFee; // currency will be taken from international/local
    private int _minimalAge;
    private List<string> _events;
    private string _description;
    
    private bool _isActiveAttraction;
    private bool _isHistoricalAttraction;
    private bool _isNightLifeAttraction;

    // role-specific attributes
    private string _activityType;        // Active 
    private string _culturalPeriod;     // Historical 
    private string _dressCode;         // NightLife

    public int EntryFee
    {
        get => _entryFee;
        set => _entryFee = ValidateEntryFee(value);
    }

    public int MinimalAge
    {
        get => _minimalAge;
        set => _minimalAge = ValidateMinimalAge(value);
    }

    public List<string> Events
    {
        get => _events;
        set => _events = ValidateEvents(value);
    }

    public string Description
    {
        get => _description;
        set => _description = ValidateDescription(value);
    }
    
    public bool IsActiveAttraction
    {
        get => _isActiveAttraction;
        set => _isActiveAttraction = value;
    }

    public bool IsHistoricalAttraction => _isHistoricalAttraction;
    public bool IsNightLifeAttraction => _isNightLifeAttraction;

    public string ActivityType => _activityType;
    public string CulturalPeriod => _culturalPeriod;
    public string DressCode => _dressCode;


    public Attraction()
    {
    }

    public static Attraction CreateAttraction(
        HashSet<OwnerAccount> owners,
        string name,
        string location,
        DateTime openTime,
        DateTime closeTime,
        PriceCategory priceCategory,
        bool petFriendly,
        int entryFee,
        int minimalAge,
        string description,
        bool isActiveAttraction = false,
        string activityType = null,
        bool isHistoricalAttraction = false,
        string culturalPeriod = null,
        bool isNightLifeAttraction = false,
        string dressCode = null)
    {
        ValidateRoles(isActiveAttraction, isHistoricalAttraction, isNightLifeAttraction);
        ValidateRoleAttributes(isActiveAttraction, activityType, isHistoricalAttraction, culturalPeriod, isNightLifeAttraction, dressCode);

        return new Attraction
        {
            _entryFee = ValidateEntryFee(entryFee),
            _minimalAge = ValidateMinimalAge(minimalAge),
            _events = new List<string>(),
            _description = ValidateDescription(description),

            _isActiveAttraction = isActiveAttraction,
            _activityType = activityType,

            _isHistoricalAttraction = isHistoricalAttraction,
            _culturalPeriod = culturalPeriod,

            _isNightLifeAttraction = isNightLifeAttraction,
            _dressCode = dressCode
        };
    }
    
    private static void ValidateRoles(bool isActive, bool isHistorical, bool isNightLife)
    {
        if (!isActive && !isHistorical && !isNightLife)
            throw new ArgumentException("At least one role must be assigned to the attraction.");
    }

    private static void ValidateRoleAttributes(
        bool isActive,
        string activityType,
        bool isHistorical,
        string culturalPeriod,
        bool isNightLife,
        string dressCode)
    {
        if (isActive && string.IsNullOrWhiteSpace(activityType))
            throw new ArgumentException("ActivityType must be provided for Active attractions.");

        if (isHistorical && string.IsNullOrWhiteSpace(culturalPeriod))
            throw new ArgumentException("CulturalPeriod must be provided for Historical attractions.");

        if (isNightLife && string.IsNullOrWhiteSpace(dressCode))
            throw new ArgumentException("DressCode must be provided for NightLife attractions.");
    }

    private static int ValidateEntryFee(int value)
    {
        if (value < 0)
            throw new InvalidAttributeException("Entry fee can not be negative value.");
        return value;
    }

    private static int ValidateMinimalAge(int value)
    {
        if (value is < 0 or > 120)
            throw new InvalidAttributeException("Minimal age should be between 0 and 120.");
        return value;
    }

    private List<string> ValidateEvents(List<string> values)
    {
        if (values == null)
            throw new InvalidAttributeException("Event list can not be null.");

        foreach (var value in values)
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidEventException("Event cannot be null or empty.");

        return values;
    }

    private static string ValidateDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Description can not be empty.");

        if (value.Length is < 10 or > 1000)
            throw new InvalidAttributeException("Description length must be between 10 and 1000 characters.");

        return value;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Entry Fee: {EntryFee}\n" +
               $"Minimal Age: {MinimalAge}\n" +
               $"Events: {string.Join(", ", Events)}\n" +
               $"Description: {Description}\n" +
               $"Roles: {(IsActiveAttraction ? "Active " : "")}{(IsHistoricalAttraction ? "Historical " : "")}{(IsNightLifeAttraction ? "NightLife" : "")}\n" +
               (IsActiveAttraction ? $"Activity Type: {ActivityType}\n" : "") +
               (IsHistoricalAttraction ? $"Cultural Period: {CulturalPeriod}\n" : "") +
               (IsNightLifeAttraction ? $"Dress Code: {DressCode}\n" : "");
    }
}