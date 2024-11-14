using get_a_way.Entities.Accounts;
using get_a_way.Entities.Review;

namespace get_a_way_unit_tests.EntitiesTests.ReviewTests;

public class ReviewTests
{
    private Review _valid;

    private const string ValidComment = "Valid comment";
    private const double ValidRating = 9.5;

    [SetUp]
    public void SetUpEnvironment()
    {
        Account.ResetExtent();
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
}