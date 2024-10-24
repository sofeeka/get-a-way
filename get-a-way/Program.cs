// See https://aka.ms/new-console-template for more information

using System.Xml;
using System.Xml.Serialization;
using get_a_way;

Console.WriteLine("Hello, World!");

Database db = new Database();

void saveDB(string path = "get-a-way-db.xml")
{
    StreamWriter file = File.CreateText(path);
    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Database));
    using (XmlTextWriter writer = new XmlTextWriter(file))
    {
        xmlSerializer.Serialize(writer, db);
    }
}

bool loadDB(string path = "get-a-way-db.xml")
{
    StreamReader file;
    try
    {
        file = File.OpenText(path);
    }
    catch (FileNotFoundException)
    {
        return false;
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
            return false;
        }
    }
    return true;
}

if (!loadDB())
    db = new Database();

saveDB();