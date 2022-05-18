using Microsoft.AspNetCore.Mvc;
using RentalsAPI.DTO;
using RentalsAPI.Models;
using RentalsAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentalsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HouseListingsController : ControllerBase
    {
        private IHouseListingRepository _houseListingRepository;

        public HouseListingsController(IHouseListingRepository houseListing)
        {
            _houseListingRepository = houseListing;
        }

        // GET: api/<HouseListingsController>
        [HttpGet]
        public async Task<ActionResult<HouseListing>> Get()
        {
            var house = await _houseListingRepository.GetAllHouseListings();
            return Ok(house);
        }

        // GET api/<HouseListingsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingModel>> GetHouseListing(Guid id)
        {
            var booking = await _houseListingRepository.GetHouseListing(id);
            return Ok(booking);
        }

        // POST api/<HouseListingsController>
        [HttpPost]
        public async Task<ActionResult<HouseListingDTO>> Post([FromBody] HouseListing houseListing)
        {
            if (houseListing == null)
            {
                return BadRequest();
            }

            await _houseListingRepository.AddHouseListing(houseListing);
            return Ok(houseListing);
        }

        // PUT api/<HouseListingsController>/5
        [HttpPut("{id}")]


        // DELETE api/<HouseListingsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HouseListingDTO>> Delete(Guid id)
        {
            await _houseListingRepository.DeleteHouseListing(id);
            return Ok();
        }
    }
}