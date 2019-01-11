using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPoint.Data.Entities
{
    public class TestObject : BaseEntity
    {
        public string TestString { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
