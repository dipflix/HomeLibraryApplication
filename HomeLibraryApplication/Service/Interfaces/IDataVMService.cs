using MathCore.WPF.ViewModels;
using System.Threading.Tasks;

namespace HomeLibraryApplication.Service.Interfaces
{
    public interface IDataVMService<T> where T : ViewModel
    {
        Task LoadData();
        T GetVM();
    }
}
