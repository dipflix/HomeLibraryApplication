using HomeLibraryApplication.Enum;
using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryApplication.Views.Managements;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;

namespace HomeLibraryApplication.ViewModels.Forms.Managers
{
    public class AuthorManagementForm : ManagementFormContextBaseVM<Author>

    {

        public AuthorManagementForm(IRepository<Author> repository) : base("Create author", new AuthorControlContext(), repository)
        {
            FormType = ManagmentType.ADD;
            Entity = new Author();
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
        }


    }
}
