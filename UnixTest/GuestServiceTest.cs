
using AutoMapper;
using HotelRoom.Service.DTOs;
using HotelRoom.Service.Services;
using HotelRoomApp.Data.Entities;
using HotelRoomApp.Data.Interfaces;
using Moq;
using Xunit;
using System.Collections.Generic;
using System;

namespace UnixTest
{
    public class GuestServiceTest
    {
        private readonly Mock<IGuestRepository> guestRepositoryMock;
        private readonly Mock<IRoomRepository> roomRepositoryMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly GuestService guestService;

        public GuestServiceTest()
        {
            guestRepositoryMock = new Mock<IGuestRepository>();
            roomRepositoryMock = new Mock<IRoomRepository>();
            mapperMock = new Mock<IMapper>();
            guestService = new GuestService(guestRepositoryMock.Object, roomRepositoryMock.Object, mapperMock.Object);
        }

        private Guest GetGuest()
        {
            return new Guest()
            {
                Id = 1,
                FirstName = "Ana",
                LastName = "Ilievska",
                DOB = new DateTime(2002, 4, 6),
                Address = "Partizanska",
                Nationality = "Macedonian",
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(3),
                RoomId = 101
            };
        }

        private GuestDTO GetGuestDTO()
        {
            return new GuestDTO()
            {
                Id = 1,
                FirstName = "Ana",
                LastName = "Ilievska",
                DOB = new DateTime(2002, 4, 6),
                Address = "Partizanska",
                Nationality = "Macedonian",
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(3),
                RoomId = 101
            };
        }

        private List<Guest> GetGuests()
        {
            return new List<Guest>
            {
                new Guest()
                {
                    Id = 1,
                    FirstName = "Ana",
                    LastName = "Ilievska",
                    DOB = new DateTime(2002, 4, 6),
                    Address = "Partizanska",
                    Nationality = "Macedonian",
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(3),
                    RoomId = 101
                },
                new Guest()
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    DOB = new DateTime(1990, 1, 1),
                    Address = "456 Main St",
                    Nationality = "American",
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(3),
                    RoomId = 102
                }
            };
        }

        [Fact]
        public void GetAllGuests_ShouldReturnAllGuests()
        {
            // Arrange
            var guestList = GetGuests();
            var guestDTOList = new List<GuestDTO>
            {
                new GuestDTO { Id = 1, FirstName = "Ana", LastName = "Ilievska", DOB = new DateTime(2002, 4, 6), Address = "Partizanska", Nationality = "Macedonian", CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(3), RoomId = 101 },
                new GuestDTO { Id = 2, FirstName = "John", LastName = "Doe", DOB = new DateTime(1990, 1, 1), Address = "456 Main St", Nationality = "American", CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(3), RoomId = 102 }
            };

            guestRepositoryMock.Setup(repo => repo.GetGuests()).Returns(guestList);
            mapperMock.Setup(m => m.Map<List<GuestDTO>>(guestList)).Returns(guestDTOList);

            // Act
            var result = guestService.GetAllGuests();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(guestDTOList, result);
        }

        [Fact]
        public void GetGuestById_ShouldReturnGuest_WhenGuestExists()
        {
            // Arrange
            var guest = GetGuest();
            var guestDTO = GetGuestDTO();

            guestRepositoryMock.Setup(repo => repo.GetGuestById(1)).Returns(guest);
            mapperMock.Setup(m => m.Map<GuestDTO>(guest)).Returns(guestDTO);

            // Act
            var result = guestService.GetGuestById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(guestDTO, result);
        }

        [Fact]
        public void AddGuest_ShouldCallRepositoryAddGuest()
        {
            // Arrange
            var guest = GetGuest();
            var guestDTO = GetGuestDTO();
            var room = new Room { Id = 101 };

            roomRepositoryMock.Setup(repo => repo.GetRoomById(guestDTO.RoomId)).Returns(room);
            mapperMock.Setup(m => m.Map<Guest>(guestDTO)).Returns(guest);
            mapperMock.Setup(m => m.Map<GuestDTO>(guest)).Returns(guestDTO);

            // Act
            var result = guestService.AddGuest(guestDTO);

            // Assert
            guestRepositoryMock.Verify(repo => repo.AddGuest(guest), Times.Once);
            Assert.Equal(guestDTO, result);
        }
    }
}
