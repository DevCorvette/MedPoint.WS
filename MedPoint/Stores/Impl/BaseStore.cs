using AutoMapper;
using MedPoint.Data;
using MedPoint.Data.Entities;
using MedPoint.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedPoint.Extensions;
using MedPoint.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MedPoint.Stores.Impl
{
    public class BaseStore<TEntity, TDto> : IStore<TDto>
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        protected IMapper Mapper { get; }
        protected MedPointDbContext DbContext { get; }

        public BaseStore(IMapper mapper, MedPointDbContext dbContext)
        {
            Mapper = mapper;
            DbContext = dbContext;
        }

        protected TDto Map(TEntity entity)
        {
            return Mapper.Map<TDto>(entity);
        }

        protected TEntity Map(TDto dto)
        {
            return Mapper.Map<TEntity>(dto);
        }

        public async Task<TDto> Insert(TDto dto)
        {
            var entity = Map(dto);
            try
            {
                DbContext.DetachAll();

                await DbContext.AddAsync(entity);
                await DbContext.SaveChangesAsync();

                return Map(entity);
            }
            finally
            {
                DbContext.Entry(entity).State = EntityState.Detached;
            }
        }

        public async Task<TDto> InsertOrUpdate(TDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                dto = await Insert(dto);
            }
            else
            {
                var entity = await DbContext.FindAsync<TEntity>(dto.Id);
                if (entity == null)
                {
                    dto = await Insert(dto);
                }
                else
                {
                    DbContext.Entry(entity).State = EntityState.Detached;
                    await Update(dto);
                }
            }
            return dto;
        }

        public async Task<TDto> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new EntityNotFoundException();
            }

            var entity = await DbContext.FindAsync<TEntity>(id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            DbContext.Entry(entity).State = EntityState.Detached;
            return Map(entity);
        }

        public async Task<TDto> GetByIdOrDefault(Guid id, TDto def = default(TDto))
        {
            try
            {
                return await GetById(id);
            }
            catch (EntityNotFoundException)
            {
                return def;
            }
        }

        public async Task<bool> Update(TDto dto)
        {
            return await UpdateRange(new[] { dto });
        }

        public async Task<bool> UpdateRange(IReadOnlyList<TDto> dtos)
        {
            var entities = dtos.Select(Map).ToArray();
            try
            {
                DbContext.DetachAll();

                foreach (var entity in entities)
                {
                    DbContext.Attach(entity);
                    DbContext.Entry(entity).State = EntityState.Modified;
                }

                var result = await DbContext.SaveChangesAsync();
                return result > 0;
            }
            finally
            {
                foreach (var entity in entities)
                {
                    DbContext.Entry(entity).State = EntityState.Detached;
                }
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var dto = await GetById(id);
                return await Delete(dto);
            }
            catch (EntityNotFoundException)
            {
                return false;
            }
        }

        public async Task<bool> Delete(TDto dto)
        {
            var entity = Map(dto);
            try
            {
                DbContext.DetachAll();

                DbContext.Remove(entity);
                var result = await DbContext.SaveChangesAsync();
                return result > 0;
            }
            finally
            {
                DbContext.Entry(entity).State = EntityState.Detached;
            }
        }
    }
}
