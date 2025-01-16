using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests;

public class MultiAspectPlaceTests
{
    private class LocalPlace(
        HashSet<OwnerAccount> owners,
        string name,
        string location,
        DateTime openTime,
        DateTime closeTime,
        PriceCategory priceCategory,
        bool petFriendly,
        bool localLanguageOnly) : Place(owners, name, location, openTime, closeTime, priceCategory, petFriendly,
        localLanguageOnly: localLanguageOnly);

    private LocalPlace _localPlace;

    private class InternationalPlace(
        HashSet<OwnerAccount> owners,
        string name,
        string location,
        DateTime openTime,
        DateTime closeTime,
        PriceCategory priceCategory,
        bool petFriendly,
        HashSet<Currency> currencies, 
        HashSet<Language> languages,
        Country country) : Place(owners, name, location, openTime, closeTime, priceCategory, petFriendly, currencies, languages, country);

    private static readonly HashSet<Currency> currencies = new HashSet<Currency>();
    private static readonly HashSet<Language> languages = new HashSet<Language>();
    private InternationalPlace _internationalPlace;

    private const string ValidName = "ValidName";
    private const string ValidLocation = "ValidLocation";
    private static readonly DateTime ValidOpenTime = DateTime.Today.AddHours(8);
    private static readonly DateTime ValidCloseTime = DateTime.Today.AddHours(21);
    private const PriceCategory ValidPriceCategory = PriceCategory.Moderate;
    private const bool ValidPetFriendly = true;

    private static readonly HashSet<OwnerAccount> Owners = new HashSet<OwnerAccount>();
    private static readonly OwnerAccount DummyOwner =
        new OwnerAccount("PlaceOwner", "ValidPassword123", "validemail@pjwstk.edu.pl");

    [SetUp]
    public void SetUpEnvironment()
    {
        languages.Add(Language.Ukrainian);
        currencies.Add(Currency.EUR);

        Owners.Add(DummyOwner);

        _localPlace = new LocalPlace(Owners, ValidName, ValidLocation, ValidOpenTime, ValidCloseTime, ValidPriceCategory,
            ValidPetFriendly, true);
        
        _internationalPlace = new InternationalPlace(Owners, ValidName, ValidLocation, ValidOpenTime, ValidCloseTime, ValidPriceCategory,
            ValidPetFriendly, currencies: currencies, languages: languages, Country.Ukraine);
    }

    [TearDown]
    public void TearDownEnvironment()
    {
        Place.ResetExtent();
        Account.ResetExtent();
    }

    [Test]
    public void ConstructorLocalPlace_ValidAttributes_AssignsCorrectValues()
    {
        LocalPlace local = new LocalPlace(Owners, ValidName, ValidLocation, ValidOpenTime, ValidCloseTime, ValidPriceCategory,
            ValidPetFriendly, localLanguageOnly: true);

        Assert.That(local.LocalLanguageOnly, Is.True);
        Assert.That(local.Languages, Is.EqualTo(null));
        Assert.That(local.Currencies, Is.EqualTo(null));
        Assert.That(local.Country, Is.EqualTo(null));
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        InternationalPlace international = new InternationalPlace(Owners, ValidName, ValidLocation, ValidOpenTime, ValidCloseTime, ValidPriceCategory,
            ValidPetFriendly, currencies, languages, Country.Argentina);

        Assert.That(international.Languages, Is.EqualTo(languages));
        Assert.That(international.Currencies, Is.EqualTo(currencies));
        Assert.That(international.Country, Is.EqualTo(Country.Argentina));
    }
    
    [Test]
    public void Setter_InvalidCurrencies_Null_ThrowsNullReferenceException()
    {
        Assert.That(() => _internationalPlace.Currencies = null, Throws.TypeOf<NullReferenceException>());
        Assert.That(() => _internationalPlace.Currencies, Is.EqualTo(currencies));
    }

    [Test]
    public void Setter_InvalidCurrencies_Empty_ThrowsInvalidOperationException()
    {
        HashSet<Currency> temp = new HashSet<Currency>();
        Assert.That(() => _internationalPlace.Currencies = temp, Throws.TypeOf<InvalidOperationException>());
        Assert.That(() => _internationalPlace.Currencies, Is.EqualTo(currencies));
    }

    [Test]
    public void Setter_InvalidLanguages_Null_ThrowsNullReferenceException()
    {
        Assert.That(() => _internationalPlace.Languages = null, Throws.TypeOf<NullReferenceException>());
        Assert.That(() => _internationalPlace.Languages, Is.EqualTo(languages));
    }

    [Test]
    public void Setter_InvalidLanguages_Empty_ThrowsInvalidOperationException()
    {
        HashSet<Language> temp = new HashSet<Language>();
        Assert.That(() => _internationalPlace.Languages = temp, Throws.TypeOf<InvalidOperationException>());
        Assert.That(() => _internationalPlace.Languages, Is.EqualTo(languages));
    }

}