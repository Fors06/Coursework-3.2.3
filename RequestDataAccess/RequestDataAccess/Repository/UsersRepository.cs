using Microsoft.EntityFrameworkCore;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository.Abstraction;


namespace RequestDataAccess.Repository
{
    public class UsersRepository : IRepository<Users>, IDisposable
    {

        public readonly ApplicationContext _context;

        public UsersRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Users> GetStatusList()
        {
            return _context.users.AsQueryable();
        }

        public Users GetStatus(int id)
        {
            return _context.users.Find(id);
        }

        public void Create(Users users)
        {
            _context.users.Add(users);
        }

        public void Delete(int id)
        {
            Users users = _context.users.Find(id);
            if (users != null)
            {
                _context.users.Remove(users);
            }
        }

        public void Update(Users users)
        {
            _context.Entry(users).State = EntityState.Modified;
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
