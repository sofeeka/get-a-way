using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Chat;

[Serializable]
public class Message : IExtent<Message>
{
    private static List<Message> _extent = new List<Message>();

    private static long IdCounter = 0;

    private long _id;
    private string _text;
    private DateTime _timestamp;

    public long ID
    {
        get => _id;
        set => _id = value;
    }

    public string Text
    {
        get => _text;
        set => _text = ValidateText(value);
    }

    public DateTime Timestamp
    {
        get => _timestamp;
        set => _timestamp = ValidateTimestamp(value);
    }

    public Message()
    {
    }

    public Message(string text, DateTime timestamp)
    {
        ID = ++IdCounter;
        Text = text;
        Timestamp = timestamp;

        AddInstanceToExtent(this);
    }

    private string ValidateText(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Text of a message cannot be empty");

        return value;
    }

    private DateTime ValidateTimestamp(DateTime value)
    {
        if (value > DateTime.Now)
            throw new InvalidAttributeException("Message cannot be sent in the future");

        return value;
    }

    public List<Message> GetExtentCopy()
    {
        return new List<Message>(_extent);
    }

    public void AddInstanceToExtent(Message instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        _extent.Add(instance);
    }

    public void RemoveInstanceFromExtent(Message instance)
    {
        _extent.Remove(instance);
    }

    public static List<Message> GetExtent()
    {
        return _extent;
    }
}