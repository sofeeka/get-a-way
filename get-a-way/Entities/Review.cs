namespace get_a_way.Entities;

public class Review
{
    public long ID { get; private set; }
    public int rating { get; private set; }
    public string comment { get; private set; }

    public Review(long id, int rating, string comment)
    {
        ID = id;
        this.rating = rating;
        this.comment = comment;
    }
}