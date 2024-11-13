using System.Collections.ObjectModel;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Entities.Places;
using get_a_way.Entities.Review;
using get_a_way.Entities.Trip;

namespace get_a_way;

[Serializable]
public class Database
{
    private List<Account> _accounts;
    private List<ChatRoom> _chatRooms;
    private List<Message> _messages;
    private List<Place> _places;
    private List<Trip> _trips;
    private List<Review> _review;

    public List<Account> Accounts
    {
        get => _accounts;
        set => _accounts = value;
    }

    public List<ChatRoom> ChatRooms
    {
        get => _chatRooms;
        set => _chatRooms = value;
    }

    public List<Message> Messages
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

    public List<Review> Reviews
    {
        get => _review;
        set => _review = value;
    }

    public Database()
    {
        Accounts = Account.GetExtent();
        ChatRooms = ChatRoom.GetExtent();
        Messages = Message.GetExtent();
        Places = Place.GetExtent();
        Trips = Trip.GetExtent();
        Reviews = Review.GetExtent();
    }

    public void Represent()
    {
        foreach (var value in Accounts)
        {
            Console.WriteLine();
            Console.WriteLine(value);
        }

        foreach (var value in ChatRooms)
        {
            Console.WriteLine();
            Console.WriteLine(value);
        }

        foreach (var value in Messages)
        {
            Console.WriteLine();
            Console.WriteLine(value);
        }

        foreach (var value in Places)
        {
            Console.WriteLine();
            Console.WriteLine(value);
        }

        foreach (var value in Trips)
        {
            Console.WriteLine();
            Console.WriteLine(value);
        }

        foreach (var value in Reviews)
        {
            Console.WriteLine();
            Console.WriteLine(value);
        }
    }
}