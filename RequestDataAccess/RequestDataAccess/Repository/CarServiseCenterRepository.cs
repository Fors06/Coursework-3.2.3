using Microsoft.EntityFrameworkCore;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository.Abstraction;

namespace RequestDataAccess.Repository
{

    public class CarServiseCenterRepository : IRepository<Car_service_center>, IDisposable
    {
        public readonly ApplicationContext _context;

        public CarServiseCenterRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Car_service_center> GetStatusList()
        {
            return _context.car_Service_Centers.AsQueryable();
        }

        public Car_service_center FindServiceCenterByAddress(string address)
        {
            var normalizedAddress = CleanAndNormalize(address);
            return _context.car_Service_Centers.FirstOrDefault(c => CleanAndNormalize(c.Адрес) == normalizedAddress);
        }

        // Метод очистки строки для нормализации (без учёта запятых и точек)
        private static string CleanAndNormalize(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var cleanedInput = new string(input.Where(ch => char.IsLetterOrDigit(ch) || ch == ' ').ToArray()).Trim();
            return cleanedInput.ToLowerInvariant();
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