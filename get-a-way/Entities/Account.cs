using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities;

abstract class Account : IExtent<Account>
{
    private static List<Account> extent = new List<Account>();
    
    // todo check values for exceptions, make visible public fields (look class diagram implementation p1 in assignment)
    
    [Key]
    [Required]
    private long ID { get; set; }
    [Required]
    [MaxLength(30)]
    private string Username { get; set; }
    [Required]
    [MinLength(8)]
    private string Password { get; set; }

    [Required]
    [EmailAddress]
    private string Email { get; set; }
    private string? ProfilePictureUrl { get; set; } // todo figure out how to store images, add a default picture
    private bool Verified { get; set; }
    
    [Range(0.0, 10.0)]
    private double Rating { get; set; }
    private List<string> Languages { get; set; }

    [ForeignKey("Account")] 
    private List<Account> Followings { get; set; }
    
    protected Account(long id, string username, string password, string email)
    {
        ID = id;
        Username = username;
        Password = password;
        Email = email;
        Verified = false;
        Rating = 10.0;
        Languages = new List<string>();
        Followings = new List<Account>();

        extent.Add(this);
    }

    public List<Account> GetExtentUnmodifiable()
    {
        // copy of the list to avoid unintentional changes
        return new List<Account>(extent);
    }

    public void AddInstanceToExtent(Account instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        extent.Add((instance));
    }

    public void RemoveInstanceFromExtent(Account instance)
    {
        extent.Remove(instance);
    }
}