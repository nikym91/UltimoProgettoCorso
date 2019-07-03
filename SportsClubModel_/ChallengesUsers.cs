using System;
using System.Collections.Generic;
using System.Text;

namespace SportsClubModel
{
    public class ChallengesUsers
    {
            public int ChallengeId { get; set; }
            public Challenge Challenge { get; set; }
            public int UserId { get; set; }
            public User User { get; set; }
    }
}
