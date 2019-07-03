using Microsoft.EntityFrameworkCore;
using SportsClubModel;
using SportsClubModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DB_Layer
{
    public class EFReservationUnitOfWork : IReservationUnitOfWork
    {
        private IChallengeRepository challengeRepository;
        private IFieldRepository fieldRepository;
        private IReservationRepository reservationRepository;
        private IUserRepository userRepository;
        private ApplicationDbContext context;

        public EFReservationUnitOfWork(IChallengeRepository challenge, IFieldRepository field, IReservationRepository reservation, IUserRepository user, ApplicationDbContext ctx)
        {
            challengeRepository = challenge;
            fieldRepository = field;
            reservationRepository = reservation;
            userRepository = user;
            context = ctx;
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            try
            {
                var user = userRepository.Users.SingleOrDefault(u => u.UserId == reservation.UserId);
                //Creare Eccezione custom
                reservation.User = user ?? throw new Exception();
                var field = fieldRepository.Fields.SingleOrDefault(f => f.FieldId == reservation.FieldId);
                reservation.Field = field ?? throw new Exception();
                reservation.Sport = ToSport(reservation.Field.GetType().Name);

                if (reservation.Price == 0)
                {

                    if (reservation.IsDoubleReservation())
                    {
                        reservation.Field.Players = 4;
                        reservation.Price = reservation.Field.Price * ((reservation.TimeEnd.Hour - reservation.TimeStart.Hour) + (reservation.TimeEnd.Minute - reservation.TimeStart.Minute) / 60) * 1.5m;

                    }
                    else
                    {
                        reservation.Price = reservation.Field.Price * ((reservation.TimeEnd.Hour - reservation.TimeStart.Hour) + (reservation.TimeEnd.Minute - reservation.TimeStart.Minute) / 60);
                    }
                    user.SpentMoney += reservation.Price;
                }
                if (reservation.IsChallenge)
                {
                    reservation.Challenge = new Challenge(reservation.Field.Players);
                    reservation.Challenge.Reservation = reservation;
                    //user.Challenges++;
                }
                reservationRepository.AddReservation(reservation);
                await context.SaveChangesAsync();
                user.Reservations++;
            }
            catch (DbUpdateException)
            {
            }
        }

        public async Task<Reservation[]> GetAllReservationsAsync()
        {
            return await reservationRepository.GetAllReservationsWithChallenges().ToArrayAsync();
        }

        public async Task<Reservation> GetReservationByReservationIdAsync(int reservationId)
        {
            return await reservationRepository.GetReservationByReservationIdAsync(reservationId);
        }

        public async Task<Reservation[]> GetReservationsByDateAsync(DateTime start, DateTime end)
        {
            return await reservationRepository.GetReservationsByDate(start, end).ToArrayAsync();
        }

        public async Task<Reservation[]> GetReservationsByFieldAsync(int fieldId)
        {
            return await reservationRepository.GetReservationsByField(fieldId).ToArrayAsync();
        }

        public async Task<Reservation[]> GetReservationsByUserIdAsync(int userId)
        {
            return await reservationRepository.GetReservationsByUserId(userId).ToArrayAsync();
        }

        public async Task RemoveReservationAsync(int reservationId)
        {
            try
            {
                reservationRepository.RemoveReservation(reservationId);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
            }
        }

        public string ToSport(string courtName)
        {
            string sport = null;

            if (courtName.Contains("Court"))
            {
                sport = courtName.Replace("Court", "");
            }
            else if (courtName.Contains("Field"))
            {
                sport = courtName.Replace("Field", "");
            }
            return sport;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync() > 0);
        }
    }
}
