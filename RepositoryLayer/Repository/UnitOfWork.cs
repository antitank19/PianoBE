using DataLayer.DbContext;
using RepositoryLayer.IRepository;

namespace RepositoryLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly PianoContext _dbContext;

        private readonly IInstrumentRepository _instrumentRepository;
        private readonly INoteRepository _noteRepository;
        private readonly ISheetRepository _sheetRepository;
        private readonly ISongRepository _songRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayTrackingRepository _playTrackingRepository;
        public UnitOfWork(PianoContext dbContext,
            IInstrumentRepository instrumentRepository,
            INoteRepository noteRepository,
            ISheetRepository sheetRepository,
            ISongRepository songRepository,
            IGenreRepository genreRepository,
            IUserRepository userRepository,
            IPlayTrackingRepository playTrackingRepository)
        {
            _dbContext = dbContext;
            _instrumentRepository = instrumentRepository;
            _noteRepository = noteRepository;
            _sheetRepository = sheetRepository;
            _songRepository = songRepository;
            _genreRepository = genreRepository;
            _userRepository = userRepository;
            _playTrackingRepository = playTrackingRepository;
        }
        
        public IInstrumentRepository InstrumentRepository => _instrumentRepository;
        public INoteRepository NoteRepository => _noteRepository;
        public ISheetRepository SheetRepository => _sheetRepository;
        public ISongRepository SongRepository => _songRepository;
        public IGenreRepository GenreRepository => _genreRepository;
        public IUserRepository UserRepository => _userRepository;
        public IPlayTrackingRepository PlayTrackingRepository => _playTrackingRepository;


        /*public IInstrumentRepository InstrumentRepository
        {
            
            get { return _instrumentRepository ??= new InstrumentRepository(_dbContext); }
        }
        public INoteRepository NoteRepository
        {
            get { return _noteRepository ??= new NoteRepository(_dbContext); }
        }
        public ISheetRepository SheetRepository
        {
            get { return _sheetRepository ??= new SheetRepository(_dbContext); }
        }
        public ISongRepository SongRepository
        {
            get { return _songRepository ??= new SongRepository(_dbContext); }

        }*/



        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }
    }
}
