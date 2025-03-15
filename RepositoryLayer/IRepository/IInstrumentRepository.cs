using DataLayer.DbObject;

namespace RepositoryLayer.IRepository
{
    public interface IInstrumentRepository : IGenericRepository<Instrument>
    {
        IQueryable<T> GetList<T>();
        Task<bool> IsExistAsync(int id);
    }
}
