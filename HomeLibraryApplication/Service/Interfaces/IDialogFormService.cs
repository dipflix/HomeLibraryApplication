using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryData.Models.Base;
using System;

namespace HomeLibraryApplication.Service.Interfaces
{
    public interface IDialogFormService
    {
        void Show(FormContextBaseVM context);
    }
}
