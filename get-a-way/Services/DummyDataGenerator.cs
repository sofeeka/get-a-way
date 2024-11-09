using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;

namespace get_a_way.Services;

public class DummyDataGenerator
{
    public static Database CreateInitialDatabase()
    {
        Database db = new Database();

        GenerateAccounts();
        GenerateChatRooms();
        GenerateMessages();
        GeneratePlaces();
        GenerateTrips();

        Serialisation.saveDB(db);
        return db;
    }

    // todo automate it with random generation
    private static void GenerateAccounts()
    {
        OwnerAccount owner = new OwnerAccount("username1", "password", "email1@gmail.com");
        TravelerAccount traveler = new TravelerAccount("username2", "password", "email2@gmail.com");
    }

    private static void GenerateChatRooms()
    {
        ChatRoom chatRoom1 = new ChatRoom("chatroom1", "photo url");
        ChatRoom chatRoom2 = new ChatRoom("chatroom2", "photo url");
    }

    private static void GenerateMessages()
    {
        // todo
    }

    private static void GeneratePlaces()
    {
        // todo
    }

    private static void GenerateTrips()
    {
        // todo
    }
}