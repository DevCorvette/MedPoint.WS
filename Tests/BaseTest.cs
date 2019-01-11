using MedPoint.Data;
using MedPoint.Data.Entities;
using MedPoint.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class BaseTest
    {
        protected MedPointDbContext DbContext { get; }

        public BaseTest()
        {
            DbContext = DefaultServiceProvider.GetService<MedPointDbContext>();
        }

        public async Task InsertForTest(params IEntity[] entities)
        {
            Array.ForEach(entities, async e => await DbContext.AddAsync(e));

            await DbContext.SaveChangesAsync();
            DbContext.DetachAll();
        }

        public async Task UpdateForTest(params IEntity[] entities)
        {
            foreach (var entity in entities)
            {
                DbContext.Entry(entity).State = EntityState.Modified;
                await DbContext.AddAsync(entity);
            }
            await DbContext.SaveChangesAsync();
            DbContext.DetachAll();
        }
    }
}
