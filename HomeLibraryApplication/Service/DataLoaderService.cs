using HomeLibraryApplication.Service.Interfaces;
using HomeLibraryData.Models.Base;
using HomeLibraryService.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;

namespace HomeLibraryApplication.Service
{
    public class DataLoaderService<T> : IDataService<T> where T : Entity
    {
        private IRepository<T> _repository;

        [Inject]
        public DataLoaderService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public ICollection<T> Load()
        {
            throw new NotImplementedException();
        }
    }
}
