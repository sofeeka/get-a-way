using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Entities.Places;
using get_a_way.Entities.Trip;

namespace get_a_way;

[Serializable]
public class Database
{
    public List<Account> Accounts { get; }
    public List<ChatRoom> ChatRooms { get; }
    public List<Message> Messages { get; }
    public List<Place> Places { get; }
    public List<Trip> Trips { get; }

    public Database()
    {
        Accounts = Account.Extent;
        ChatRooms = ChatRoom.Extent;
        Messages = Message.Extent;
        Places = Place.Extent;
        Trips = Trip.Extent;
    }
}