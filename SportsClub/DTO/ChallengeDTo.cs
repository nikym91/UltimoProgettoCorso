using SportsClubModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsClubWeb.DTO
{
    public class ChallengeDTO
    {
        public int ChallengeId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<User> Team1 { get; set; }
        public List<User> Team2 { get; set; }
        public int PlayersToInsert { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
