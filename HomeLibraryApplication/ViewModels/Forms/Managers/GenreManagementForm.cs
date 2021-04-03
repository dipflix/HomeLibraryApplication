using HomeLibraryApplication.Enum;
using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryApplication.Views.Managements;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeLibraryApplication.ViewModels.Forms.Managers
{
    public class GenreManagementForm : ManagementFormContextBaseVM<Genre>
    {
        public GenreManagementForm(IRepository<Genre> repository) : base("Create genre", new GenreControlContext(), repository)
        {
            FormType = ManagmentType.ADD;
            Entity = new Genre();
        }

        public GenreManagementForm(IRepository<Genre> repository, in Genre entity) : base("Edit genre", new GenreControlContext(), repository)
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

    }
}
