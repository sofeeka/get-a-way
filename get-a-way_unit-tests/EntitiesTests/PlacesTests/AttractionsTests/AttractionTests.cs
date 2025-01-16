using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Attractions;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests.AttractionsTests;

public class AttractionTests
{
    private Attraction _valid;

    private const string ValidName = "ValidAttraction";
    private const string ValidLocation = "Some Location";
    private static readonly DateTime ValidOpenTime = new DateTime(2025, 1, 1, 9, 0, 0);
    private static readonly DateTime ValidCloseTime = new DateTime(2025, 1, 1, 17, 0, 0);
    private const PriceCategory ValidPriceCategory = PriceCategory.Budget;
    private const bool PetFriendly = true;
    private static readonly HashSet<OwnerAccount> Owners = new() { new OwnerAccount("Owner", "ValidPass123", "owner@valid.com") };

    private const int ValidEntryFee = 20;
    private const int ValidMinimalAge = 18;
    private const string ValidDescription = "A valid attraction description.";

    private const string ValidActivityType = "Outdoor Adventure";
    private const string ValidCulturalPeriod = "Medieval Era";
    private const string ValidDressCode = "All black";

    [SetUp]
    public void SetUpEnvironment()
    {
        Place.ResetExtent();

        _valid = Attraction.CreateAttraction(
            Owners,
            ValidName,
            ValidLocation,
            ValidOpenTime,
            ValidCloseTime,
            ValidPriceCategory,
            PetFriendly,
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
    }

    [TearDown]
    public void TearDownEnvironment()
    {
        Place.ResetExtent();
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        Assert.That(_valid.EntryFee, Is.EqualTo(ValidEntryFee));
        Assert.That(_valid.MinimalAge, Is.EqualTo(ValidMinimalAge));
        Assert.That(_valid.Events, Is.Empty);
        Assert.That(_valid.Description, Is.EqualTo(ValidDescription));

        Assert.That(_valid.IsActiveAttraction, Is.True);
        Assert.That(_valid.ActivityType, Is.EqualTo(ValidActivityType));

        Assert.That(_valid.IsHistoricalAttraction, Is.True);
        Assert.That(_valid.CulturalPeriod, Is.EqualTo(ValidCulturalPeriod));

        Assert.That(_valid.IsNightLifeAttraction, Is.True);
        Assert.That(_valid.DressCode, Is.EqualTo(ValidDressCode));
    }

    [Test]
    public void Constructor_InvalidRoles_ThrowsArgumentException()
    {
        Assert.That(() =>
            Attraction.CreateAttraction(
                Owners,
                ValidName,
                ValidLocation,
                ValidOpenTime,
                ValidCloseTime,
                ValidPriceCategory,
                PetFriendly,
                ValidEntryFee,
                ValidMinimalAge,
                ValidDescription
            ), Throws.TypeOf<ArgumentException>());
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
                PetFriendly,
                ValidEntryFee,
                ValidMinimalAge,
                ValidDescription,
                isActiveAttraction: true
                // no activityType
            ), Throws.TypeOf<ArgumentException>());

        Assert.That(() =>
            Attraction.CreateAttraction(
                Owners,
                ValidName,
                ValidLocation,
                ValidOpenTime,
                ValidCloseTime,
                ValidPriceCategory,
                PetFriendly,
                ValidEntryFee,
                ValidMinimalAge,
                ValidDescription,
                isHistoricalAttraction: true
                // no culturalPeriod
            ), Throws.TypeOf<ArgumentException>());

        Assert.That(() =>
            Attraction.CreateAttraction(
                Owners,
                ValidName,
                ValidLocation,
                ValidOpenTime,
                ValidCloseTime,
                ValidPriceCategory,
                PetFriendly,
                ValidEntryFee,
                ValidMinimalAge,
                ValidDescription,
                isNightLifeAttraction: true
                // no dressCode
            ), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void Constructor_InvalidAttributes_ThrowsInvalidAttributeException()
    {
        Assert.That(() =>
            Attraction.CreateAttraction(
                Owners,
                "",
                ValidLocation,
                ValidOpenTime,
                ValidCloseTime,
                ValidPriceCategory,
                PetFriendly,
                ValidEntryFee,
                ValidMinimalAge,
                ValidDescription,
                isActiveAttraction: true,
                activityType: ValidActivityType
            ), Throws.TypeOf<InvalidAttributeException>());

        Assert.That(() =>
            Attraction.CreateAttraction(
                Owners,
                ValidName,
                ValidLocation,
                ValidOpenTime,
                ValidCloseTime,
                ValidPriceCategory,
                PetFriendly,
                -1, // invalid entry fee
                ValidMinimalAge,
                ValidDescription,
                isActiveAttraction: true,
                activityType: ValidActivityType
            ), Throws.TypeOf<InvalidAttributeException>());

        Assert.That(() =>
            Attraction.CreateAttraction(
                Owners,
                ValidName,
                ValidLocation,
                ValidOpenTime,
                ValidCloseTime,
                ValidPriceCategory,
                PetFriendly,
                ValidEntryFee,
                -1, // invalid minimal age
                ValidDescription,
                isActiveAttraction: true,
                activityType: ValidActivityType
            ), Throws.TypeOf<InvalidAttributeException>());
    }

    [Test]
    public void RoleAttributes_AccessingUnavailableRoles_ThrowsInvalidOperationException()
    {
        var attraction = Attraction.CreateAttraction(
            Owners,
            ValidName,
            ValidLocation,
            ValidOpenTime,
            ValidCloseTime,
            ValidPriceCategory,
            PetFriendly,
            ValidEntryFee,
            ValidMinimalAge,
            ValidDescription,
            isActiveAttraction: true,
            activityType: ValidActivityType
        );

        Assert.That(() => attraction.CulturalPeriod, Throws.TypeOf<InvalidOperationException>());
        Assert.That(() => attraction.DressCode, Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void RoleFlags_ImmutableAfterCreation()
    {
        Assert.That(_valid.IsActiveAttraction, Is.True);
        Assert.That(_valid.IsHistoricalAttraction, Is.True);
        Assert.That(_valid.IsNightLifeAttraction, Is.True);

        // direct modification should not be possible
        Assert.Throws<NotSupportedException>(() => _valid.IsActiveAttraction = false);
    }

    [Test]
    public void Events_AddingValidEvent_AddsSuccessfully()
    {
        _valid.Events.Add("New Event");
        Assert.That(_valid.Events.Contains("New Event"));
    }

    [Test]
    public void Events_AddingInvalidEvent_ThrowsInvalidEventException()
    {
        Assert.That(() => _valid.Events.Add(null), Throws.TypeOf<InvalidEventException>());
        Assert.That(() => _valid.Events.Add(""), Throws.TypeOf<InvalidEventException>());
    }
}
