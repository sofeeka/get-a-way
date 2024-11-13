using System.Data;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Accounts;

[Serializable]
[XmlInclude(typeof(OwnerAccount))]
[XmlInclude(typeof(TravelerAccount))]
public abstract class Account : IExtent<Account>
{
    private static List<Account> _extent = new List<Account>();

    private static long IdCounter = 0;

    private long _id;

    private string _username;
    private string _password;
    private string _email;

    private string? _profilePictureUrl;
    private bool _verified;
    private double _rating;

    public long ID
    {
        get => _id;
        set => _id = value;
    }

    public string Username
    {
        get => _username;
        set => _username = ValidateUsername(value);
    }

    public string Password
    {
        get => _password;
        set => _password = ValidatePassword(value);
    }

    public string Email
    {
        get => _email;
        set => _email = ValidateEmail(value);
    }

    public string? ProfilePictureUrl
    {
        get => _profilePictureUrl;
        set
        {
            if (value != null) _profilePictureUrl = ValidateProfilePictureUrl(value);
        }
    }

    public bool Verified
    {
        get => _verified;
        set => _verified = value;
    }

    public double Rating
    {
        get => _rating;
        set => _rating = ValidateRating(value);
    }

    // todo make sure lists are saved properly, maybe change to protected
    [XmlArray("Languages")]
    [XmlArrayItem("Language")]
    protected List<string> Languages { get; set; }

    [XmlArray("Followings")]
    [XmlArrayItem("Following")]
    protected List<Account> Followings { get; set; }

    public Account()
    {
    }

    protected Account(string username, string password, string email)
    {
        ID = ++IdCounter;

        Username = username;
        Password = password;
        Email = email;

        ProfilePictureUrl = "static/img/default_profile_img.jpg";
        Verified = false;
        Rating = 10.0;
        Languages = new List<string>();
        Followings = new List<Account>();

        AddInstanceToExtent(this);
    }

    private string ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Length < 5 || username.Length > 30)
            throw new InvalidAttributeException("Username must be between 5 and 30 characters long");

        if (IsUsernameTaken(username))
            throw new DuplicateUsernameException();

        return username;
    }

    private bool IsUsernameTaken(string username)
    {
        foreach (var account in _extent)
            if (account.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                return true;

        return false;
    }

    private string ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new InvalidAttributeException("Password must be at least 8 characters long");
        
        if (!password.Any(char.IsUpper))
            throw new InvalidAttributeException("Password must contain at least one uppercase letter");

        if (!password.Any(char.IsLower))
            throw new InvalidAttributeException("Password must contain at least one lowercase letter");

        if (!password.Any(char.IsDigit))
            throw new InvalidAttributeException("Password must contain at least one digit");
        
        return password;
    }

    private string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            throw new InvalidAttributeException("Invalid email format");

        return email;
    }

    private double ValidateRating(double value)
    {
        value = Math.Max(value, 0.0);
        value = Math.Min(value, 10.0);
        return value;
    }

    private string ValidateProfilePictureUrl(string value)
    {
        // todo create exception for invalid url
        var pattern = @"^(https?://.*\.(jpg|jpeg|png|gif|bmp))$";
        bool valid = Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);

        try
        {
            if (string.IsNullOrWhiteSpace(value) || !valid)
                throw new InvalidAttributeException($"Invalid picture URL: '{value}'.");
        }
        catch (InvalidAttributeException e)
        {
            value = "static/img/default_profile_img.jpg";
        }

        return value;
    }

    public void AddLanguage(string language)
    {
        if (!string.IsNullOrWhiteSpace(language))
            Languages.Add(language);
    }

    public static List<Account> GetExtentCopy()
    {
        return new List<Account>(_extent);
    }

    public static void AddInstanceToExtent(Account instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        _extent.Add(instance);
    }

    public static void RemoveInstanceFromExtent(Account instance)
    {
        _extent.Remove(instance);
    }

    public static List<Account> GetExtent()
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
        return $"Account Details:\n" +
               $"ID: {ID}\n" +
               $"Username: {Username}\n" +
               $"Email: {Email}\n" +
               $"Profile Picture URL: {(ProfilePictureUrl ?? "No profile picture available")}\n" +
               $"Verified: {(Verified ? "Yes" : "No")}\n" +
               $"Rating: {Rating:F1}\n" +
               $"Languages: {GetLanguages()}\n" +
               $"Following: {GetFollowings()}\n";
    }

    private string GetLanguages()
    {
        if (Languages.Count == 0)
            return "None";
        return string.Join(", ", Languages);
    }

    private string GetFollowings()
    {
        if (Followings.Count == 0)
            return "None";
        return string.Join(", ", Followings.ConvertAll(f => f.Username));
    }
}