using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace get_a_way.Entities;

abstract class Account
{
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
    private string? ProfilePictureUrl { get; set; } // todo figure out how to store images
    private bool Verified { get; set; }
    
    [Range(0.0, 10.0)]
    private double Rating { get; set; }
    private List<string> Languages { get; set; }

    [ForeignKey("Account")] 
    private List<Account> Followings { get; set; }
}