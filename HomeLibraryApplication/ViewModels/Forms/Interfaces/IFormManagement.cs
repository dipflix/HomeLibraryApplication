using System.Windows.Input;

namespace HomeLibraryApplication.ViewModels.Forms.Interfaces
{
    public interface IFormManagement
    {
        ICommand ActionManagementCommand { get; }
        void ActionExecute();
    }
}
