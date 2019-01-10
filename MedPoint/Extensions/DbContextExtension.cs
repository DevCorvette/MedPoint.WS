using MedPoint.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedPoint.Extensions
{
    public static class DbContextExtension
    {
        public static void DetachAll(this MedPointDbContext dbContext)
        {
            dbContext.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Detached);
        }
    }
}
