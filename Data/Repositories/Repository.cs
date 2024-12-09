using HavenHotel.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly HotelDbContext _dbContext;

    public Repository(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(T item)
    {
        _dbContext.Set<T>().Add(item);
        _dbContext.SaveChanges();
    }

    public IEnumerable<T> GetAllItems()
    {
        var items = _dbContext.Set<T>().ToList();

        if (!items.Any())
        {
            Console.WriteLine($"No {typeof(T).Name.ToLower()}s were found.");
        }

        return items;
    }

    public T GetItemById(int id)
    {
        var item = _dbContext.Set<T>().Find(id);
        if (item == null)
        {
            Console.WriteLine($"{typeof(T).Name} with ID {id} was not found.");
        }
        return item;
    }

    public void Update(T item)
    {
        _dbContext.Set<T>().Update(item);
        _dbContext.SaveChanges();
    }

    public void RemoveItemById(int id)
    {
        var item = _dbContext.Set<T>().Find(id);

        if (item != null)
        {
            _dbContext.Set<T>().Remove(item);
            _dbContext.SaveChanges();
        }
        else
        {
            Console.WriteLine($"No {typeof(T).Name.ToLower()} found with ID {id}.");
        }
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
