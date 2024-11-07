using System.Xml.Serialization;
using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Chat;

[Serializable]
public class ChatRoom : IExtent<ChatRoom>
{
    private static List<ChatRoom> _extent = new List<ChatRoom>();

    private static long IdCounter = 0;

    private long _id;
    private string _name;
    private string _photoUrl;

    public long ID
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = ValidateName(value);
    }

    public string PhotoUrl
    {
        get => _photoUrl;
        set => _photoUrl = value;
    }

    [XmlArray("Members")]
    [XmlArrayItem("Member")]
    public List<Account> Members { get; set; }

    public ChatRoom()
    {
    }

    public ChatRoom(string name, string photoUrl)
    {
        ID = ++IdCounter;
        Name = name;
        PhotoUrl = photoUrl;
        Members = new List<Account>();

        AddInstanceToExtent(this);
    }


    private string ValidateName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Name of a group cannot be empty");

        return value;
    }

    public List<ChatRoom> GetExtentCopy()
    {
        return new List<ChatRoom>(_extent);
    }

    public void AddInstanceToExtent(ChatRoom instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        _extent.Add((instance));
    }

    public void RemoveInstanceFromExtent(ChatRoom instance)
    {
        _extent.Remove(instance);
    }

    public static List<ChatRoom> GetExtent()
    {
        return _extent;
    }
}