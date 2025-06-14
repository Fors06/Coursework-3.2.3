using DataAccess.Entity;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class CardStatusRepository : IRepository<Card_status>, IDisposable
    {
        public readonly ApplicationContext _context;

        public CardStatusRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Card_status> GetStatusList()
        {
            return _context.card_Statuses.ToList();
        }

        public Card_status GetStatus(int id)
        {
            return _context.card_Statuses.Find(id);
        }

        public void Create(Card_status card_Statuses)
        {
            _context.card_Statuses.Add(card_Statuses);
        }

        public void Delete(int id)
        {
            Card_status card_Statuses = _context.card_Statuses.Find(id);
            if (card_Statuses != null)
            {
                _context.card_Statuses.Remove(card_Statuses);
            }
        }

        public void Update(Card_status card_Statuses)
        {
            _context.Entry(card_Statuses).State = EntityState.Modified;
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
