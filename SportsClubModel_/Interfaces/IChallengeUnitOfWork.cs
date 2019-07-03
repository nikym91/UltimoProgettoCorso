using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClubModel
{
    public interface IChallengeUnitOfWork
    {
        Task<Challenge[]> GetAllChallengesAsync();
        Task AddChallengeAsync(Challenge challenge);
        Task RemoveChallengeAsync(int challengeId);
        Task<Challenge> GetChallengeByIdAsync(int challengeId);
        Task<bool> SaveChangesAsync();
    }
}
