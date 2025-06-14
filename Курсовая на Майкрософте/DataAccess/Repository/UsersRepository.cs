using DataAccess.Entity;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repository
{
    public class UsersRepository : IRepository<Users>, IDisposable
    {

        public readonly ApplicationContext _context;

        public UsersRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetStatusList()
        {
            return _context.users.ToList();
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
