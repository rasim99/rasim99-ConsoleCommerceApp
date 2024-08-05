using Core.Entities.Base;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repistories.Base
{
    public class Repistory<T> : IRepistory<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbTable;

        public Repistory(AppDbContext context)
        {
            _context = context;
            _dbTable = _context.Set<T>();
        }

        public List<T> GetAll()
        {
            return _dbTable.ToList();
        }
        public T Get(int id)
        {
            return _dbTable.FirstOrDefault(t => t.Id == id);
        }

        public void Add(T entity)
        {
            entity.CreateAt= DateTime.Now;
            _dbTable.Add(entity);
        }
        public void Update(T entity)
        {
          entity.ModifyAt= DateTime.Now;
            _dbTable.Update(entity);
        }
        public void Delete(T entity)
        {
            entity.IsDeleted= true;
            _dbTable.Update(entity);
        }
    }
}
