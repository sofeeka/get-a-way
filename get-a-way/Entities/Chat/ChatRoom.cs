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
    private List<Message> _messages;

    private static string _defaultImage = "static/img/default_chatroom_img.jpg";

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
    
    [XmlArray("Messages")]
    [XmlArrayItem("Message")]
    public List<Message> Messages => new List<Message>(_messages);

    public ChatRoom()
    {
        _members = new HashSet<Account>();
        _messages = new List<Message>();
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
        var pattern = @"^(https?://.*\.(jpg|jpeg|png|gif|bmp))$";
        bool valid = Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);

        if (string.IsNullOrWhiteSpace(value) || !valid)
            return _defaultImage;

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
    
    public void AddMessage(Message message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message), "Message cannot be null");

        if (message.ChatRoom != null)
            throw new InvalidOperationException("Message already belongs to another ChatRoom");

        message.AssignToChatRoom(this); //reverse connection 
        _messages.Add(message);
    }

    public void RemoveMessage(Message message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message), "Message cannot be null");

        if (_messages.Remove(message))
        {
            message.RemoveFromChatRoom();
        }
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
        if (instance == null)
            throw new ArgumentNullException(nameof(instance), "ChatRoom instance cannot be null");

        //remove all messages from Message extent
        foreach (var message in instance._messages)
        {
            Message.RemoveInstanceFromExtent(message);
        }

        instance._messages.Clear(); //clear messages list
        _extent.Remove(instance);
    }

    public static List<ChatRoom> GetExtent()
    {
        return _extent;
    }

    public static void ResetExtent()
    {
        foreach (var chatRoom in _extent)
        {
            //remove all messages from Message extent
            foreach (var message in chatRoom._messages)
            {
                Message.RemoveInstanceFromExtent(message);
            }

            chatRoom._messages.Clear(); //clear messages list
        }
        
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