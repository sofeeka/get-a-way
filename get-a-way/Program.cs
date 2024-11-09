// See https://aka.ms/new-console-template for more information

using System.Xml;
using System.Xml.Serialization;
using get_a_way;
using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Services;

Console.WriteLine("-");

// Database db = DummyDataGenerator.CreateInitialDatabase();
Database db = Serialisation.loadDB();

Database loadDB(string path = "get-a-way-db.xml")
{
    Database db;
    StreamReader file;
    try
    {
        file = File.OpenText(path);
    }
    catch (FileNotFoundException)
    {
        return new Database();
    }

    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Database));
    using (XmlTextReader reader = new XmlTextReader(file))
    {
        try
        {
            db = (Database)xmlSerializer.Deserialize(reader);
        }
        catch (InvalidCastException)
        {
            db = new Database();
        }
    }

    return db;
}

Database createInitialDatabase()
{
    Database db = new Database();

    OwnerAccount owner = new OwnerAccount("username1", "password", "email1@gmail.com");
    TravelerAccount traveler = new TravelerAccount("username2", "password", "email2@gmail.com");

    ChatRoom chatRoom1 = new ChatRoom("chatroom1", "photo url");
    ChatRoom chatRoom2 = new ChatRoom("chatroom2", "photo url");

    //saveDB(db);
    return db;
}

// Database db = createInitialDatabase();
//Database db = loadDB();

foreach (var account in db.Accounts)
{
    Console.WriteLine();
    Console.WriteLine(account);
}

foreach (var chat in db.ChatRooms)
{
    Console.WriteLine();
    Console.WriteLine(chat.Name);
}
db.Represent();

Console.WriteLine("-");