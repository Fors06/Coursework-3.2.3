using DataAccess.Entity;
using DataAccess.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repository
{
    public class CardConditionRepository : IRepository<Card_Condition>, IDisposable
    {
        public readonly ApplicationContext _context;

        public CardConditionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Card_Condition> GetStatusList()
        {
            return _context.card_Conditions.ToList();
        }

        public Card_Condition GetStatus(int id)
        {
            return _context.card_Conditions.Find(id);
        }

        public void Create(Card_Condition card_Conditions)
        {
            _context.card_Conditions.Add(card_Conditions);
        }

        public void Delete(int id)
        {
            Card_Condition card_Conditions = _context.card_Conditions.Find(id);
            if (card_Conditions != null)
            {
                _context.card_Conditions.Remove(card_Conditions);
            }
        }

        public void Update(Card_Condition card_Conditions)
        {
            _context.Entry(card_Conditions).State = EntityState.Modified;
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
