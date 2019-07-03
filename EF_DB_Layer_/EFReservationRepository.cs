using Microsoft.EntityFrameworkCore;
using SportsClubModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DB_Layer
{
    public class EFReservationRepository : IReservationRepository
    {
        private ApplicationDbContext context;

        public EFReservationRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Reservation> Reservations => context.Reservations.Include(u=> u.User).Include(f => f.Field).Include(c => c.Challenge);

        public void AddReservation(Reservation reservation)
        {
            context.Add(reservation);
        }

        public void RemoveReservation(int reservationId)
        {

            var res = context.Reservations.Where(r => r.ReservationId == reservationId).Single();

            if (res.IsChallenge)
            {
                var cha = context.Challenges.Where(c => c.ChallengeId == res.Challenge.ChallengeId).Single();
                context.Challenges.Remove(cha);
            }
            context.Reservations.Remove(res);
        }

        public IQueryable<Reservation> GetReservationsByField(int fieldId)
        {
           return context.Reservations.Include(r => r.Challenge).Where(r => r.FieldId == fieldId);
        }

        public IQueryable<Reservation> GetReservationsByUserId(int userId)
        {
            return context.Reservations.Include(r => r.Challenge).Where(r => r.UserId == userId);
        }

        public IQueryable<Reservation> GetReservationsByDate(DateTime start, DateTime end)
        {
            start = start.Date;
            end = end.Date;

            return context.Reservations.Include(r => r.Challenge).Where(r => r.Date > start && r.Date < end);
        }

        public async Task<Reservation> GetReservationByReservationIdAsync(int reservationId)
        {
            return await context.Reservations.Include(r => r.Challenge).SingleOrDefaultAsync(r => r.ReservationId == reservationId);
        }

        public IQueryable<Reservation> GetAllReservationsWithChallenges()
        {
            return context.Reservations.Include(r => r.Challenge);
        }
    }
}
