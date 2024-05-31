using HotelRoom.Service.DTOs;
using HotelRoom.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelRoom.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        [Route("GetAllGuests")]
        public IEnumerable<GuestDTO> GetGuests()
        {
            var guests = _guestService.GetAllGuests();
            return guests;
        }

        [HttpGet]
        [Route("GetGuestsById")]
        public IActionResult GetGuestsById(int id)
        {
            GuestDTO guest = _guestService.GetGuestById(id);

            if (guest == null)
            {
                return NotFound("Guest with that id doesn't exist");
            }

            return Ok(guest);
        }

        [HttpPost]
        [Route("AddGuest")]
        public IActionResult AddGuest([FromBody] GuestDTO guest)
        {
            if (ModelState.IsValid)
            {
                var newGuest = _guestService.AddGuest(guest);
                return Created($"Guest with {newGuest} has been created", newGuest.Id);
            }

            return UnprocessableEntity(ModelState);
        }

        [HttpPut]
        [Route("UpdateGuest/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] GuestDTO guest)
        {
            if (ModelState.IsValid)
            {
                guest.Id = id;
                var result = _guestService.UpdateGuest(guest);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }

            return BadRequest();
        }

        [HttpDelete("RemoveGuest/{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(_guestService.DeleteGuest(id));
            }

            return BadRequest();
        }



    }
}
