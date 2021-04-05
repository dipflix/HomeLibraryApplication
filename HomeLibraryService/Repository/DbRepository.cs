using HomeLibraryService.Context;
using HomeLibraryService.Interfaces;
using HomeLibraryData.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLibraryService.Repository
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        public bool AutoSave { get; set; } = true;
        protected readonly DBContext _context;
        protected DbSet<T> _dbSet;
        public virtual IQueryable<T> Items => _dbSet;
        public DbRepository()
        {
            _context = new DBContext();
            _dbSet = _context.Set<T>();
        }

        

        public T Get(int id) => Items.SingleOrDefault(entity => entity.Id.Equals(id));

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default) => await Items
           .SingleOrDefaultAsync(entity => entity.Id.Equals(id), Cancel)
           .ConfigureAwait(false);

        public T Add(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

           _context.Entry(entity).State = EntityState.Added;

            if (AutoSave) _context.SaveChanges();

            return entity;
        }

        public async Task<T> AddAsync(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Added;

            if (AutoSave) await _context.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return entity;
        }


        public void Update(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            var item = Get(entity.Id);
            _context.Entry(item).CurrentValues.SetValues(entity);
            
            if (AutoSave) _context.SaveChanges();
      
        }

        public async Task UpdateAsync(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            var item = Get(entity.Id);
            _context.Entry(item).CurrentValues
                .SetValues(entity);
            //_context.Entry(entity).State = EntityState.Modified;

            if (AutoSave) await _context.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public void Remove(int id)
        {
            var item = _dbSet.Local.FirstOrDefault(i => i.Id == id) ?? new T { Id = id };

            _dbSet.Remove(item);
            //_context.Entry(item).State = EntityState.Deleted;
            if (AutoSave) _context.SaveChanges();

        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            _dbSet.Remove(new T { Id = id });

            if (AutoSave) await _context.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

 
    }
}

