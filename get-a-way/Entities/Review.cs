namespace get_a_way.Entities;

[Serializable]
public class Review
{
    public long ID { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }

    public Review()
    {
    }

    public Review(long id, int rating, string comment)
    {
        ID = id;
        this.Rating = rating;
        this.Comment = comment;
    }
}