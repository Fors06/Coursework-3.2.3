using Microsoft.EntityFrameworkCore;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository.Abstraction;

namespace RequestDataAccess.Repository
{
    public class EmployeeRepository : IRepository<Employee>, IDisposable
    {
        public readonly ApplicationContext _context;

        public EmployeeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> GetStatusList()
        {
            return _context.employees.AsQueryable();
        }

        public Employee GetStatus(int  id)
        {
            return _context.employees.Find(id);
        }

        public void Create(Employee Employees)
        {
            _context.employees.Add(Employees);
        }

        public void Delete(int id)
        {
            Employee employees = _context.employees.Find(id);
            if (employees != null)
            {
                _context.employees.Remove(employees);
            }
        }

        public void Update(Employee employees)
        {
            _context.Entry(employees).State = EntityState.Modified;
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
