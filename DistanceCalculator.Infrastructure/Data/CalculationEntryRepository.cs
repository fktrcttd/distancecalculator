using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DistanceCalculator.Domain.Core;
using DistanceCalculator.Domain.Interfaces;

namespace DistanceCalculator.Infrastructure.Data
{
    public class CalculationEntryRepository : ICalculationEntryRepository, IDisposable
    {
        private DataContext _dataContext;

        private bool _disposed  = false;

        public CalculationEntryRepository()
        {
            _dataContext = new DataContext();
        }
        
        public CalculationEntryRepository([NotNull] DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<CalculationEntry> GetList()
        {
            return _dataContext.CalculationEntries.ToList();
        }

        public CalculationEntry Get(int id)
        {
            return _dataContext.CalculationEntries.FirstOrDefault(ce => ce.Id == id);
        }

        public void Create(CalculationEntry item)
        {
            _dataContext.CalculationEntries.Add(item);
        }

        public void Update(CalculationEntry item)
        {
            var exists = this.Get(item.Id) != null;
            
            if (exists)
                _dataContext.CalculationEntries.Update(item);
            else
                throw new InvalidOperationException("Невозможно обновить несуществующую запись!");
        }

        public void Delete(int id)
        {
            var calculationEntry = this.Get(id); 
            var exists = calculationEntry != null;
            if (exists)
                _dataContext.CalculationEntries.Remove(calculationEntry);
            
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }
        
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                }
            }
            this._disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}