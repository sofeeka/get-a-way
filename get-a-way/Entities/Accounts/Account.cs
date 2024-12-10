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
    private HashSet<Language> _languages;
    
    private HashSet<Account> _followers;
    private HashSet<Account> _followings;

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

    [XmlArray("Languages")]
    [XmlArrayItem("Language")]
    public HashSet<Language> Languages => new HashSet<Language>(_languages);

    [XmlArray("Followings")]
    [XmlArrayItem("Following")]
    public HashSet<Account> Followings => new HashSet<Account>(_followings);

    [XmlArray("Followers")]
    [XmlArrayItem("Follower")]
    public  HashSet<Account> Followers => new HashSet<Account>(_followers);

    private static string _defaultImage = "static/img/default_profile_img.jpg";

    public Account()
    {
        _followings = new HashSet<Account>();
        _followers = new HashSet<Account>();
        _languages = new HashSet<Language>();
    }

    protected Account(string username, string password, string email) : this()
    {
        ID = ++IdCounter;

        Username = username;
        Password = password;
        Email = email;

        ProfilePictureUrl = _defaultImage;
        Verified = false;
        Rating = 10.0;

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
        var errorMessages = new List<string>();

        if (string.IsNullOrWhiteSpace(password))
            errorMessages.Add("Password cannot be empty or whitespace.");

        if (password.Length < 8 || password.Length > 40)
            errorMessages.Add("Password must be at least 8 characters long.");

        if (!password.Any(char.IsUpper))
            errorMessages.Add("Password must contain at least one uppercase letter.");

        if (!password.Any(char.IsLower))
            errorMessages.Add("Password must contain at least one lowercase letter.");

        if (!password.Any(char.IsDigit))
            errorMessages.Add("Password must contain at least one digit.");

        if (errorMessages.Any())
            throw new InvalidPasswordException(string.Join(" ", errorMessages));

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
        var pattern = @"^(https?://.*\.(jpg|jpeg|png|gif|bmp))$";
        bool valid = Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);

        if (string.IsNullOrWhiteSpace(value) || !valid)
            return _defaultImage;

        return value;
    }

    public void AddLanguage(Language language)
    {
        if (language == null)
            throw new ArgumentNullException(nameof(language));
        _languages.Add(language);
    }

    public void RemoveLanguage(Language language)
    {
        _languages.Remove(language);
    }
    
    private void AddFollower(Account account)
    {
        _followers.Add(account);
    }

    private void RemoveFollower(Account account)
    {
        _followers.Remove(account);
    }
    
    public void Follow(Account account)
    {
        if (account == null)
            throw new ArgumentNullException(nameof(account), "Account to follow cannot be null");

        if (account == this)
            throw new InvalidOperationException("An account cannot follow itself");

        if (_followings.Add(account)) //checks if not already present
        {
            account.AddFollower(this); //reverse connection
        }
    }

    public void Unfollow(Account account)
    {
        if (account == null)
            throw new ArgumentNullException(nameof(account), "Account to unfollow cannot be null");

        if (_followings.Remove(account)) //checks if present
        {
            account.RemoveFollower(this); //reverse connection
        }
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
               $"Languages: {GetLanguagesToString()}\n" +
               $"Followings: {GetUsernamesToStringFromAccountSet(_followings)}\n" + 
               $"Followers: {GetUsernamesToStringFromAccountSet(_followers)}\n";
    }

    private string GetLanguagesToString()
    {
        if (_languages == null || _languages.Count == 0)
            return "None";
        return string.Join(", ", _languages);
    }

    private string GetUsernamesToStringFromAccountSet(HashSet<Account> accounts)
    {
        if (accounts == null || accounts.Count == 0)
            return "None";
        return string.Join(", ", accounts.Select(f => f.Username));
    }
}