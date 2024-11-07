using System.Collections.ObjectModel;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Entities.Places;
using get_a_way.Entities.Trip;

namespace get_a_way;

[Serializable]
public class Database
{
    private ReadOnlyCollection<Account> _accounts;
    private ReadOnlyCollection<ChatRoom> _chatRooms;
    private ReadOnlyCollection<Message> _messages;
    private List<Place> _places;
    private List<Trip> _trips;

    public ReadOnlyCollection<Account> Accounts
    {
        get => _accounts;
        set => _accounts = value;
    }

    public ReadOnlyCollection<ChatRoom> ChatRooms
    {
        get => _chatRooms;
        set => _chatRooms = value;
    }

    public ReadOnlyCollection<Message> Messages
    {
        get => _messages;
        set => _messages = value;
    }

    public List<Place> Places
    {
        get => _places;
        set => _places = value;
    }

    public List<Trip> Trips
    {
        get => _trips;
        set => _trips = value;
    }

    public Database()
    {
        Accounts = Account.GetExtent();
        ChatRooms = ChatRoom.GetExtent();
        Messages = Message.GetExtent();
        Places = Place.Extent;
        Trips = Trip.Extent;
    }
}