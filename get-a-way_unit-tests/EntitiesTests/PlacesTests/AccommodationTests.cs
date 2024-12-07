using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Accommodation;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.PlacesTests;

public class AccommodationTests
{
    private Accommodation _valid;

    // place fields
    private const string ValidName = "ValidName";
    private const string ValidLocation = "Some Location";
    private static DateTime _validOpenTime = new DateTime();
    private static DateTime _validCloseTime = new DateTime();
    private PriceCategory _priceCategory = PriceCategory.Free;
    private static bool _petFriendly = true;

    // accommodation fields
    private static AccommodationType _accommodationType = AccommodationType.Apartment;
    private static HashSet<Amenity> _validAmenities;
    private int _validMaxPeople;

    [SetUp]
    public void SetUpEnvironment()
    {
        Place.ResetExtent();
        _valid = new Accommodation(ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            _priceCategory, _petFriendly, _accommodationType, _validMaxPeople);

        _validAmenities = new HashSet<Amenity>();
        _validAmenities.Add(Amenity.Iron);
        _validAmenities.Add(Amenity.WheelchairAccessible);
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var accommodation = new Accommodation(ValidName, ValidLocation, _validOpenTime, _validCloseTime,
            _priceCategory, _petFriendly, _accommodationType, _validMaxPeople);

        // ID == 2 because _valid.ID == 1
        Assert.That(accommodation.ID, Is.EqualTo(2));

        Assert.That(accommodation.Type, Is.EqualTo(_accommodationType));
        Assert.That(accommodation.Amenities, Is.Empty);
        Assert.That(accommodation.MaxPeople, Is.EqualTo(_validMaxPeople));
    }

    [Test]
    public void Setter_ValidAccommodationType_SetsAccommodationType()
    {
        _valid.Type = AccommodationType.Hostel;
        Assert.That(_valid.Type, Is.EqualTo(AccommodationType.Hostel));
    }

    [Test]
    public void Setter_ValidAmenities_SetsAmenities()
    {
        _valid.Amenities = _validAmenities;
        Assert.That(_valid.Amenities, Is.EqualTo(_validAmenities));
    }

    [Test]
    public void Setter_InvalidAmenities_ThrowsInvalidAttributeException()
    {
        _valid.Amenities = _validAmenities;

        Assert.That(() => _valid.Amenities = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Amenities, Is.EqualTo(_validAmenities));
    }

    [Test]
    public void Setter_DuplicateAmenities_DoesNotChangeSizeOfAmenitiesSet()
    {
        _valid.Amenities.Add(Amenity.Dishwasher);
        int count = _valid.Amenities.Count;
        _valid.Amenities.Add(Amenity.Dishwasher);
        Assert.That(() => _valid.Amenities.Count, Is.EqualTo(count));
    }

    [Test]
    public void Setter_ValidMaxPeople_SetsMaxPeople()
    {
        _valid.MaxPeople = _validMaxPeople;
        Assert.That(_valid.MaxPeople, Is.EqualTo(_validMaxPeople));
    }

    [Test]
    public void Setter_InvalidMaxPeople_ThrowsInvalidAttributeException()
    {
        _valid.MaxPeople = _validMaxPeople;

        Assert.That(() => _valid.MaxPeople = 0, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.MaxPeople, Is.EqualTo(_validMaxPeople));

        Assert.That(() => _valid.MaxPeople = -1, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.MaxPeople, Is.EqualTo(_validMaxPeople));
    }
}