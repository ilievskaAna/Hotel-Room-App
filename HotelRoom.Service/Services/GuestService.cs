using AutoMapper;
using HotelRoom.Service.DTOs;
using HotelRoom.Service.Interfaces;
using HotelRoomApp.Data.Entities;
using HotelRoomApp.Data.Interfaces;

namespace HotelRoom.Service.Services
{
        public class GuestService : IGuestService
        {
            private readonly IGuestRepository _guestRepository;
            private readonly IRoomRepository _roomRepository;
            private readonly IMapper _mapper;

            //Dependency Injection tho the constructor
            public GuestService(IGuestRepository guestRepository, IRoomRepository roomRepository, IMapper mapper)
            {
                _guestRepository = guestRepository;
                _roomRepository = roomRepository;
                _mapper = mapper;
            }

            public List<GuestDTO> GetAllGuests()
            {
                var guests = _guestRepository.GetGuests();
                return _mapper.Map<List<GuestDTO>>(guests);
            }

            public GuestDTO GetGuestById(int id)
            {
                var guest = _guestRepository.GetGuestById(id);
                return _mapper.Map<GuestDTO>(guest);
            }

            public GuestDTO AddGuest(GuestDTO guestDto)
            {
                var room = _roomRepository.GetRoomById(guestDto.RoomId);
                if (room == null)
                {
                    throw new Exception("Room not found");
                }

                var guest = _mapper.Map<Guest>(guestDto);
                guest.Id = 0;

                _guestRepository.AddGuest(guest);

                return _mapper.Map<GuestDTO>(guest);
        }

            public GuestDTO UpdateGuest(GuestDTO guest)
            {
                Guest newGuest = _mapper.Map<Guest>(guest);
                Guest oldGuest = _guestRepository.GetGuestById(guest.Id);

            if (oldGuest != null)
            {
                _guestRepository.UpdateGuest(oldGuest, newGuest);
            }

            return _mapper.Map<GuestDTO>(newGuest);
        }

            public bool DeleteGuest(int id)
            {
                var guest = _guestRepository.GetGuestById(id);
                if (guest != null)
                {
                    return _guestRepository.DeleteGuest(id);
                }

                return false;
            }

            public ICollection<GuestDTO> GetGuestsByRoomId(int roomId)
            {
                var guests = _guestRepository.GetGuestsByRoomId(roomId);
                return _mapper.Map<ICollection<GuestDTO>>(guests);
            }
    }
   }
