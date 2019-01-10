using MedPoint.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPoint.Stores
{
    public interface IStore<TDto> where TDto : IDto
    {
        Task<TDto> Insert(TDto dto);
        Task<TDto> InsertOrUpdate(TDto dto);

        Task<TDto> GetById(Guid id);
        Task<TDto> GetByIdOrDefault(Guid id, TDto def = default(TDto));

        Task<bool> Update(TDto dto);
        Task<bool> UpdateRange(IReadOnlyList<TDto> dto);

        Task<bool> Delete(Guid id);
        Task<bool> Delete(TDto dto);
    }
}
