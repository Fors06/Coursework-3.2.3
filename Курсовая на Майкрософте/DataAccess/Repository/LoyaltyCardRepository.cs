using DataAccess;
using DataAccess.Entity;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;



namespace DataAccess.Repository
{
    public class LoyaltyCardRepository: IRepository<Loyalty_Card>, IDisposable
    {
    
        public readonly ApplicationContext _context;

        public LoyaltyCardRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Loyalty_Card> GetStatusList()
        {
            return _context.loyalty_Cards.ToList();
        }

        public Loyalty_Card GetStatus(int id)
        {
            return _context.loyalty_Cards.Find(id);
        }

        public void Create(Loyalty_Card loyalty_Cards)
        {
            _context.loyalty_Cards.Add(loyalty_Cards);
        }

        public void Delete(int id)
        {
            Loyalty_Card loyalty_Cards = _context.loyalty_Cards.Find(id);
            if (loyalty_Cards != null)
            {
                _context.loyalty_Cards.Remove(loyalty_Cards);
            }
        }

        public void Update(Loyalty_Card loyalty_Cards)
        {
            _context.Entry(loyalty_Cards).State = EntityState.Modified;
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
