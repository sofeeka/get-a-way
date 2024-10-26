namespace get_a_way.Services;

public interface IExtent<T>
{
    public List<T> GetExtentCopy();
    public void AddInstanceToExtent(T instance);
    public void RemoveInstanceFromExtent(T instance);
}