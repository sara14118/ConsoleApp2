using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp2
{
    internal class BookingController : ControllerBase
    {
        private readonly BookingDbContext _context;

        public BookingController(BookingDbContext context) => _context = context;


        // Action to get a list of available spaces
       
        public ActionResult<IEnumerable<Space>> GetAvailableSpaces()
        {
            var availableSpaces = _context.Spaces.ToList();
            return Ok(availableSpaces);
        }

        // Action to get the booking status for a specific space
     
        public ActionResult<IEnumerable<Booking>> GetBookingStatusForSpace(int spaceID)
        {
            var bookingsForSpace = _context.Bookings
                .Where(b => b.SpaceID == spaceID)
                .ToList();

            if (bookingsForSpace.Any())
            {
                return Ok(bookingsForSpace);
            }
            else
            {
                return NotFound($"No bookings found for space with ID {spaceID}");
            }
        }








        public ActionResult<IEnumerable<Booking>> GetBookings() => _context.Bookings.ToList();

        public ActionResult<Booking> GetBooking(int id) => _context.Bookings.Find(id);

        //[HttpPost]
        public ActionResult<Booking> CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBooking), new { id = booking?.BookingID }, booking);
        }

        public IActionResult UpdateBooking(int id, Booking booking)
        {
            if (id != booking?.BookingID || !_context.Bookings.Any(b => b.BookingID == id))
                return BadRequest();

            _context.Entry(booking).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        public ActionResult<Booking> CancelBooking(int id)
        {
            var booking = _context.Bookings.Find(id);

            if (booking == null)
                return NotFound();

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return booking;
        }
    }
}

