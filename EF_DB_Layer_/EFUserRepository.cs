using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsClubModel;

namespace EF_DB_Layer
{
    public class EFUserRepository : IUserRepository
    {
        private ApplicationDbContext context;

        public EFUserRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<User> Users => context.Users;

        public void AddUser(User user)
        {
            context.Add(user);
        }

        public void RemoveUser(int userId)
        {
            var user = context.Users.Where(x => x.UserId == userId).First();
            context.Remove(user);
        }

        public IQueryable<User> GetAllUsersByLastName(string token)
        {
            return context.Users.Where(r => r.LastName.Contains(token));

        }

        public IQueryable<User> GetUsersByDateOfBirthRange(DateTime start, DateTime end)
        {
            start = start.Date;
            end = end.Date;

            return context.Users.Where(r => r.BirthDate > start && r.BirthDate < end);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
        }
    }
}