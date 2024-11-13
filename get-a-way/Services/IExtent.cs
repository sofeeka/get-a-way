namespace get_a_way.Services;

public interface IExtent<T>
{
    public static abstract void AddInstanceToExtent(T instance);
    public static abstract void RemoveInstanceFromExtent(T instance);
    public static abstract List<T> GetExtentCopy();
    public static abstract List<T> GetExtent();
    // todo rename to ResetExtent
    static abstract void Reset();
}