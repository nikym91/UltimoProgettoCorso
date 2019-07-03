using SportsClubModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClubModel
{
    public interface IFieldRepository
    {
        IQueryable<Field> Fields { get; }
        void AddField(Field field);
        void RemoveField(int fieldId);
        Task<Field> GetFieldByIdAsync(int fieldId);
        IQueryable<Field> GetAllFieldsBySurface(Surfaces surface);
        IQueryable<Field> GetAllFieldsByPrice(decimal price);

    }
}
