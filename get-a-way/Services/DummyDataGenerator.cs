using get_a_way.Entities.Accounts;
using get_a_way.Entities.Chat;
using get_a_way.Entities.Places;
using get_a_way.Entities.Places.Accommodation;
using get_a_way.Entities.Places.Attractions;
using get_a_way.Entities.Review;
using get_a_way.Entities.Trip;

namespace get_a_way.Services;

public class DummyDataGenerator
{
    public static Database CreateInitialDatabase()
    {
        Database db = new Database();

        GenerateData();

        Serialisation.saveDB(db);
        return db;
    }

    public static void GenerateData()
    {
        GenerateAccounts();
        GenerateChatRooms();
        GenerateMessages();
        GeneratePlaces();
        GenerateTrips();
        GenerateReviews();
    }

    // todo automate it with random generation
    private static void GenerateAccounts()
    {
        var traveler1 = new TravelerAccount("Traveler01", "securePass123", "traveler01@example.com");
        var traveler2 = new TravelerAccount("Traveler02", "securePass456", "traveler02@example.com");
        var traveler3 = new TravelerAccount("Traveler03", "securePass789", "traveler03@example.com");

        var owner1 = new OwnerAccount("Owner01", "ownerPass123", "owner01@example.com");
        var owner2 = new OwnerAccount("Owner02", "ownerPass456", "owner02@example.com");
        var owner3 = new OwnerAccount("Owner03", "ownerPass789", "owner03@example.com");
    }

    private static void GenerateChatRooms()
    {
        var chatRoom1 = new ChatRoom("Travel Buddies", "https://example.com/photos/travel_buddies.jpg");
        var chatRoom2 = new ChatRoom("Adventure Seekers", "https://example.com/photos/adventure_seekers.jpg");
        var chatRoom3 = new ChatRoom("City Explorers", "https://example.com/photos/city_explorers.jpg");
    }

    private static void GenerateMessages()
    {
        var message1 = new Message("Hello, everyone! Excited to be here.");
        var message2 = new Message("Does anyone have recommendations for places to visit?");
        var message3 = new Message("Looking forward to our next adventure together!");
    }

    private static void GeneratePlaces()
    {
        GenerateAccommodations();
        GenerateAttractions();
    }

    private static void GenerateAttractions()
    {
        GenerateActiveAttractions();
    }

    private static void GenerateActiveAttractions()
    {
        var activeAttraction = new ActiveAttraction(
            name: "Mountain Climbing Adventure",
            location: "Alpine Ridge, Switzerland",
            openTime: new DateTime(2024, 1, 1, 6, 0, 0),
            closeTime: new DateTime(2024, 1, 1, 20, 0, 0),
            priceCategory: PriceCategory.Luxury,
            petFriendly: false,
            entryFee: 50,
            minimalAge: 12,
            description: "An exhilarating guided mountain climbing experience with professional instructors.",
            activityType: "Climbing"
        );
    }

    private static void GenerateAccommodations()
    {
        var accommodation1 = new Accommodation(
            name: "Seaside Resort",
            location: "Beachfront, Malibu",
            openTime: new DateTime(2024, 1, 1, 8, 0, 0),
            closeTime: new DateTime(2024, 1, 1, 22, 0, 0),
            priceCategory: PriceCategory.Moderate,
            petFriendly: true,
            type: AccommodationType.Resort,
            maxPeople: 4
        );

        var accommodation2 = new Accommodation(
            name: "Mountain Lodge",
            location: "Rocky Mountains, Colorado",
            openTime: new DateTime(2024, 1, 1, 6, 0, 0),
            closeTime: new DateTime(2024, 1, 1, 23, 0, 0),
            priceCategory: PriceCategory.Luxury,
            petFriendly: false,
            type: AccommodationType.Lodge,
            maxPeople: 6
        );

        var accommodation3 = new Accommodation(
            name: "Urban Suites",
            location: "Downtown, New York City",
            openTime: new DateTime(2024, 1, 1, 0, 0, 0),
            closeTime: new DateTime(2024, 1, 1, 23, 59, 59),
            priceCategory: PriceCategory.Expensive,
            petFriendly: true,
            type: AccommodationType.Hotel,
            maxPeople: 2
        );
    }

    private static void GenerateTrips()
    {
        var traveler1 = new TravelerAccount("Traveler1", "Password123!", "traveler1@example.com");
        var traveler2 = new TravelerAccount("Traveler2", "Password456!", "traveler2@example.com");
        var traveler3 = new TravelerAccount("Traveler3", "Password789!", "traveler3@example.com");

        var trip1 = new Trip(
            account: traveler1,
            date: new DateTime(2024, 5, 15),
            tripType: TripType.Friends,
            description: "A thrilling trip to the Rocky Mountains with hiking and camping!"
        );

        var trip2 = new Trip(
            account: traveler2,
            date: new DateTime(2024, 6, 10),
            tripType: TripType.Family,
            description: "Exploring the rich history and architecture of Rome."
        );

        var trip3 = new Trip(
            account: traveler3,
            date: new DateTime(2024, 7, 20),
            tripType: TripType.Couple,
            description: "A peaceful getaway to the Maldives with beach relaxation and snorkeling."
        );
    }

    private static void GenerateReviews()
    {
        var review1 = new Review(
            rating: 4.5,
            comment: "Amazing experience! The guide was knowledgeable, and the scenery was breathtaking."
        );

        var review2 = new Review(
            rating: 3.0,
            comment: "It was okay, but the service could be improved. The location was beautiful, though."
        );

        var review3 = new Review(
            rating: 5.0,
            comment: "Absolutely perfect! Everything was organized, and I had a fantastic time. Highly recommended!"
        );
    }
}