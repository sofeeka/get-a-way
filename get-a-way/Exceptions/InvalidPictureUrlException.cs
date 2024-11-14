namespace get_a_way.Exceptions;

public class InvalidPictureUrlException(string msg = "Invalid picture URL.") : Exception(msg);