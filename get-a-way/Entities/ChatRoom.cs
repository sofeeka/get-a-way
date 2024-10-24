namespace get_a_way.Entities;

public class ChatRoom
{
    public long ID { get; private set; }
    public string name { get; private set; }
    public string photoUrl { get; private set; }
    public List<Account> accounts { get; private set; }
}