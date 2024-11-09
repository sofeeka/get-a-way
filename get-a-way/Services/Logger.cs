namespace get_a_way;

public static class Logger
{
    public static readonly bool Logging = false;

    public static void Log(string message)
    {
        if (Logging)
            Console.WriteLine(message);
    }
}