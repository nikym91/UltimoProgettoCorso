using SportsClubModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClubModel
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void AddUser(User user);
        void RemoveUser(int userId);
        IQueryable<User> GetAllUsersByLastName(string token);
        IQueryable<User> GetUsersByDateOfBirthRange(DateTime start, DateTime end);
        Task<User> GetUserByIdAsync(int userId);
    }
}
