namespace RequestDataAccess.Repository.Abstraction
{
    public interface IRepository<T> : IDisposable
          where T : class
    {
        IQueryable<T> GetStatusList(); // получение всех объектов
        T GetStatus(int id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}
