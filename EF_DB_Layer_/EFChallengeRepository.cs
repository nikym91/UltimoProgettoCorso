using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsClubModel;

namespace EF_DB_Layer
{
    public class EFChallengeRepository : IChallengeRepository
    {
        private ApplicationDbContext context;

        public EFChallengeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Challenge> Challenges => context.Challenges;

        public void AddChallenge(Challenge challenge)
        {
            context.Add(challenge);
        }

        public void RemoveChallenge(int challengeId)
        {
            //conviene usare il metodo di get?
            var chal = context.Challenges.Where(ch => ch.ChallengeId == challengeId).Single();
            context.Remove(chal);
        }

        public async Task<Challenge> GetChallengeByIdAsync(int challengeId)
        {
            return await context.Challenges.SingleOrDefaultAsync(ch => ch.ChallengeId == challengeId);
        }
        
        //public void AddTeam(int userId,int reservationId)
        //{
        //    User user = context.Users.SingleOrDefault(u => u.UserId == userId);
        //    Reservation r = context.Reservations.SingleOrDefault(re=>re.ReservationId==reservationId);
        //    for(int i=0;i<r.Field.Players/2;i++)
        //    {
                
        //    }
             
        //}
    }
}
