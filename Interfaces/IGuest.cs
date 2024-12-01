using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Interfaces
{
    public interface IGuest
    {
         int Id { get; set; }
         string Name { get; set; }
         string PhoneNumber { get; set; }
         string Email { get; set; }
         bool IsActive { get; set; }
    }
}
