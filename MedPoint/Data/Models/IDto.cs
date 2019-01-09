using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MedPoint.Data.Models
{
    public interface IDto
    {
        Guid Id { get; set; }
    }

    [Serializable]
    public class BaseDto : IDto
    {
        public Guid Id { get; set; }
    }
}
