using DataAccess.Entity;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repository
{

    public class CarServiseCenterRepository : IRepository<Car_service_center>, IDisposable
    {
        public readonly ApplicationContext _context;

        public CarServiseCenterRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Car_service_center> GetStatusList()
        {
            return _context.car_Service_Centers.ToList();
        }

        public Car_service_center GetStatus(int id)
        {
            return _context.car_Service_Centers.Find(id);
        }

        public void Create(Car_service_center status)
        {
            _context.car_Service_Centers.Add(status);
        }

        public void Delete(int id)
        {
            Car_service_center status = _context.car_Service_Centers.Find(id);
            if (status != null)
            {
                _context.car_Service_Centers.Remove(status);
            }
        }

        public void Update(Car_service_center status)
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