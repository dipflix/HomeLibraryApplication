using HomeLibraryApplication.Service.Interfaces;
using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryApplication.ViewModels.Forms.Managers;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;
using MathCore.WPF.Commands;
using System;
using System.Threading.Tasks;

namespace HomeLibraryApplication.ViewModels.Pages
{
    public class AuthorPageVM : PageDataControlVM<Author>
    {
        private IRepository<Author> _repository;

        public AuthorPageVM(IRepository<Author> repository, IDialogFormService dialogFormService) : base(repository, dialogFormService)
        {
            _repository = repository;

            OpenCreateManagerCommand = new LambdaCommand(() => OpenForm(new AuthorManagementForm(_repository)));
            OpenEditManagerCommand = new LambdaCommand(() => OpenForm(new AuthorManagementForm(_repository, SelectedEntity)));

            _ = LoadData();
        }


    }
}
