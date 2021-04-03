using HomeLibraryApplication.Service.Interfaces;
using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryApplication.Views.Forms;
using System;
using System.ComponentModel;
using System.Windows;

namespace HomeLibraryApplication.Service
{
    public class DialogFormService : IDialogFormService
    {
        public static event Action<bool> OnOpenChanged;
        private bool _isFormClosed = true;
        public string Test { get; set; } = " werinjbkwh";
        public FrameworkElement CurrentDataContext { get; set; }
        public bool IsSomeSupportFormClose
        {
            get => _isFormClosed;
            set
            {
                _isFormClosed = value;
                OnOpenChanged?.Invoke(_isFormClosed);
            }
        }

        public void Show(FormContextBaseVM context)
        {
            if (!IsSomeSupportFormClose) return;
            var form = new FormManagementWindow();
            IsSomeSupportFormClose = false;

            context.EventAfterManagement += (e, m) => form.Close();

            form.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            form.DataContext = context;
            form.Closing += (object sender, CancelEventArgs e) => IsSomeSupportFormClose = true;
            form.Show();
        }
    }
}
