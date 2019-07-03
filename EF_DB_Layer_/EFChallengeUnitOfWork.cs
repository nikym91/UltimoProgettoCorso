using Microsoft.EntityFrameworkCore;
using SportsClubModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DB_Layer
{
    public class EFChallengeUnitOfWork : IChallengeUnitOfWork
    {
        private IChallengeRepository challengeRepository;
        private IFieldRepository fieldRepository;
        private IReservationRepository reservationRepository;
        private IUserRepository userRepository;
        private ApplicationDbContext context;

        public EFChallengeUnitOfWork(IChallengeRepository challenge, IFieldRepository field, IReservationRepository reservation, IUserRepository user, ApplicationDbContext ctx)
        {
            challengeRepository = challenge;
            fieldRepository = field;
            reservationRepository = reservation;
            userRepository = user;
            context = ctx;
        }

        public async Task AddChallengeAsync(Challenge challenge)
        {
            try
            {
                challengeRepository.AddChallenge(challenge);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
            }
        }

        public async Task<Challenge[]> GetAllChallengesAsync()
        {
            return await challengeRepository.Challenges.ToArrayAsync();
        }

        public async Task<Challenge> GetChallengeByIdAsync(int challengeId)
        {
            return await challengeRepository.GetChallengeByIdAsync(challengeId);
        }

        public async Task RemoveChallengeAsync(int challengeId)
        {
            try
            {
                challengeRepository.RemoveChallenge(challengeId);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync() > 0);
        }
    }
}
