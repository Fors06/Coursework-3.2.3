using DataAccess;
using DataAccess.Entity;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repository
{
    public class ClientRepository : IRepository<Client>, IDisposable
    {
        
    
        public readonly ApplicationContext _context;

        public ClientRepository(ApplicationContext context)
        {
            _context = context;
        }
        //public string GetClientNameById(int clientId)
        //{
        //    return _context.clients.FirstOrDefault(e => e.Id == clientId)?.Name;
        //}
        public IEnumerable<Client> GetStatusList()
        {
            return _context.clients.ToList();
        }

        public Client GetStatus(int id)
        {
            return _context.clients.Find(id);
        }

        public void Create(Client status)
        {
            _context.clients.Add(status);
        }

        public void Delete(int id)
        {
            Client status = _context.clients.Find(id);
            if (status != null)
            {
                _context.clients.Remove(status);
            }
        }

        public void Update(Client status)
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
