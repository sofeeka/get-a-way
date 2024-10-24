using get_a_way.Services;

namespace get_a_way.Entities;

[Serializable]
public class Message : IExtent<Message>
{
    public static List<Message> Extent = new List<Message>();
    
    public long ID { get;   set; }
    public string text { get;   set; }
    public DateTime timestamp { get;   set; }
}