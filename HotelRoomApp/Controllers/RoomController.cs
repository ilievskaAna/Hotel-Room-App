using Microsoft.AspNetCore.Mvc;
using HotelRoom.Service.Interfaces;
using HotelRoom.Service.DTOs;
using System.Collections.Generic;

namespace HotelRoom.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IRoomService roomService, ILogger<RoomController> logger)
        {
            _roomService = roomService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllRooms")]
        public IEnumerable<RoomDTO> GetRooms()
        {
            var rooms = _roomService.GetRooms();
            return rooms;
        }

        [HttpGet]
        [Route("GetRoomById/{id:int}")]
        public IActionResult GetRoomById(int id)
        {
            RoomDTO room = _roomService.GetRoomById(id);

            if (room == null)
            {
                return NotFound("Room with that id doesn't exist");
            }

            return Ok(room);
        }

        [HttpPost]
        [Route("AddRoom")]
        public IActionResult AddRoom([FromBody] RoomDTO room)
        {
            if (ModelState.IsValid)
            {
                var newRoom = _roomService.AddRoom(room);
                return CreatedAtAction(nameof(GetRoomById), new { id = newRoom.Id }, newRoom);
            }

            return UnprocessableEntity(ModelState);
        }

        [HttpPut]
        [Route("UpdateRoom/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] RoomDTO room)
        {
            if (ModelState.IsValid)
            {
                room.Id = id;
                var result = _roomService.UpdateRoom(id, room);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("RemoveRoom/{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var result = _roomService.DeleteRoom(id);
                if (result)
                {
                    return Ok("Room deleted successfully");
                }
                else
                {
                    return NotFound("Room with that id doesn't exist");
                }
            }

            return BadRequest(ModelState);
        }
    }
}
