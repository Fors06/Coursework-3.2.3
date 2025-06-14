using Microsoft.EntityFrameworkCore;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDataAccess.Repository
{
    public class OrderStatusRepository : IRepository<Order_status>, IDisposable
    {
        public readonly ApplicationContext _context;

        public OrderStatusRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Order_status> GetStatusList()
        {
            return _context.order_Statuses.AsQueryable();
        }

        public Order_status GetStatus(int id)
        {
            return _context.order_Statuses.Find(id);
        }

        public void Create(Order_status order_Statuses)
        {
            _context.order_Statuses.Add(order_Statuses);
        }

        public void Delete(int id)
        {
            Order_status order_Statuses = _context.order_Statuses.Find(id);
            if (order_Statuses != null)
            {
                _context.order_Statuses.Remove(order_Statuses);
            }
        }

        public void Update(Order_status order_Statuses)
        {
            _context.Entry(order_Statuses).State = EntityState.Modified;
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
