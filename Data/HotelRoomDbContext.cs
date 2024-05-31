using HotelRoomApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomApp.Data
{
    public class HotelRoomDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        //Constructor
        public HotelRoomDbContext(DbContextOptions<HotelRoomDbContext> options) : base(options)
        { }

        //This creates a table for the upcoming database
        //Property <Type>
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        //Configure our database provider
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   {

        //  if (!optionsBuilder.IsConfigured)
        //  {
        //     optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
        //         x => x.MigrationsAssembly("HotelRoom.Data"));
        //    }
        //database provider
        //   optionsBuilder.UseSqlServer(); 
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
