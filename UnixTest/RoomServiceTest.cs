using HotelRoom.Service.Services;
using HotelRoomApp.Data.Entities;
using HotelRoomApp.Data.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnixTest
{
    using AutoMapper;
    using HotelRoom.Service.DTOs;
    using HotelRoom.Service.Services;
    using HotelRoomApp.Data.Entities;
    using HotelRoomApp.Data.Interfaces;
    using Moq;
    using Xunit;
    using System.Collections.Generic;

    namespace UnixTest
    {
        public class RoomServiceTest
        {
            private readonly Mock<IRoomRepository> roomRepositoryMock;
            private readonly Mock<IMapper> mapperMock;
            private readonly RoomService roomService;

            public RoomServiceTest()
            {
                roomRepositoryMock = new Mock<IRoomRepository>();
                mapperMock = new Mock<IMapper>();
                roomService = new RoomService(roomRepositoryMock.Object, mapperMock.Object);
            }

            private Room GetRoom()
            {
                return new Room()
                {
                    Id = 1,
                    Number = 101,
                    Floor = 1,
                    Type = "Standard"
                };
            }

            private RoomDTO GetRoomDTO()
            {
                return new RoomDTO()
                {
                    Id = 1,
                    Number = 101,
                    Floor = 1,
                    Type = "Standard"
                };
            }

            private List<Room> GetRooms()
            {
                return new List<Room>
            {
                new Room { Id = 1, Number = 101, Floor = 1, Type = "Standard" },
                new Room { Id = 2, Number = 102, Floor = 1, Type = "Deluxe" }
            };
            }

            [Fact]
            public void GetRooms_ShouldReturnAllRooms()
            {
                // Arrange
                var roomList = GetRooms();
                var roomDTOList = new List<RoomDTO>
            {
                new RoomDTO { Id = 1, Number = 101, Floor = 1, Type = "Standard" },
                new RoomDTO { Id = 2, Number = 102, Floor = 1, Type = "Deluxe" }
            };

                roomRepositoryMock.Setup(repo => repo.GetRooms()).Returns(roomList);
                mapperMock.Setup(m => m.Map<List<RoomDTO>>(roomList)).Returns(roomDTOList);

                // Act
                var result = roomService.GetRooms();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count);
                Assert.Equal(roomDTOList, result);
            }

            [Fact]
            public void GetRoomById_ShouldReturnRoom_WhenRoomExists()
            {
                // Arrange
                var room = GetRoom();
                var roomDTO = GetRoomDTO();

                roomRepositoryMock.Setup(repo => repo.GetRoomById(1)).Returns(room);
                mapperMock.Setup(m => m.Map<RoomDTO>(room)).Returns(roomDTO);

                // Act
                var result = roomService.GetRoomById(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(roomDTO, result);
            }

            [Fact]
            public void AddRoom_ShouldCallRepositoryAddRoom()
            {
                // Arrange
                var room = GetRoom();
                var roomDTO = GetRoomDTO();

                mapperMock.Setup(m => m.Map<Room>(roomDTO)).Returns(room);
                mapperMock.Setup(m => m.Map<RoomDTO>(room)).Returns(roomDTO);

                // Act
                var result = roomService.AddRoom(roomDTO);

                // Assert
                roomRepositoryMock.Verify(repo => repo.AddRoom(room), Times.Once);
                Assert.Equal(roomDTO, result);
            }
        }
    }
}