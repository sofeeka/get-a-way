using get_a_way;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Shop;
using get_a_way.Entities.Review;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.ReviewTests;

public class ReviewTests
{
    private Review ValidReview;

    private const string ValidComment = "Valid comment";
    private const double ValidRating = 9.5;

    private static readonly TravelerAccount ValidTraveler =
        new TravelerAccount("ReviewTestTraveler", "Password123", "traveler.email@pjwstk.edu.pl");

    private static readonly HashSet<OwnerAccount> Owners = new HashSet<OwnerAccount>();

    private static readonly OwnerAccount DummyOwner =
        new OwnerAccount("ValidOwnerName", "ValidPassword123", "validemail@pjwstk.edu.pl");

    private static readonly Place ValidPlace = new Shop(Owners, "ValidName", "ValidLocation",
        DateTime.Today.AddHours(7), DateTime.Today.AddHours(20), PriceCategory.Expensive, true,
        ShopType.Mall, false);

    [SetUp]
    public void SetUpEnvironment()
    {
        ValidReview = new Review(ValidTraveler, ValidPlace, ValidRating, ValidComment);
    }

    [TearDown]
    public void TearDownEnvironment()
    {
        Database.Reset();
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var traveler = new Review(ValidTraveler, ValidPlace, ValidRating, ValidComment);

        Assert.That(traveler.Rating, Is.EqualTo(ValidRating));
        Assert.That(traveler.Comment, Is.EqualTo(ValidComment));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var test1 = new Review(ValidTraveler, ValidPlace, ValidRating, ValidComment);
        var test2 = new Review(ValidTraveler, ValidPlace, ValidRating, ValidComment);

        Assert.That(test2.ID - test1.ID, Is.EqualTo(1));
    }

    [Test]
    public void Constructor_ValidTraveler_AddsTravelerAndReverseConnection()
    {
        Assert.That(ValidReview.Traveler, Is.EqualTo(ValidTraveler)); // Traveler was added on constructor
        Assert.That(ValidTraveler.Reviews, Does.Contain(ValidReview)); // reverse connection was added
    }

    [Test]
    public void Constructor_ValidPlace_AddsPlaceAndReverseConnection()
    {
        Assert.That(ValidReview.Place, Is.EqualTo(ValidPlace)); // Place was added on constructor
        Assert.That(ValidPlace.Reviews, Does.Contain(ValidReview)); // reverse connection was added
    }

    [Test]
    public void Constructor_NullTraveler_ThrowsArgumentNullException()
    {
        Assert.That(() => new Review(null, ValidPlace, ValidRating, ValidComment),
            Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Constructor_NullPlace_ThrowsArgumentNullException()
    {
        Assert.That(() => new Review(ValidTraveler, null, ValidRating, ValidComment),
            Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void Setter_ValidRating_SetsRating()
    {
        ValidReview.Rating = 8.0;
        Assert.That(ValidReview.Rating, Is.EqualTo(8.0));
    }

    [Test]
    public void Setter_InvalidRating_DoesNotChangeRating()
    {
        ValidReview.Rating = -5.0;
        Assert.That(() => ValidReview.Rating, Is.EqualTo(0.0));

        ValidReview.Rating = 100500;
        Assert.That(() => ValidReview.Rating, Is.EqualTo(10.0));
    }

    [Test]
    public void Setter_ValidComment_SetsComment()
    {
        ValidReview.Comment = "Another valid comment";
        Assert.That(ValidReview.Comment, Is.EqualTo("Another valid comment"));
    }

    [Test]
    public void Setter_InvalidComment_ThrowsInvalidAttributeException()
    {
        Assert.That(() => ValidReview.Comment = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => ValidReview.Comment, Is.EqualTo(ValidComment));

        Assert.That(() => ValidReview.Comment = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => ValidReview.Comment, Is.EqualTo(ValidComment));

        Assert.That(() => ValidReview.Comment = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => ValidReview.Comment, Is.EqualTo(ValidComment));

        var hugeComment = new string('a', 1001);

        Assert.That(() => ValidReview.Comment = hugeComment,
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => ValidReview.Comment, Is.EqualTo(ValidComment));
    }

    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = Review.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestReview = new Review(ValidTraveler, ValidPlace, ValidRating, ValidComment);
        Assert.That(Review.GetExtentCopy().Count, Is.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_OnRemovalOfInstance_DecreasesExtentCount()
    {
        int count = Review.GetExtentCopy().Count;
        Review.RemoveInstanceFromExtent(ValidReview);
        Assert.That(Review.GetExtentCopy().Count, Is.EqualTo(count - 1));
    }

    [Test]
    public void GetExtentCopy_DoesNotReturnActualExtent()
    {
        // addresses are different
        Assert.True(Review.GetExtentCopy() != Review.GetExtent());
    }

    [Test]
    public void ResetExtent_ClearsExtent()
    {
        Review.ResetExtent();
        Assert.That(Review.GetExtent().Count, Is.EqualTo(0));
    }
}