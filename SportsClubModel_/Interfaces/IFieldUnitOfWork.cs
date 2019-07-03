using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsClubModel.Interfaces
{
    public interface IFieldUnitOfWork
    {
        Task<Field[]> GetAllFieldsAsync();
        Task AddFieldAsync(Field field);
        Task RemoveFieldAsync(int fieldId);
        Task<Field> GetFieldByIdAsync(int fieldId);
        Task<bool> SaveChangesAsync();
        Task<Field[]> GetAllFieldsBySurfaceAsync(Surfaces surface);
        Task<Field[]> GetAllFieldsByPriceAsync(decimal price);

        //Fare metodi di ricerca
    }
}
