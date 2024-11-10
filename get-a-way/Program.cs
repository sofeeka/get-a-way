// See https://aka.ms/new-console-template for more information

using get_a_way;
using get_a_way.Services;

Console.WriteLine("-");

// Database db = DummyDataGenerator.CreateInitialDatabase();
Database db = Serialisation.loadDB();

db.Represent();

Console.WriteLine("-");