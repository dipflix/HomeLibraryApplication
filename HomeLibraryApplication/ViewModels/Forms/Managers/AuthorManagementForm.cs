using HomeLibraryApplication.Enum;
using HomeLibraryApplication.Validators;
using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryApplication.Views.Managements;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;
using System.Linq;

namespace HomeLibraryApplication.ViewModels.Forms.Managers
{
    public class AuthorManagementForm : ManagementFormContextBaseVM<Author>

    {

        public AuthorManagementForm(IRepository<Author> repository) : base("Create author", new AuthorControlContext(), repository)
        {
            FormType = ManagmentType.ADD;
            Entity = new Author();
            Validator = new AuthorEntityValidator(Entity);
        }
        public AuthorManagementForm(IRepository<Author> repository, Author entity) : base("Edit author", new AuthorControlContext(), repository)
        {
            FormType = ManagmentType.UPDATE;
            Entity = new Author()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Books = entity.Books
            };

            Validator = new AuthorEntityValidator(Entity);
        }

        public override void ActionExecute()
        {
            if (!Validator.Validate())
                ValidatorErrorNotifyExecute();
            else
                base.ActionExecute();
        }

    }
}
