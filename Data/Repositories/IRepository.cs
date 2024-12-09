namespace HavenHotel.Data.Repositories;

public interface IRepository<T>
{

    void Add(T item);
    T GetItemById(int id);
    IEnumerable<T> GetAllItems();
    void Update(T item);
    void RemoveItemById(int id);
    void SaveChanges();
}
