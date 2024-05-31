using AutoMapper;
using HotelRoom.Service.Interfaces;
using HotelRoom.Service.Profiles;
using HotelRoom.Service.Services;
using HotelRoom.WebApi.Infrastructure;
using HotelRoomApp.Data;
using HotelRoomApp.Data.Interfaces;
using HotelRoomApp.Repositories;
using Microsoft.EntityFrameworkCore;


namespace HotelRoomApp
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            

            //Connection strings
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Configuration = configuration;

            builder.Services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            builder.Services.AddDbContextPool<HotelRoomDbContext>((serviceProvider, options) =>
            {
                 options.UseSqlServer(Configuration.GetSection("ConnectionStrings")
                   .GetSection("DefaultConnection").Value,
                    x => x.MigrationsAssembly("HotelRoom.Data"));
            });

            // Mappers

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GuestProfile());
                mc.AddProfile(new RoomProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            //Services
            builder.Services.AddScoped<IGuestService, GuestService>();
            builder.Services.AddScoped<IRoomService, RoomService>();

            builder.Services.AddScoped<IGuestRepository, GuestRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}