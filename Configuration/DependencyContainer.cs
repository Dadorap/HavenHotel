﻿using Autofac;
using Autofac.Features.AttributeFilters;
using HavenHotel.Common;
using HavenHotel.Data;
using HavenHotel.Data.Repositories;
using HavenHotel.Interfaces;
using HavenHotel.Interfaces.BookingInterfaces;
using HavenHotel.Interfaces.DeleteInterfaces;
using HavenHotel.Interfaces.DisplayInterfaces;
using HavenHotel.Interfaces.GuestInterfaces;
using HavenHotel.Interfaces.RoomsInterfaces;
using HavenHotel.Models;
using HavenHotel.Services.Bookings.BookingServices.Update;
using HavenHotel.Services.BookingServices.Services.Create;
using HavenHotel.Services.BookingServices.Services.Delete;
using HavenHotel.Services.BookingServices.Services.Display;
using HavenHotel.Services.BookingServices.Services.Update;
using HavenHotel.Services.Guests.GuestServices.Create;
using HavenHotel.Services.Guests.GuestServices.Delete;
using HavenHotel.Services.Guests.GuestServices.Display;
using HavenHotel.Services.Guests.GuestServices.Update;
using HavenHotel.Services.Rooms.RoomServices.Update;
using HavenHotel.Services.RoomServices.Services.Create;
using HavenHotel.Services.RoomServices.Services.Delete;
using HavenHotel.Services.RoomServices.Services.Display;
using HavenHotel.Utilities;
using HavenHotel.Utilities.BookingsMenus;
using HavenHotel.Utilities.GuestsMenus;
using HavenHotel.Utilities.RoomsMenus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HavenHotel.Configuration;

public static class DependencyContainer
{
    public static IContainer Configure()
    {
        var containerBuilder = new ContainerBuilder();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        containerBuilder.Register(c =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();
            optionsBuilder.UseSqlServer(configuration
                .GetConnectionString("DefaultConnection"));

            return new HotelDbContext(optionsBuilder.Options);
        }).AsSelf()
        .InstancePerLifetimeScope();

        containerBuilder.RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();


        containerBuilder.RegisterType<Room>()
            .As<IRoom>();
        containerBuilder.RegisterType<Guest>()
            .As<IGuest>();
        containerBuilder.RegisterType<Booking>()
            .As<IBooking>();

        //Menu
        containerBuilder.RegisterType<MainMenu>()
            .Named<IMenu>("MainMenu")
            .WithAttributeFiltering();

        //Sup booking menu
        containerBuilder.RegisterType<DeletedBookingMenu>()
            .Named<IMenu>("DeletedBookingMenu")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayBookingMenu>()
            .Named<IMenu>("DisplayBookingMenu")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<UpdateBookingMenu>()
            .Named<IMenu>("UpdateBookingMenu")
            .WithAttributeFiltering();

        //Sup guest menu
        containerBuilder.RegisterType<DeletedGuestMenu>()
            .Named<IMenu>("DeletedGuestMenu")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayGuestMenu>()
            .Named<IMenu>("DisplayGuestMenu")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<UpdateGuestMenu>()
            .Named<IMenu>("UpdateGuestMenu")
            .WithAttributeFiltering();

        //Sup room menu
        containerBuilder.RegisterType<DeletedRoomMenu>()
            .Named<IMenu>("DeletedRoomMenu")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayRoomMenu>()
            .Named<IMenu>("DisplayRoomMenu")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<UpdateRoomMenu>()
            .Named<IMenu>("UpdateRoomMenu")
            .WithAttributeFiltering();
        //CRUD menu
        containerBuilder.RegisterType<BookingMenu>()
            .Named<IMenu>("BookingMenu")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<GuestMenu>()
            .Named<IMenu>("GuestMenu")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<RoomMenu>()
            .Named<IMenu>("RoomMenu")
            .WithAttributeFiltering();

        //Create
        containerBuilder.RegisterType<CreateGuest>()
            .Named<ICreate>("CreateGuest")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<CreateRoom>()
            .Named<ICreate>("CreateRoom")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<CreateBooking>()
            .Named<ICreate>("CreateBooking")
            .WithAttributeFiltering();
        //Display all 
        containerBuilder.RegisterType<DisplayAllBookings>()
            .Named<IDisplayAll>("DisplayAllBookings")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayAllGuests>()
            .Named<IDisplayAll>("DisplayAllGuests")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayAllRooms>()
            .Named<IDisplayAll>("DisplayAllRooms")
            .WithAttributeFiltering();
        //Display all active
        containerBuilder.RegisterType<DisplayActiveRooms>()
            .Named<IDisplayAll>("DisplayActiveRooms")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayActiveGuests>()
            .Named<IDisplayAll>("DisplayActiveGuests")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayActiveBookings>()
            .Named<IDisplayAll>("DisplayActiveBookings")
            .WithAttributeFiltering();
        //Display all deleted
        containerBuilder.RegisterType<DisplayDeletedGuests>()
            .Named<IDisplayAll>("DisplayDeletedGuests")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayDeletedRooms>()
            .Named<IDisplayAll>("DisplayDeletedRooms")
            .WithAttributeFiltering()
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayDeletedBookings>()
            .Named<IDisplayAll>("DisplayDeletedBookings")
            .WithAttributeFiltering();

        //Display
        containerBuilder.RegisterType<DisplayGuest>()
            .Named<IDisplay>("DisplayGuest")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayRoom>()
            .Named<IDisplay>("DisplayRoom")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayBooking>()
            .Named<IDisplay>("DisplayBooking")
            .WithAttributeFiltering();
        ///////////////////////////////////////
        containerBuilder.RegisterType<DisplayRoomsDetails>()
            .Named<IDisplayAllDetails>("DisplayRoomsDetails")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayGuestsDetails>()
            .Named<IDisplayAllDetails>("DisplayGuestsDetails")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayBookingsDetail>()
            .Named<IDisplayAllDetails>("DisplayBookingsDetail")
            .WithAttributeFiltering();
        ////////////////////////////////////////
        containerBuilder.RegisterType<DisplayRoomNumRight>()
            .As<IDisplayRoomNumRight>();
        containerBuilder.RegisterType<BookingSidebarDisplay>()
            .As<IBookingSidebarDisplay>();
        containerBuilder.RegisterType<DisplayIDRight>()
            .As<IDisplayRight>()
            .WithAttributeFiltering();



        //HardDelete
        containerBuilder.RegisterType<DeleteBooking>()
            .Named<IDelete>("DeleteBooking")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DeleteGuest>()
            .Named<IDelete>("DeleteGuest")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DeleteRoom>()
            .Named<IDelete>("DeleteRoom")
            .WithAttributeFiltering();
        //SoftDelete
        containerBuilder.RegisterType<SoftDeleteBooking>()
            .Named<ISoftDelete>("SoftDeleteBooking")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<SoftDeleteGuest>()
            .Named<ISoftDelete>("SoftDeleteGuest")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<SoftDeleteRoom>()
            .Named<ISoftDelete>("SoftDeleteRoom")
            .WithAttributeFiltering();

        //UnDelete
        containerBuilder.RegisterType<UnDeleteBooking>()
            .Named<IUnDelete>("UnDeleteBooking")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<UnDeleteGuest>()
            .Named<IUnDelete>("UnDeleteGuest")
            .WithAttributeFiltering();
        containerBuilder.RegisterType<UnDeleteRoom>()
            .Named<IUnDelete>("UnDeleteRoom")
            .WithAttributeFiltering();

        //update booking
        containerBuilder.RegisterType<DateRangeUpdate>()
            .As<IDateRange>()
            .WithAttributeFiltering();
        containerBuilder.RegisterType<PromptForBookingId>()
            .As<IPromptForBookingId>()
            .WithAttributeFiltering();
        containerBuilder.RegisterType<GuestAssignmentUpdate>()
            .As<IGuestAssignmentHandler>()
            .WithAttributeFiltering();
        containerBuilder.RegisterType<IdDisplayHandler>()
            .As<IIdDisplayHandler>()
            .WithAttributeFiltering();
        containerBuilder.RegisterType<PaymentDetailUpdate>()
            .As<IPaymentDetailUpdate>()
            .WithAttributeFiltering();        
        containerBuilder.RegisterType<InvoiceUpdate>()
            .As<IInvoiceUpdate>()
            .WithAttributeFiltering();
        //update guest
        containerBuilder.RegisterType<EmailUpdate>()
            .As<IEmailUpdate>()
            .WithAttributeFiltering();
        containerBuilder.RegisterType<NameUpdate>()
            .As<INameUpdate>()
            .WithAttributeFiltering();
        containerBuilder.RegisterType<UpdateConfirmation>()
            .As<IUpdateConfirmation>()
            .WithAttributeFiltering();
        containerBuilder.RegisterType<PromptForId>()
            .As<IPromptForId>();
        containerBuilder.RegisterType<PhoneNumberUpdate>()
            .As<IPhoneNumberUpdate>();
        //update room
        containerBuilder.RegisterType<RoomNumberUpdate>()
        .Named<IUpdateRoom>("RoomNumberUpdate")
        .WithAttributeFiltering();
        containerBuilder.RegisterType<ExtraBedUpdate>()
        .Named<IUpdateRoom>("ExtraBedUpdate")
        .WithAttributeFiltering();
        containerBuilder.RegisterType<SizeUpdate>()
        .Named<IUpdateRoom>("SizeUpdate")
        .WithAttributeFiltering();
        containerBuilder.RegisterType<TotalGuestsUpdate>()
        .Named<IUpdateRoom>("TotalGuestsUpdate")
        .WithAttributeFiltering();        
        containerBuilder.RegisterType<PriceUpdate>()
        .Named<IUpdateRoom>("PriceUpdate")
        .WithAttributeFiltering();       
        containerBuilder.RegisterType<RoomTypeUpdate>()
        .Named<IUpdateRoom>("RoomTypeUpdate")
        .WithAttributeFiltering();



        //Common
        containerBuilder.RegisterType<Exit>()
            .As<IExit>();
        containerBuilder.RegisterType<Header>()
            .As<IHeader>();
        containerBuilder.RegisterType<UserMessages>()
            .As<IUserMessages>();
        containerBuilder.RegisterType<ErrorHandler>()
            .As<IErrorHandler>();
        containerBuilder.RegisterType<NavigationHelper>()
            .As<INavigationHelper>()
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DisplayIDRight>()
            .As<IDisplayRight>();
        containerBuilder.RegisterType<BookingIdDisplay>()
            .As<IBookingIdRenderer>();
        containerBuilder.RegisterType<SoftDeleteItem>()
            .As<ISoftDeleteItem>();
        containerBuilder.RegisterType<UnDeleteItem>()
            .As<IUnDeleteItem>();
        containerBuilder.RegisterType<HardDeleteItem>()
            .As<IHardDeleteItem>()
            .WithAttributeFiltering();
        containerBuilder.RegisterType<DateValidator>()
            .As<IDateValidator>();
        containerBuilder.RegisterType<EmailValidator>()
            .As<IEmailValidator>();




        containerBuilder.RegisterType<RoomMenu>()
            .AsSelf();
        containerBuilder.RegisterType<BookingMenu>()
            .AsSelf();
        containerBuilder.RegisterType<GuestMenu>()
            .AsSelf();
        containerBuilder.RegisterType<Menu>()
            .As<ISharedMenu>()
            .WithAttributeFiltering();


        return containerBuilder.Build();
    }
}