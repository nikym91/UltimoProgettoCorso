using Microsoft.EntityFrameworkCore;
using SportsClubModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DB_Layer
{
    public class EFFieldRepository : IFieldRepository
    {
        private ApplicationDbContext context;

        public EFFieldRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Field> Fields => context.Fields;

        public void AddField(Field field)
        {
            context.Add(field);
        }

        public async Task<Field> GetFieldByIdAsync(int fieldId)
        {
            return await context.Fields.SingleOrDefaultAsync(r => r.FieldId == fieldId);
        }

        public IQueryable<Field> GetAllFieldsByPrice(decimal price)
        {
            return context.Fields.Where(p => p.Price == price);
        }

        public IQueryable<Field> GetAllFieldsBySurface(Surfaces surface)
        {
            return context.Fields.Where(s => s.Surface == surface);
        }

        public void RemoveField(int fieldId)
        {
            var field = context.Fields.Where(x => x.FieldId == fieldId).First();
            context.Remove(field);
        }
    }
}
