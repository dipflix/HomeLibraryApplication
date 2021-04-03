using HomeLibraryApplication.Service.Interfaces;
using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryApplication.ViewModels.Forms.Managers;
using HomeLibraryApplication.Views.Managements;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;
using MathCore.WPF.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeLibraryApplication.ViewModels.Pages
{
    public class GenrePageVM : PageDataControlVM<Genre>
    {
        private IRepository<Genre> _repository;

        public GenrePageVM(IRepository<Genre> repository, IDialogFormService dialogFormService): base(repository, dialogFormService)
        {
            _repository = repository;

            OpenCreateManagerCommand = new LambdaCommand(() => OpenForm(new GenreManagementForm(_repository)));
            OpenEditManagerCommand = new LambdaCommand(() => OpenForm(new GenreManagementForm(_repository, SelectedEntity)));

            _ = LoadData();
        }



    }
}
