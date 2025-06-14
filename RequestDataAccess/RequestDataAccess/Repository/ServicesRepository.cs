using Microsoft.EntityFrameworkCore;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository.Abstraction;


namespace RequestDataAccess.Repository
{
    public class ServicesRepository : IRepository<Services>, IDisposable
    {

        public readonly ApplicationContext _context;

        public ServicesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Services> GetStatusList()
        {
            return _context.serviceses.AsQueryable();
        }

        public Services GetStatus(int id)
        {
            return _context.serviceses.Find(id);
        }

        public void Create(Services Servicess)
        {
            _context.serviceses.Add(Servicess);
        }

        public void Delete(int id)
        {
            Services Servicess = _context.serviceses.Find(id);
            if (Servicess != null)
            {
                _context.serviceses.Remove(Servicess);
            }
        }

        public void Update(Services Servicess)
        {
            _context.Entry(Servicess).State = EntityState.Modified;
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
