namespace get_a_way.Entities;

[Serializable]
public class ChatRoom
{
    public static List<ChatRoom> Extent = new List<ChatRoom>();

    public long ID { get;   set; }
    public string name { get;   set; }
    public string photoUrl { get;   set; }
    public List<Account> accounts { get;   set; }
}