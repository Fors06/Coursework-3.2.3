using DataAccess.Entity;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

using System;

namespace DataAccess.Repository
{
    public class LoyaltyProgramRepository : IRepository<Loyalty_Program>, IDisposable
    {
        public readonly ApplicationContext _context;

        public LoyaltyProgramRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Loyalty_Program> GetStatusList()
        {
            return _context.loyalty_Programs.ToList();
        }

        public Loyalty_Program GetStatus(int id)
        {
            return _context.loyalty_Programs.Find(id);
        }

        public void Create(Loyalty_Program status)
        {
            _context.loyalty_Programs.Add(status);
        }

        public void Delete(int id)
        {
            Loyalty_Program status = _context.loyalty_Programs.Find(id);
            if (status != null)
            {
                _context.loyalty_Programs.Remove(status);
            }
        }

        public void Update(Loyalty_Program status)
        {
            _context.Entry(status).State = EntityState.Modified;
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
