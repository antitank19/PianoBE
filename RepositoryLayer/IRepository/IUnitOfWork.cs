namespace RepositoryLayer.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IInstrumentRepository InstrumentRepository { get; }
        ISongRepository SongRepository { get; }
        ISheetRepository SheetRepository { get; }
        INoteRepository NoteRepository { get; }
        IGenreRepository GenreRepository { get; }
        IUserRepository UserRepository { get; }
        IPlayTrackingRepository PlayTrackingRepository { get; }
        
        
        
        
        IGenericRepository<T> GetRepository<T>() where T : class;

        void Save();
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollBack();

    }
}
