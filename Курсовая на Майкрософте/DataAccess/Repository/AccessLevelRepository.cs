using DataAccess.Entity;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Repository
{
    public class AccessLevelRepository : IRepository<Access_level>, IDisposable
    {
        public readonly ApplicationContext _context;

        public AccessLevelRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Access_level> GetStatusList()
        {
            return _context.access_Levels.ToList();
        }

        public Access_level GetStatus(int id)
        {
            return _context.access_Levels.Find(id);
        }

        public void Create(Access_level access_Levels)
        {
            _context.access_Levels.Add(access_Levels);
        }

        public void Delete(int id)
        {
            Access_level access_Levels = _context.access_Levels.Find(id);
            if (access_Levels != null)
            {
                _context.access_Levels.Remove(access_Levels);
            }
        }

        public void Update(Access_level access_Levels)
        {
            _context.Entry(access_Levels).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
