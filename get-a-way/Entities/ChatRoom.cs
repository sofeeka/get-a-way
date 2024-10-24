namespace get_a_way.Entities;

[Serializable]
public class ChatRoom
{
    public static List<ChatRoom> Extent = new List<ChatRoom>();

    public long ID { get; set; }
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
    public List<Account> Accounts { get; set; }

    public ChatRoom()
    {
    }

    public ChatRoom(long id, string name, string photoUrl, List<Account> accounts)
    {
        ID = id;
        Name = name;
        PhotoUrl = photoUrl;
        Accounts = accounts;
    }
}