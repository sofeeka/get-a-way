using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Attractions;
using get_a_way.Exceptions;
using NUnit.Framework;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests.AttractionsTests
{
    public class AttractionTests
    {
        private static readonly HashSet<OwnerAccount> Owners = new() { new OwnerAccount("Owner", "ValidPass123", "owner@example.com") };

        private const string ValidName = "Valid Attraction Name";
        private const string ValidLocation = "City Center";
        private const int ValidEntryFee = 20;
        private const int ValidMinimalAge = 18;
        private const string ValidDescription = "A well-described attraction.";
        private const string ValidActivityType = "Outdoor Sports";
        private const string ValidCulturalPeriod = "Renaissance Era";
        private const string ValidDressCode = "Formal Attire";

        private static readonly DateTime ValidOpenTime = DateTime.Parse("08:00");
        private static readonly DateTime ValidCloseTime = DateTime.Parse("18:00");
        private static readonly PriceCategory ValidPriceCategory = PriceCategory.Budget;
        private const bool ValidPetFriendly = true;

        [SetUp]
        public void SetUpEnvironment()
        {
            Place.ResetExtent();
        }

        [TearDown]
        public void TearDownEnvironment()
        {
            Place.ResetExtent();
        }

        [Test]
        public void Constructor_ValidAttributes_AssignsCorrectValues()
        {
            var attraction = Attraction.CreateAttraction(
                Owners,
                ValidName,
                ValidLocation,
                ValidOpenTime,
                ValidCloseTime,
                ValidPriceCategory,
                ValidPetFriendly,
                ValidEntryFee,
                ValidMinimalAge,
                ValidDescription,
                isActiveAttraction: true,
                activityType: ValidActivityType,
                isHistoricalAttraction: true,
                culturalPeriod: ValidCulturalPeriod,
                isNightLifeAttraction: true,
                dressCode: ValidDressCode
            );

            Assert.That(attraction.EntryFee, Is.EqualTo(ValidEntryFee));
            Assert.That(attraction.MinimalAge, Is.EqualTo(ValidMinimalAge));
            Assert.That(attraction.Description, Is.EqualTo(ValidDescription));
            Assert.That(attraction.IsActiveAttraction, Is.True);
            Assert.That(attraction.ActivityType, Is.EqualTo(ValidActivityType));
            Assert.That(attraction.IsHistoricalAttraction, Is.True);
            Assert.That(attraction.CulturalPeriod, Is.EqualTo(ValidCulturalPeriod));
            Assert.That(attraction.IsNightLifeAttraction, Is.True);
            Assert.That(attraction.DressCode, Is.EqualTo(ValidDressCode));
        }

        [Test]
        public void Constructor_NoRolesAssigned_ThrowsArgumentException()
        {
            Assert.That(() =>
                Attraction.CreateAttraction(
                    Owners,
                    ValidName,
                    ValidLocation,
                    ValidOpenTime,
                    ValidCloseTime,
                    ValidPriceCategory,
                    ValidPetFriendly,
                    ValidEntryFee,
                    ValidMinimalAge,
                    ValidDescription
                ),
                Throws.TypeOf<ArgumentException>()
                    .With.Message.EqualTo("At least one role must be assigned to the attraction."));
        }

        [Test]
        public void Constructor_InvalidRoleAttributes_ThrowsArgumentException()
        {
            Assert.That(() =>
                Attraction.CreateAttraction(
                    Owners,
                    ValidName,
                    ValidLocation,
                    ValidOpenTime,
                    ValidCloseTime,
                    ValidPriceCategory,
                    ValidPetFriendly,
                    ValidEntryFee,
                    ValidMinimalAge,
                    ValidDescription,
                    isActiveAttraction: true
                ),
                Throws.TypeOf<ArgumentException>()
                    .With.Message.EqualTo("ActivityType must be provided for Active attractions."));
        }

        [Test]
        public void BaseAttributes_Validation_WorksCorrectly()
        {
            Assert.Throws<InvalidAttributeException>(() =>
                Attraction.CreateAttraction(
                    Owners,
                    "",
                    ValidLocation,
                    ValidOpenTime,
                    ValidCloseTime,
                    ValidPriceCategory,
                    ValidPetFriendly,
                    ValidEntryFee,
                    ValidMinimalAge,
                    ValidDescription,
                    isActiveAttraction: true,
                    activityType: ValidActivityType
                ));

            Assert.Throws<InvalidAttributeException>(() =>
                Attraction.CreateAttraction(
                    Owners,
                    ValidName,
                    "",
                    ValidOpenTime,
                    ValidCloseTime,
                    ValidPriceCategory,
                    ValidPetFriendly,
                    ValidEntryFee,
                    ValidMinimalAge,
                    ValidDescription,
                    isActiveAttraction: true,
                    activityType: ValidActivityType
                ));
        }

        [Test]
        public void ToString_ContainsCorrectInformation()
        {
            var attraction = Attraction.CreateAttraction(
                Owners,
                ValidName,
                ValidLocation,
                ValidOpenTime,
                ValidCloseTime,
                ValidPriceCategory,
                ValidPetFriendly,
                ValidEntryFee,
                ValidMinimalAge,
                ValidDescription,
                isActiveAttraction: true,
                activityType: ValidActivityType,
                isHistoricalAttraction: true,
                culturalPeriod: ValidCulturalPeriod
            );

            var result = attraction.ToString();
            Assert.That(result, Does.Contain(ValidName));
            Assert.That(result, Does.Contain(ValidLocation));
            Assert.That(result, Does.Contain(ValidActivityType));
            Assert.That(result, Does.Contain(ValidCulturalPeriod));
        }
    }
}
