using AutoMapper;
using MedPoint.Data;
using MedPoint.Data.Entities;
using MedPoint.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPoint.Stores.Impl
{
    public class TestObjectStore : BaseStore<TestObject, TestObjectDto>, ITestObjectStore
    {
        public TestObjectStore(IMapper mapper, MedPointDbContext dbContext) : base(mapper, dbContext)
        {
        }
    }
}
