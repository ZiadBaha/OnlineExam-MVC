using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.Entities.Common;
using OnlineExam.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Repositories.Auth.Admin
{
    public class GenericCrudService<TEntity, TAddDto, TUpdateDto, TGetDto>
     : IGenericCrudService<TAddDto, TUpdateDto, TGetDto>
     where TEntity : BaseEntity
    {
        protected readonly IMapper _mapper;
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericCrudService(IMapper mapper, DbContext context)
        {
            _mapper = mapper;
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<MessagesResponse<string>> AddAsync(TAddDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new MessagesResponse<string>(201, "Created");
        }

        public virtual async Task<MessagesResponse<string>> UpdateAsync(TUpdateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return new MessagesResponse<string>(200, "Updated");
        }

        public async Task<MessagesResponse<string>> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null)
                return new MessagesResponse<string>(404, null, "Not Found");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return new MessagesResponse<string>(204, "Deleted");
        }

        public async Task<MessagesResponse<TGetDto>> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null)
                return new MessagesResponse<TGetDto>(404, default, "Not Found");

            var dto = _mapper.Map<TGetDto>(entity);
            return new MessagesResponse<TGetDto>(200, dto);
        }

        public async Task<MessagesResponse<List<TGetDto>>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            var dtoList = _mapper.Map<List<TGetDto>>(entities);
            return new MessagesResponse<List<TGetDto>>(200, dtoList);
        }
    }

}
