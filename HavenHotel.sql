select * 
	from Guests;

select *
	from Rooms;

select * 
	from Bookings;

Select *
	from Rooms 
	where Size < 25;

Select *
	from Guests g
	order by g.Name

Select 
	Guests.Name AS [Name],
	Bookings.Id AS BookingID,
	Rooms.RoomNumber AS RoomNumber,
	Bookings.TotalPrice AS TotalPrice
	FROM 
		Bookings
	Join Rooms
		on
		Bookings.RoomId = Rooms.Id
	join Guests
		on
		Bookings.GuestId = Guests.Id;