using System.Data;
using System.Xml;
using System.Xml.Serialization;
using get_a_way.Entities;

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