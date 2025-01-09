using get_a_way.Entities.Accounts;
using get_a_way.Entities.Places;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Review;

[Serializable]
public class Review : IExtent<Review>
{
    private static List<Review> _extent = new List<Review>();

    private static long IdCounter = 0;

    private long _id;

    private double _rating;
    private string _comment;

    private TravelerAccount _traveler;
    private Place _place;

    public long ID
    {
        get => _id;
        set => _id = value;
    }

    public double Rating
    {
        get => _rating;
        set => _rating = ValidateRating(value);
    }

    public string Comment
    {
        get => _comment;
        set => _comment = ValidateComment(value);
    }

    public TravelerAccount Traveler => _traveler;
    public Place Place => _place;

    public Review()
    {
    }

    public Review(TravelerAccount traveler, Place place, double rating, string comment)
    {
        _place = place ?? throw new ArgumentNullException(nameof(place), "Cannot leave review to a null place.");
        _traveler = traveler ??
                    throw new ArgumentNullException(nameof(traveler), "Null traveler cannot leave review to a place.");

        Rating = rating;
        Comment = comment;

        _place.AddReview(this); // reverse connection traveler leaves review
        _traveler.AddReview(this); // reverse connection review of a place

        ID = ++IdCounter;
        AddInstanceToExtent(this);
    }

    ~Review()
    {
        _traveler.RemoveReview(this);
        _place.RemoveReview(this);
    }

    private double ValidateRating(double value)
    {
        value = Math.Max(value, 0.0);
        value = Math.Min(value, 10.0);
        return value;
    }

    private string ValidateComment(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Comment cannot be empty.");

        if (value.Length > 1000)
            throw new InvalidAttributeException("Comment length cannot exceed 1000 characters.");
        return value;
    }

    public static List<Review> GetExtentCopy()
    {
        return new List<Review>(_extent);
    }

    public static void AddInstanceToExtent(Review instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        _extent.Add((instance));
    }

    public static void RemoveInstanceFromExtent(Review instance)
    {
        _extent.Remove(instance);
    }

    public static List<Review> GetExtent()
    {
        return _extent;
    }

    public static void ResetExtent()
    {
        _extent.Clear();
        IdCounter = 0;
    }

    public override string ToString()
    {
        return $"Review ID: {ID}\n" +
               $"Rating: {Rating:F1}/10\n" +
               $"Comment: {(string.IsNullOrWhiteSpace(Comment) ? "No comment provided" : Comment)}\n";
    }
}