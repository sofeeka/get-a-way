using get_a_way.Entities.Review;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.ReviewTests;

public class ReviewTests
{
    private Review _valid;

    private const string ValidComment = "Valid comment";
    private const double ValidRating = 9.5;

    [SetUp]
    public void SetUpEnvironment()
    {
        Review.ResetExtent();
        _valid = new Review(ValidRating, ValidComment);
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var traveler = new Review(ValidRating, ValidComment);

        // ID == 2 because _valid.ID == 1
        Assert.That(traveler.ID, Is.EqualTo(2));

        Assert.That(traveler.Rating, Is.EqualTo(ValidRating));
        Assert.That(traveler.Comment, Is.EqualTo(ValidComment));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var test1 = new Review(ValidRating, ValidComment);
        var test2 = new Review(ValidRating, ValidComment);

        Assert.That(test2.ID - test1.ID, Is.EqualTo(1));
    }

    [Test]
    public void Setter_ValidRating_SetsRating()
    {
        _valid.Rating = 8.0;
        Assert.That(_valid.Rating, Is.EqualTo(8.0));
    }

    [Test]
    public void Setter_InvalidRating_DoesNotChangeRating()
    {
        _valid.Rating = -5.0;
        Assert.That(() => _valid.Rating, Is.EqualTo(0.0));

        _valid.Rating = 100500;
        Assert.That(() => _valid.Rating, Is.EqualTo(10.0));
    }

    [Test]
    public void Setter_ValidComment_SetsComment()
    {
        _valid.Comment = "Another valid comment";
        Assert.That(_valid.Comment, Is.EqualTo("Another valid comment"));
    }

    [Test]
    public void Setter_InvalidComment_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Comment = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Comment, Is.EqualTo(ValidComment));

        Assert.That(() => _valid.Comment = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Comment, Is.EqualTo(ValidComment));

        Assert.That(() => _valid.Comment = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Comment, Is.EqualTo(ValidComment));

        var hugeComment = new string('a', 1001);

        Assert.That(() => _valid.Comment = hugeComment,
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Comment, Is.EqualTo(ValidComment));
    }

    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = Review.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestReview = new Review(ValidRating, ValidComment);
        Assert.That(Review.GetExtentCopy().Count, Is.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_OnRemovalOfInstance_DecreasesExtentCount()
    {
        int count = Review.GetExtentCopy().Count;
        Review.RemoveInstanceFromExtent(_valid);
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