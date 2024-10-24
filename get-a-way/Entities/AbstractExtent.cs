namespace get_a_way.Entities;

public abstract class AbstractExtent<T>
{
    private List<T> container = new List<T>() ;
    
    public List<T> GetExtentUnmodifiable()
    {
        return new List<T>(container);
    }

    public void AddInstanceToExtent(T instance)
    {
        container.Add(instance);
    }

    public void RemoveInstanceFromExtent(T instance)
    {
        container.Remove(instance);
    }
    
}