using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClubModel
{
    public interface IChallengeRepository
    {
        IQueryable<Challenge> Challenges { get; }
        void AddChallenge(Challenge challenge);
        void RemoveChallenge(int challengeId);
        Task<Challenge> GetChallengeByIdAsync(int challengeId);

    }
}
