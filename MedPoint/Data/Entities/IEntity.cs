using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedPoint.Data.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }

    [Serializable]
    public class BaseEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
