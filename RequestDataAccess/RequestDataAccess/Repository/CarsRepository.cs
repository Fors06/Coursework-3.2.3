using Microsoft.EntityFrameworkCore;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository.Abstraction;

namespace RequestDataAccess.Repository
{
    public class CarsRepository : IRepository<Cars>, IDisposable
    {
        public readonly ApplicationContext _context;

        public CarsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Cars> GetStatusList()
        {
            return _context.cars.AsQueryable();
        }

        public Cars GetStatus(int id)
        {
            return _context.cars.Find(id);
        }

        public void Create(Cars tasks)
        {
            _context.cars.Add(tasks);
        }

        public void Delete(int id)
        {
            Cars tasks = _context.cars.Find(id);
            if (tasks != null)
            {
                _context.cars.Remove(tasks);
            }
        }

        public void Update(Cars tasks)
        {
            _context.Entry(tasks).State = EntityState.Modified;
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

