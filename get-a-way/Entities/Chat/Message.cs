using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Chat;

[Serializable]
public class Message : IExtent<Message>
{
    public static List<Message> Extent = new List<Message>();
    
    private static long IdCounter = 0;
    public long ID { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }

    public Message()
    {
    }

    public Message(string text, DateTime timestamp)
    {
        ID = ++IdCounter;
        Text = text;
        Timestamp = timestamp;
    }

    public List<Message> GetExtentCopy()
    {
        return new List<Message>(Extent);
    }

    public void AddInstanceToExtent(Message instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        Extent.Add((instance));
    }

    public void RemoveInstanceFromExtent(Message instance)
    {
        Extent.Remove(instance);
    }
}