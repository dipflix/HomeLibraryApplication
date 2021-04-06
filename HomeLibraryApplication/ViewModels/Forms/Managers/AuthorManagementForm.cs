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

        private AuthorManagementForm(IRepository<Author> repository, string formName) : base(formName, new AuthorControlContext(), repository)
        {
            Validator = new AuthorEntityValidator(Entity);
        }

        public AuthorManagementForm(IRepository<Author> repository) : this(repository, "Create author")
        {
            FormType = ManagmentType.ADD;
            Entity = new Author();
            
        }
        public AuthorManagementForm(IRepository<Author> repository, Author entity) :this(repository, "Edit author")
        {
            FormType = ManagmentType.UPDATE;
            Entity = new Author()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Books = entity.Books
            };

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
