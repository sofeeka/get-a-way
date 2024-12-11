using System.Text.RegularExpressions;
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
    private string? _photoUrl;
    
    private HashSet<Account> _members;

    private static string defaultImage = "https://i.pinimg.com/736x/a2/de/85/a2de85ffccbdc7267ef8bf801b56a747.jpg";

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

    public string? PhotoUrl
    {
        get => _photoUrl;
        set
        {
            if (value != null) _photoUrl = ValidatePhotoUrl(value);
        }
    }
    
    [XmlArray("Members")]
    [XmlArrayItem("Member")]
    public HashSet<Account> Members => new HashSet<Account>(_members);

    public ChatRoom()
    {
        _members = new HashSet<Account>();
    }

    public ChatRoom(string name, string photoUrl) : this()
    {
        ID = ++IdCounter;
        Name = name;
        PhotoUrl = photoUrl;

        AddInstanceToExtent(this);
    }

    private string ValidateName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Name of a group cannot be empty.");

        if (value.Length > 50)
            throw new InvalidAttributeException("Name of a group cannot be longer than 30 characters.");

        return value;
    }

    private string ValidatePhotoUrl(string value)
    {
        // todo change like in Account
        var pattern = @"^(https?://.*\.(jpg|jpeg|png|gif|bmp))$";
        bool valid = Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);

        if (string.IsNullOrWhiteSpace(value) || !valid)
            throw new InvalidPictureUrlException();

        return value;
    }
    
    public void AddMember(Account account)
    {
        if (account == null)
            throw new ArgumentNullException(nameof(account), "Account cannot be null");
        
        _members.Add(account);
    }

    public void RemoveMember(Account account)
    {
        _members.Remove(account);
    }

    public static List<ChatRoom> GetExtentCopy()
    {
        return new List<ChatRoom>(_extent);
    }

    public static void AddInstanceToExtent(ChatRoom instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        _extent.Add((instance));
    }

    public static void RemoveInstanceFromExtent(ChatRoom instance)
    {
        _extent.Remove(instance);
    }

    public static List<ChatRoom> GetExtent()
    {
        return _extent;
    }

    public static void ResetExtent()
    {
        _extent.Clear();
        IdCounter = 0;
    }

    public override string ToString()
    {
        return $"Chat Room Details:\n" +
               $"ID: {ID}\n" +
               $"Name: {Name}\n" +
               $"Photo URL: {(PhotoUrl ?? "No photo available")}\n" +
               $"Number of Members: {Members?.Count ?? 0}\n" +
               $"Members: {GetMembersNames()}\n";
    }

    private string GetMembersNames()
    {
        return _members.Count == 0 ? "No participants" : string.Join(", ", _members.Select(a => a.Username));
    }
}