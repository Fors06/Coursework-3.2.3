using Microsoft.EntityFrameworkCore;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository.Abstraction;
using System.Linq;

namespace RequestDataAccess.Repository
{
    public class Type_UserRepository : IRepository<Type_User>, IDisposable
    {
        public readonly ApplicationContext _context;

        public Type_UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Type_User> GetStatusList()
        {
            return _context.type_Users.AsQueryable();
        }

        public Type_User GetStatus(int id)
        {
            return _context.type_Users.Find(id);
        }

        public void Create(Type_User type_Users)
        {
            _context.type_Users.Add(type_Users);
        }

        public void Delete(int id)
        {
            Type_User type_Users = _context.type_Users.Find(id);
            if (type_Users != null)
            {
                _context.type_Users.Remove(type_Users);
            }
        }

        public void Update(Type_User type_Users)
        {
            _context.Entry(type_Users).State = EntityState.Modified;
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
