// using get_a_way;
// using get_a_way.Entities.Accounts;
// using get_a_way.Entities.Chat;
// using get_a_way.Entities.Places;
// using get_a_way.Entities.Review;
// using get_a_way.Entities.Trip;
// using get_a_way.Services;
//
// namespace get_a_way_unit_tests.DatabaseTests;
//
// public class DatabaseSerializationTests
// {
//     private const string TestFilePath = "test-db.xml";
//     private Database _originalDatabase;
//     
//     [SetUp]
//     public void SetUp()
//     {
//         _originalDatabase = new Database();
//         Database.Reset();
//         DummyDataGenerator.GenerateData();
//     }
//     
//     [Test]
//     public void SerializationSaveDB_CreatesFile()
//     {
//         Serialisation.saveDB(_originalDatabase, TestFilePath);
//         Assert.That(File.Exists(TestFilePath), Is.True);
//     }
//
//     [Test]
//     public void Deserialization_RestoresDataCorrectly()
//     {
//         var accountCopyExtent = Account.GetExtentCopy();
//         var chatRoomCopyExtent = ChatRoom.GetExtentCopy();
//         var messageCopyExtent = Message.GetExtentCopy();
//         var placeCopyExtent = Place.GetExtentCopy();
//         var tripCopyExtent = Trip.GetExtentCopy();
//         var reviewCopyExtent = Review.GetExtentCopy();
//
//         Serialisation.saveDB(_originalDatabase, TestFilePath);
//         
//         var loadedDatabase = Serialisation.loadDB(TestFilePath);
//
//         Assert.That(loadedDatabase.Accounts.Count, Is.EqualTo(accountCopyExtent.Count));
//         Assert.That(loadedDatabase.ChatRooms.Count, Is.EqualTo(chatRoomCopyExtent.Count));
//         Assert.That(loadedDatabase.Messages.Count, Is.EqualTo(messageCopyExtent.Count));
//         Assert.That(loadedDatabase.Places.Count, Is.EqualTo(placeCopyExtent.Count));
//         Assert.That(loadedDatabase.Trips.Count, Is.EqualTo(tripCopyExtent.Count));
//         Assert.That(loadedDatabase.Reviews.Count, Is.EqualTo(reviewCopyExtent.Count));
//         
//         Assert.That(loadedDatabase.Accounts[0].Username, Is.EqualTo(accountCopyExtent[0].Username));
//         Assert.That(loadedDatabase.ChatRooms[0].Name, Is.EqualTo(chatRoomCopyExtent[0].Name));
//         Assert.That(loadedDatabase.Messages[0].Text, Is.EqualTo(messageCopyExtent[0].Text));
//         Assert.That(loadedDatabase.Places[0].Name, Is.EqualTo(placeCopyExtent[0].Name));
//         Assert.That(loadedDatabase.Trips[0].TripType, Is.EqualTo(tripCopyExtent[0].TripType));
//         Assert.That(loadedDatabase.Reviews[0].Rating, Is.EqualTo(reviewCopyExtent[0].Rating));
//     }
//
//     [TearDown]
//     public void Cleanup()
//     {
//         if (File.Exists(TestFilePath))
//             File.Delete(TestFilePath);
//     }
// }
