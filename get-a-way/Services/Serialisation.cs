using System.Xml;
using System.Xml.Serialization;

namespace get_a_way.Services;

public static class Serialisation
{
    private const string _fileName = "get-a-way-db.xml";

    public static void saveDB(Database db, string path = _fileName)
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Database));
        using (XmlTextWriter writer = new XmlTextWriter(file))
        {
            xmlSerializer.Serialize(writer, db);
        }
    }

    public static Database loadDB(string path = _fileName)
    {
        Database.Reset();
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
}