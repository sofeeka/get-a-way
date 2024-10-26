using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities;

[Serializable]
public class ChatRoom : IExtent<ChatRoom>
{
    public static List<ChatRoom> Extent = new List<ChatRoom>();

    public long ID { get; set; }
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
    public List<Account> Accounts { get; set; }

    public ChatRoom()
    {
    }

    public ChatRoom(long id, string name, string photoUrl)
    {
        ID = id;
        Name = name;
        PhotoUrl = photoUrl;
        Accounts = new List<Account>();
    }

    public List<ChatRoom> GetExtentCopy()
    {
        return new List<ChatRoom>(Extent);
    }

    public void AddInstanceToExtent(ChatRoom instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        Extent.Add((instance));
    }

    public void RemoveInstanceFromExtent(ChatRoom instance)
    {
        Extent.Remove(instance);
    }
}