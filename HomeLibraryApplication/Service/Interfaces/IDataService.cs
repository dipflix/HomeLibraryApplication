using System.Collections.Generic;

namespace HomeLibraryApplication.Service.Interfaces
{
    public interface IDataService<T> where T : class
    {
        ICollection<T> Load();
    }
}
