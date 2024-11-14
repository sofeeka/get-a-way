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

    public Review()
    {
    }

    public Review(double rating, string comment)
    {
        ID = ++IdCounter;
        Rating = rating;
        Comment = comment;

        AddInstanceToExtent(this);
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