namespace get_a_way.Entities;

[Serializable]
public class Message
{
    public static List<Message> Extent = new List<Message>();

    public long ID { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }

    public Message()
    {
    }
}