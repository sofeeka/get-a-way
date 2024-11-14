using get_a_way.Entities.Accounts;
using get_a_way.Entities.Trip;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.TripTests;

public class TripTests
{
    private static Trip _valid;

    private static readonly Account ValidAccount =
        new OwnerAccount("ValidName", "Password123", "valid.email@pjwstk.edu.pl");

    private static readonly DateTime Now = DateTime.Now;
    private static readonly DateTime ValidDate = new DateTime(Now.Year - 1, Now.Month, Now.Day);
    private const string ValidDescription = "Valid Description";
    private const string ValidPictureUrl = "https://valid/image.png";
    private List<string> _validPictureUrls = new List<string>();

    [SetUp]
    public void SetUpEnvironment()
    {
        Trip.ResetExtent();
        Account.ResetExtent();
        _valid = new Trip(ValidAccount, ValidDate, TripType.Friends, ValidDescription);

        _validPictureUrls = new List<string>();
        _validPictureUrls.Add(ValidPictureUrl);
        _validPictureUrls.Add(ValidPictureUrl);

        _valid.PictureUrls = _validPictureUrls;
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var trip = new Trip(ValidAccount, ValidDate, TripType.Friends, ValidDescription);

        // ID == 2 because _valid.ID == 1
        Assert.That(trip.ID, Is.EqualTo(2));

        Assert.That(trip.Date, Is.EqualTo(ValidDate));
        Assert.That(trip.TripType, Is.EqualTo(TripType.Friends));
        Assert.That(trip.PictureUrls, Is.Empty);
        Assert.That(trip.Description, Is.EqualTo(ValidDescription));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var test1 = new Trip(ValidAccount, ValidDate, TripType.Friends, ValidDescription);
        var test2 = new Trip(ValidAccount, ValidDate, TripType.Friends, ValidDescription);

        Assert.That(test2.ID - test1.ID, Is.EqualTo(1));
    }

    [Test]
    public void Setter_ValidDate_SetsDate()
    {
        var anotherValidDate = new DateTime(Now.Year - 2, Now.Month, Now.Day);
        _valid.Date = anotherValidDate;
        Assert.That(_valid.Date, Is.EqualTo(anotherValidDate));
    }

    [Test]
    public void Setter_InvalidDate_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Date = new DateTime(Now.Year + 1, Now.Month, Now.Day),
            Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Date, Is.EqualTo(ValidDate));
    }

    [Test]
    public void Setter_ValidPictureUrls_SetsPictureUrls()
    {
        _valid.PictureUrls = _validPictureUrls;
        Assert.That(_valid.PictureUrls, Is.EqualTo(_validPictureUrls));
    }

    [Test]
    public void Setter_InvalidPictureUrlsList_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.PictureUrls = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Description, Is.EqualTo(ValidDescription));

        List<string> urls = new List<string>();

        for (int i = 0; i < 12; i++)
            urls.Add(ValidPictureUrl);

        Assert.That(() => _valid.PictureUrls = urls, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.PictureUrls, Is.EqualTo(_validPictureUrls));
    }

    [Test]
    public void Setter_InvalidPictureUrl_ThrowsInvalidAttributeException()
    {
        List<string> urls = new List<string>();

        urls.Add(null);
        Assert.That(() => _valid.PictureUrls = urls, Throws.TypeOf<InvalidPictureUrlException>());
        Assert.That(() => _valid.PictureUrls, Is.EqualTo(_validPictureUrls));

        urls.Clear();
        urls.Add("");
        Assert.That(() => _valid.PictureUrls = urls, Throws.TypeOf<InvalidPictureUrlException>());
        Assert.That(() => _valid.PictureUrls, Is.EqualTo(_validPictureUrls));

        urls.Clear();
        urls.Add(" ");
        Assert.That(() => _valid.PictureUrls = urls, Throws.TypeOf<InvalidPictureUrlException>());
        Assert.That(() => _valid.PictureUrls, Is.EqualTo(_validPictureUrls));

        urls.Clear();
        urls.Add("invalid path");
        Assert.That(() => _valid.PictureUrls = urls, Throws.TypeOf<InvalidPictureUrlException>());
        Assert.That(() => _valid.PictureUrls, Is.EqualTo(_validPictureUrls));

        urls.Clear();
        urls.Add("https://not/image/extention");
        Assert.That(() => _valid.PictureUrls = urls, Throws.TypeOf<InvalidPictureUrlException>());
        Assert.That(() => _valid.PictureUrls, Is.EqualTo(_validPictureUrls));
    }

    [Test]
    public void Setter_ValidDescription_SetsDescription()
    {
        _valid.Description = "Some proper new description.";
        Assert.That(_valid.Description, Is.EqualTo("Some proper new description."));
    }

    [Test]
    public void Setter_InvalidDescription_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Description = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Description, Is.EqualTo(ValidDescription));

        Assert.That(() => _valid.Description = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Description, Is.EqualTo(ValidDescription));

        Assert.That(() => _valid.Description = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Description, Is.EqualTo(ValidDescription));

        string hugeDescription = """
                                 Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula
                                 eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient
                                 montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu,
                                 pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel,
                                 aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis
                                 vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras
                                 dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo
                                 ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in,
                                 viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet.
                                 Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper
                                 ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum
                                 rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc,
                                 blandit vel, luctus pulvinar, hendre
                                 """;

        Assert.That(() => _valid.Description = hugeDescription, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Description, Is.EqualTo(ValidDescription));
    }

    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = Trip.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestInstance = new Trip(ValidAccount, ValidDate, TripType.Family, ValidDescription);
        Assert.That(Trip.GetExtentCopy().Count, Is.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_OnRemovalOfInstance_DecreasesExtentCount()
    {
        int count = Trip.GetExtentCopy().Count;
        Trip.RemoveInstanceFromExtent(_valid);
        Assert.That(Trip.GetExtentCopy().Count, Is.EqualTo(count - 1));
    }

    [Test]
    public void GetExtentCopy_DoesNotReturnActualExtent()
    {
        // addresses are different
        Assert.True(Trip.GetExtentCopy() != Trip.GetExtent());
    }

    [Test]
    public void ResetExtent_ClearsExtent()
    {
        Trip.ResetExtent();
        Assert.That(Trip.GetExtent().Count, Is.EqualTo(0));
    }
}