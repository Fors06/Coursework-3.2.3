using DataAccess.Entity;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repository
{
    public class MalfunctionRepository : IRepository<Malfunction>, IDisposable
    {
        public readonly ApplicationContext _context;

        public MalfunctionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Malfunction> GetStatusList()
        {
            return _context.malfunctions.ToList();
        }

        public Malfunction GetStatus(int id)
        {
            return _context.malfunctions.Find(id);
        }

        public void Create(Malfunction tasks)
        {
            _context.malfunctions.Add(tasks);
        }

        public void Delete(int id)
        {
            Malfunction tasks = _context.malfunctions.Find(id);
            if (tasks != null)
            {
                _context.malfunctions.Remove(tasks);
            }
        }

        public void Update(Malfunction tasks)
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
