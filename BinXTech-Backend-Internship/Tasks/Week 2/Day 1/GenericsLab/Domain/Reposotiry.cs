using System.Runtime.CompilerServices;

public class Repository<T> 
where T: class
// The "where T : class" constraint ensures that Repository<T> can only be used with reference types (classes).
// This prevents using value types like int or double and matches the intended use of the repository for domain entities.
{
    private List<T> _items=new();
    public void Add(T item)
        => _items.Add(item);
    public IReadOnlyList<T> GetAll()
        => _items.AsReadOnly();
    public T? Find(Predicate<T> predicate)
    {
        foreach (T item in _items)
        {
            if(predicate(item) == true)
                return item;
        }
        return null;
    }
}