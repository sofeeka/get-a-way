namespace get_a_way.Services;

public interface IExtent<T>
{
    List<T> GetExtentUnmodifiable();
    void AddInstanceToExtent(T instance);
    void RemoveInstanceFromExtent(T instance);
}