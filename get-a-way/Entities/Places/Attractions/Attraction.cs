using System;
using System.Collections.Generic;
using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;

namespace get_a_way.Entities.Places.Attractions
{
    [Serializable]
    public class Attraction : Place
    {
        private int _entryFee;
        private int _minimalAge;
        private List<string> _events;
        private string _description;

        // roles
        private readonly bool _isActiveAttraction;
        private readonly bool _isHistoricalAttraction;
        private readonly bool _isNightLifeAttraction;

        // role-specific attributes
        private readonly string _activityType;       // active 
        private readonly string _culturalPeriod;    // historical 
        private readonly string _dressCode;         // nightLife 

        public int EntryFee => _entryFee;
        public int MinimalAge => _minimalAge;
        public List<string> Events => new List<string>(_events);
        public string Description => _description;

        public bool IsActiveAttraction => _isActiveAttraction;
        public bool IsHistoricalAttraction => _isHistoricalAttraction;
        public bool IsNightLifeAttraction => _isNightLifeAttraction;

        public string ActivityType => _activityType;
        public string CulturalPeriod => _culturalPeriod;
        public string DressCode => _dressCode;

        private Attraction(
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
            bool isActiveAttraction,
            string activityType,
            bool isHistoricalAttraction,
            string culturalPeriod,
            bool isNightLifeAttraction,
            string dressCode)
            : base(owners, name, location, openTime, closeTime, priceCategory, petFriendly)
        {
            _entryFee = ValidateEntryFee(entryFee);
            _minimalAge = ValidateMinimalAge(minimalAge);
            _description = ValidateDescription(description);
            _events = new List<string>();

            _isActiveAttraction = isActiveAttraction;
            _activityType = activityType;

            _isHistoricalAttraction = isHistoricalAttraction;
            _culturalPeriod = culturalPeriod;

            _isNightLifeAttraction = isNightLifeAttraction;
            _dressCode = dressCode;
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

            return new Attraction(
                owners,
                name,
                location,
                openTime,
                closeTime,
                priceCategory,
                petFriendly,
                entryFee,
                minimalAge,
                description,
                isActiveAttraction,
                activityType,
                isHistoricalAttraction,
                culturalPeriod,
                isNightLifeAttraction,
                dressCode
            );
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
                throw new InvalidAttributeException("Entry fee cannot be negative.");
            return value;
        }

        private static int ValidateMinimalAge(int value)
        {
            if (value < 0 || value > 120)
                throw new InvalidAttributeException("Minimal age must be between 0 and 120.");
            return value;
        }

        private static string ValidateDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 10 || value.Length > 1000)
                throw new InvalidAttributeException("Description must be between 10 and 1000 characters.");
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
}
