using Microsoft.EntityFrameworkCore;
using SportsClubModel;
using SportsClubModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EF_DB_Layer
{
    public class EFUserUnitOfWork :IUserUnitOfWork
    {
        private IChallengeRepository challengeRepository;
        private IFieldRepository fieldRepository;
        private IReservationRepository reservationRepository;
        private IUserRepository userRepository;
        private ApplicationDbContext context;

        public EFUserUnitOfWork(IChallengeRepository challenge, IFieldRepository field, IReservationRepository reservation, IUserRepository user, ApplicationDbContext ctx)
        {
            challengeRepository = challenge;
            fieldRepository = field;
            reservationRepository = reservation;
            userRepository = user;
            context = ctx;
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            return await userRepository.Users.ToArrayAsync();
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                userRepository.AddUser(user);
                await context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
            }
        }

        public async Task<User[]> GetAllUsersByLastNameAsync(string token)
        {
            return await userRepository.GetAllUsersByLastName(token).ToArrayAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await userRepository.GetUserByIdAsync(userId);
        }

        public async Task<User[]> GetUsersByDateOfBirthRangeAsync(DateTime start, DateTime end)
        {
            return await userRepository.GetUsersByDateOfBirthRange(start, end).ToArrayAsync();
        }

        public async Task RemoveUserAsync(int userId)
        {
            try
            {
                userRepository.RemoveUser(userId);
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
