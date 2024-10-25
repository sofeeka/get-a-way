using System.Xml.Serialization;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities;

[Serializable]
[XmlInclude(typeof(OwnerAccount))]
[XmlInclude(typeof(TravelerAccount))]
public abstract class Account : IExtent<Account>
{
    public static List<Account> Extent = new List<Account>();

    // todo check values for exceptions, make visible public fields (look class diagram implementation p1 in assignment)
    public long ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool Verified { get; set; }
    public double Rating { get; set; }
    public List<string> Languages { get; set; }
    public List<Account> Followings { get; set; }

    public Account()
    {
    }

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

        Extent.Add(this);
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
        foreach (var account in Extent)
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


    public List<Account> GetExtentCopy()
    {
        return new List<Account>(Extent);
    }

    public void AddInstanceToExtent(Account instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        Extent.Add((instance));
    }

    public void RemoveInstanceFromExtent(Account instance)
    {
        Extent.Remove(instance);
    }
}