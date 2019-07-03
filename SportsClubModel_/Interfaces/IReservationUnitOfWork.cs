using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsClubModel.Interfaces
{
    public interface IReservationUnitOfWork
    {
        Task<Reservation[]> GetAllReservationsAsync();
        Task AddReservationAsync(Reservation reservation);
        Task RemoveReservationAsync(int reservationId);
        Task<Reservation[]> GetReservationsByFieldAsync(int fieldId);
        Task<Reservation[]> GetReservationsByUserIdAsync(int userId);
        Task<Reservation[]> GetReservationsByDateAsync(DateTime start, DateTime end);
        Task<Reservation> GetReservationByReservationIdAsync(int reservationId);
        Task<bool> SaveChangesAsync();
    }
}
