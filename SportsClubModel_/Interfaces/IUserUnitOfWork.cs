using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClubModel.Interfaces
{
    public interface IUserUnitOfWork
    {
        Task<User[]> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task RemoveUserAsync(int userId);
        Task<User[]> GetAllUsersByLastNameAsync(string token);
        Task<User[]> GetUsersByDateOfBirthRangeAsync(DateTime start, DateTime end);
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> SaveChangesAsync();
    }
}
