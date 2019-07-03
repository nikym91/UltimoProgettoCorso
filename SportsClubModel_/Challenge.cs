using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportsClubModel
{
    public class Challenge
    {
        [Key]
        public int ChallengeId { get; set; }
        public int PlayersToInsert { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public ICollection<ChallengesUsers> ChallengesUsers { get; set; }

        public Challenge() { }

        public Challenge(int playersToInsert)
        {
            PlayersToInsert = playersToInsert;
        }
    }
}
