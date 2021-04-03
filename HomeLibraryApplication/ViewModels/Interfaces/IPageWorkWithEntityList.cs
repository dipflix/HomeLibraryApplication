using System.Collections.ObjectModel;

namespace HomeLibraryApplication.ViewModels.Interfaces
{
    public interface IPageWorkWithEntityList<T>
    {
        ObservableCollection<T> RenderList { get; set; }

    }
}
