using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPoint.Data.Models
{
    public class TestObjectDto : BaseDto
    {
        public string TestString { get; set; }
        public Guid? UserId { get; set; }
        public UserDto User { get; set; }
    }
}
