namespace get_a_way.Exceptions;

public class DuplicateUsernameException(string msg = "Account with this username already exists.") : Exception(msg);