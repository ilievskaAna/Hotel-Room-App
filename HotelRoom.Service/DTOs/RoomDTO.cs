using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoom.Service.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public string Type { get; set; }
    }
}
