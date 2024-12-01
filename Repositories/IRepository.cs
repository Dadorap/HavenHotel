using HavenHotel.BookingsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Repositories
{
    public interface IRepository<T>
    {

        void Add(T item);
        T GetItemById(int id);
        IEnumerable<T> GetAllItems();
        void Update(T item);
        void RemoveItemById(int id);
    }

}
