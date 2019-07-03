using Microsoft.EntityFrameworkCore;
using SportsClubModel;
using SportsClubModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EF_DB_Layer
{
    public class EFFieldUnitOfWork : IFieldUnitOfWork
    {
        private IChallengeRepository challengeRepository;
        private IFieldRepository fieldRepository;
        private IReservationRepository reservationRepository;
        private IUserRepository userRepository;
        private ApplicationDbContext context;

        public EFFieldUnitOfWork(IChallengeRepository challenge, IFieldRepository field, IReservationRepository reservation, IUserRepository user, ApplicationDbContext ctx)
        {
            challengeRepository = challenge;
            fieldRepository = field;
            reservationRepository = reservation;
            userRepository = user;
            context = ctx;
        }

        public async Task<Field[]> GetAllFieldsAsync()
        {
            return await fieldRepository.Fields.ToArrayAsync();
        }

        public async Task AddFieldAsync(Field field)
        {
            try
            {
                fieldRepository.AddField(field);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
            }
        }

        public async Task<Field> GetFieldByIdAsync(int fieldId)
        {
            return await fieldRepository.GetFieldByIdAsync(fieldId);
        }

        public async Task RemoveFieldAsync(int fieldId)
        {
            try
            {
                fieldRepository.RemoveField(fieldId);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
            }
        }

        public async Task<Field[]> GetAllFieldsBySurfaceAsync(Surfaces surface)
        {
            return await fieldRepository.GetAllFieldsBySurface(surface).ToArrayAsync();
        }

        public async Task<Field[]> GetAllFieldsByPriceAsync(decimal price)
        {
            return await fieldRepository.GetAllFieldsByPrice(price).ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync() > 0);
        }
    }
}
