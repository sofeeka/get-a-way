namespace get_a_way.Entities;

[Serializable]
public class Review
{
    public long ID { get;   set; }
    public int rating { get;   set; }
    public string comment { get;   set; }

    public Review()
    {
    }

    public Review(long id, int rating, string comment)
    {
        ID = id;
        this.rating = rating;
        this.comment = comment;
    }
}