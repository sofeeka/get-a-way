using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Review;

[Serializable]
public class Review : IExtent<Review>
{
    public static List<Review> Extent = new List<Review>();
    
    private static long IdCounter = 0;
    public long ID { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }

    public Review()
    {
    }

    public Review(int rating, string comment)
    {
        ID = ++IdCounter;
        Rating = rating;
        Comment = comment;
    }

    public List<Review> GetExtentCopy()
    {
        return new List<Review>(Extent);
    }

    public void AddInstanceToExtent(Review instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        Extent.Add((instance));
    }

    public void RemoveInstanceFromExtent(Review instance)
    {
        Extent.Remove(instance);
    }
}