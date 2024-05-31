using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomApp.Data.Entities
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public string Type { get; set; }

        // Multiple guests can stay in the same room
        // 1-to-many relationship
        public ICollection<Guest> Guests { get; set; }
    }
}
