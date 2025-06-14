using DataAccess.Entity;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;



namespace DataAccess.Repository
{
    public class OrderRepository : IRepository<Order>, IDisposable
    {
        public readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetStatusList()
        {
            return _context.orders.ToList();
        }

        public Order GetStatus(int id)
        {
            return _context.orders.Find(id);
        }

        public void Create(Order tasks)
        {
            _context.orders.Add(tasks);
        }

        public void Delete(int id)
        {
            Order tasks = _context.orders.Find(id);
            if (tasks != null)
            {
                _context.orders.Remove(tasks);
            }
        }

        public void Update(Order tasks)
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
