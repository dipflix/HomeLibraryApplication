using HomeLibraryApplication.Enum;
using HomeLibraryApplication.Validators;
using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryApplication.Views.Managements;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeLibraryApplication.ViewModels.Forms.Managers
{
    public class GenreManagementForm : ManagementFormContextBaseVM<Genre>
    {
        private GenreManagementForm(IRepository<Genre> repository, string formName) : base(formName, new GenreControlContext(), repository)
        {
            Validator = new GenreEntityValidator(Entity);
        }
        public GenreManagementForm(IRepository<Genre> repository) : this(repository, "Create genre")
        {
            FormType = ManagmentType.ADD;
            Entity = new Genre();
        }

        public GenreManagementForm(IRepository<Genre> repository, in Genre entity) : this(repository, "Edit genre")

        {
            FormType = ManagmentType.UPDATE;
            Entity = new Genre()
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name,
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
