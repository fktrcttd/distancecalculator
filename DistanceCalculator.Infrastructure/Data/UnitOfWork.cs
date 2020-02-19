using System;
using DistanceCalculator.Domain.Interfaces;

namespace DistanceCalculator.Infrastructure.Data
{
    public class UnitOfWork : IDisposable
    {
        private bool disposed = false;
        private DataContext db;
        private CalculationEntryRepository _calculationEntryRepository;

        public UnitOfWork(DataContext db)
        {
            this.db = db;
        }
        
        public CalculationEntryRepository CalculationEntryRepository
        {
            get
            {
                if (_calculationEntryRepository == null)
                    _calculationEntryRepository = new CalculationEntryRepository(db);
                return _calculationEntryRepository;
            }
        }

        public IRepository<T> Repository<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            db.SaveChanges();
        }
        
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //DI will dispose DataContext
                    //db.Dispose();
                }
                this.disposed = true;
            }
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}