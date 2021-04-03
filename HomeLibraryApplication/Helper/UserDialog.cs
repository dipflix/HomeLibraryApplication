using System.Windows;

namespace HomeLibraryApplication.Helper
{
    public static class UserDialog
    {
        public static bool ConfirmError(string Error, string Caption) => MessageBox
           .Show(
                Error, Caption,
                MessageBoxButton.OK,
                MessageBoxImage.Error)
                == MessageBoxResult.Yes;


        public static bool ConfirmInformation(string Information, string Caption) => MessageBox
           .Show(
                Information, Caption,
                MessageBoxButton.OK,
                MessageBoxImage.Information)
                == MessageBoxResult.Yes;

        public static bool ConfirmWarning(string Warning, string Caption) => MessageBox
           .Show(
                Warning, Caption,
                MessageBoxButton.OK,
                MessageBoxImage.Warning)
                == MessageBoxResult.Yes;
    }
}
