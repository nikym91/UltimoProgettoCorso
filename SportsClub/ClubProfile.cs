using AutoMapper;
using SportsClubModel;
using SportsClubWeb.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsClubWeb
{
    public class ClubProfile : Profile
    {
        public ClubProfile()
        {
            this.CreateMap<User, UserDTO>()
                .ReverseMap();

            this.CreateMap<TennisCourt, FieldDTO>().ForMember(f => f.Sport, opt => opt.MapFrom(source => ToSport(source.GetType().Name)));
            this.CreateMap<PaddleCourt, FieldDTO>().ForMember(f => f.Sport, opt => opt.MapFrom(source => ToSport(source.GetType().Name)));
            this.CreateMap<SoccerField, FieldDTO>().ForMember(f => f.Sport, opt => opt.MapFrom(source => ToSport(source.GetType().Name)));

            this.CreateMap<FieldDTO, TennisCourt>().ForMember(f => f.Players, opt => opt.MapFrom(source => source.Players));
            this.CreateMap<FieldDTO, PaddleCourt>().ForMember(f => f.Players, opt => opt.MapFrom(source => source.Players));
            this.CreateMap<FieldDTO, SoccerField>().ForMember(f => f.Players, opt => opt.MapFrom(source => source.Players));

            this.CreateMap<Reservation, ReservationsDTO>()
                .ReverseMap()
                .ForMember(r => r.Challenge, opt => opt.Ignore());

            //Challenge?

        }

        public string ToSport(string courtName)
        {
            string sport = null;

            if (courtName.Contains("Court"))
            {
                sport = courtName.Replace("Court", "");
            }
            else if (courtName.Contains("Field"))
            {
                sport = courtName.Replace("Field", "");
            }
            return sport;
        }
    }
}
