using Autofac.Features.AttributeFilters;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using HavenHotel.Interfaces.DisplayInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.Bookings.Services.Delete
{
    public class DeletedBookingMenu : IMenu
    {
        private readonly ISharedMenu _menu;
        private readonly Lazy<IMenu> _mainMenu;
        private readonly IDelete _delete;
        private readonly ISoftDelete _softDelete;
        private readonly IUnDelete _unDelete;
        private readonly IDisplayAll _displayAllDeleted;

        public DeletedBookingMenu
        (
            ISharedMenu menu,
           [KeyFilter("MainMenu")] Lazy<IMenu> mainMenu,
           [KeyFilter("DeleteBooking")] IDelete delete,
           [KeyFilter("SoftDeleteBooking")] ISoftDelete softDelete,
           [KeyFilter("UnDeleteBooking")] IUnDelete unDelete,
           [KeyFilter("DisplayDeletedBookings")] IDisplayAll displayAllDeleted
        )
        {
            _menu = menu;
            _mainMenu = mainMenu;
            _delete = delete;
            _softDelete = softDelete;
            _unDelete = unDelete;
            _displayAllDeleted = displayAllDeleted;
        }

        public void DisplayMenu()
        {
            var deletedBookingMenu = new List<string>
            {
                "Soft Delete Booking",   
                "Delete Booking",      
                "Undelete Booking",    
                "View Deleted Bookings", 
                "Back to Main Menu"     
            };
           string header =("Deleted Bookings Menu");

            _menu.DisplayMenu(header,deletedBookingMenu, _softDelete.SoftDelete, _delete.Delete, _unDelete.UndoDete, _displayAllDeleted.DisplayAll, _mainMenu.Value.DisplayMenu);


        }
    }
}
