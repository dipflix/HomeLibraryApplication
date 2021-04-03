using MathCore.WPF.ViewModels;
using System.Windows;

namespace HomeLibraryApplication.Views.Forms
{
    public class UserDialogVM: ViewModel
    {
        public string Title { get; set; }
        public FrameworkElement BaseDataContext { get; set; }

    }
}
