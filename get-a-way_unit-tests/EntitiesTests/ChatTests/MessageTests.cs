using get_a_way.Entities.Chat;
using get_a_way.Exceptions;

namespace get_a_way_unit_tests.EntitiesTests.ChatTests;

public class MessageTests
{
    private Message _valid;

    private const string ValidText = "Valid text of a message.";
    private static readonly DateTime Now = DateTime.Now;
    private static readonly DateTime ValidTimestamp = new DateTime(Now.Year - 1, Now.Month, Now.Day);

    [SetUp]
    public void SetUpEnvironment()
    {
        Message.ResetExtent();
        _valid = new Message(ValidText, ValidTimestamp);
    }

    [Test]
    public void Constructor_ValidAttributes_AssignsCorrectValues()
    {
        var message = new Message(ValidText, ValidTimestamp);

        Assert.That(message.Text, Is.EqualTo(ValidText));
        Assert.That(message.Timestamp, Is.EqualTo(ValidTimestamp));

        // ID is 2 because _valid.ID == 1
        Assert.That(message.ID, Is.EqualTo(2));
    }

    [Test]
    public void Constructor_NewInstanceCreation_IncrementsId()
    {
        var message1 = new Message(ValidText, ValidTimestamp);
        var message2 = new Message(ValidText, ValidTimestamp);

        Assert.That(message2.ID - message1.ID, Is.EqualTo(1));
    }

    [Test]
    public void AddInstanceToExtent_OnCreationOfNewInstance_IncreasesExtentCount()
    {
        int count = Message.GetExtentCopy().Count;
        // AddInstanceToExtent is called in constructor
        var newTestInstance = new Message(ValidText, ValidTimestamp);
        Assert.That(Message.GetExtentCopy().Count, Is.EqualTo(count + 1));
    }

    [Test]
    public void RemoveInstanceFromExtent_OnRemovalOfInstance_DecreasesExtentCount()
    {
        int count = Message.GetExtentCopy().Count;
        Message.RemoveInstanceFromExtent(_valid);
        Assert.That(Message.GetExtentCopy().Count, Is.EqualTo(count - 1));
    }

    [Test]
    public void GetExtentCopy_DoesNotReturnActualExtent()
    {
        // addresses are different
        Assert.True(Message.GetExtentCopy() != Message.GetExtent());
    }

    [Test]
    public void ResetExtent_ClearsExtent()
    {
        Message.ResetExtent();
        Assert.That(Message.GetExtent().Count, Is.EqualTo(0));
    }

    [Test]
    public void Setter_ValidText_SetsText()
    {
        _valid.Text = "Another valid text";
        Assert.That(_valid.Text, Is.EqualTo("Another valid text"));

        _valid.Text = "v";
        Assert.That(_valid.Text, Is.EqualTo("v"));

        _valid.Text = "50";
        Assert.That(_valid.Text, Is.EqualTo("50"));

        _valid.Text = "_";
        Assert.That(_valid.Text, Is.EqualTo("_"));
    }

    [Test]
    public void Setter_InvalidText_ThrowsInvalidAttributeException()
    {
        Assert.That(() => _valid.Text = null, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Text, Is.EqualTo(ValidText));

        Assert.That(() => _valid.Text = "", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Text, Is.EqualTo(ValidText));

        Assert.That(() => _valid.Text = " ", Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Text, Is.EqualTo(ValidText));

        string tooLong = """
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
        Assert.That(() => _valid.Text = tooLong, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Text, Is.EqualTo(ValidText));
    }

    [Test]
    public void Setter_ValidTimestamp_SetsTimestamp()
    {
        DateTime newTimestamp = new DateTime(Now.Year - 2, Now.Month, Now.Day);
        _valid.Timestamp = newTimestamp;
        Assert.That(_valid.Timestamp, Is.EqualTo(newTimestamp));
    }

    [Test]
    public void Setter_InvalidTimestamp_ThrowsInvalidAttributeException()
    {
        DateTime invalidTimestamp = new DateTime(Now.Year + 1, Now.Month, Now.Day);

        Assert.That(() => _valid.Timestamp = invalidTimestamp, Throws.TypeOf<InvalidAttributeException>());
        Assert.That(() => _valid.Timestamp, Is.EqualTo(ValidTimestamp));
    }
}