using Microsoft.AspNetCore.Mvc;
using RentalsAPI.Models;
using RentalsAPI.Repositories;

namespace RentalsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class BookingController : ControllerBase
    {
        private IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        //DELETE: api/<BookingController>/
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookingModel>> Delete(Guid id)
        {
            await _bookingRepository.DeleteBooking(id);
            return Ok();
        }

        //PUT:api/<BookingController>/
        [HttpPut("{id}")]
        public async Task<ActionResult<BookingModel>> Put(Guid id, [FromBody] BookingModel booking)
        {
            if (booking == null)
            {
                return BadRequest();
            }

            await _bookingRepository.UpdateBooking(booking);
            return Ok(booking);
        }

     
        [HttpGet]
        public async Task<ActionResult<BookingModel>> GetAllBookings()
        {
            var booking = await _bookingRepository.GetAllBookings();
            return Ok(booking);
        }
      
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingModel>> GetBooking(Guid id)
        {
            try
            {
                var booking = await _bookingRepository.GetBooking(id);
                if(booking == null)
                {
                    return NotFound();
                }

                return Ok(booking);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
      
        [HttpPost]
        public async Task<ActionResult<BookingModel>> SetBooking([FromBody] BookingModel booking)
        {
            if (booking == null)
            {
                return BadRequest();
            }

            await _bookingRepository.AddBooking(booking);
            return Ok(booking);
        }      
    }
}