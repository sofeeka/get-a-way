using get_a_way.Exceptions;

namespace get_a_way.Services;

public interface IExtent<T>
{
    public static List<T> Extent = new List<T>();
    public static List<T> GetExtentCopy()
    {
        // copy of the list to avoid unintentional changes
        return new List<T>(Extent);
    }

    public static void AddInstanceToExtent(T instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        Extent.Add((instance));
    }

    public static void RemoveInstanceFromExtent(T instance)
    {
        Extent.Remove(instance);
    }
}