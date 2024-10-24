using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities;

abstract class Account : IExtent<Account>
{
    private static List<Account> extent = new List<Account>();
    
    // todo check values for exceptions, make visible public fields (look class diagram implementation p1 in assignment)
    
    public long ID { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public bool Verified { get; private set; }
    public double Rating { get; private set; }
    public List<string> Languages { get; private set; }
    public List<Account> Followings { get; private set; }
    
    protected Account(long id, string username, string password, string email)
    {
        ID = id;
        Username = ValidateUsername(username);
        Password = ValidatePassword(password);
        Email = ValidateEmail(email);
        ProfilePictureUrl = "static/img/default_profile_img.jpg";
        Verified = false;
        Rating = 10.0;
        Languages = new List<string>();
        Followings = new List<Account>();

        extent.Add(this);
    }

    private string ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Length < 5 || username.Length > 30)
        {
            throw new InvalidAttributeException("Username must be between 5 and 30 characters long");
        }

        if (IsUsernameTaken(username))
        {
            throw new InvalidAttributeException($"Username '{username}' is already taken");
        }

        return username;
    }
    
    private bool IsUsernameTaken(string username)
    {
        foreach (var account in extent)
        {
            if (account.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
    
    private string ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            throw new InvalidAttributeException("Password must be at least 8 characters long");
        }
        return password;
    }

    private string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
        {
            throw new InvalidAttributeException("Invalid email format");
        }
        return email;
    }

    private bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }
    

    // Public method to add a language (since Languages is private)
    public void AddLanguage(string language)
    {
        if (!string.IsNullOrWhiteSpace(language))
        {
            Languages.Add(language);
        }
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