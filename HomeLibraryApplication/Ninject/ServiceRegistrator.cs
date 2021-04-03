using HomeLibraryApplication.Service;
using HomeLibraryApplication.Service.Interfaces;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;
using HomeLibraryService.Repository;
using Ninject.Modules;

namespace HomeLibraryApplication.Ninject
{
    public class ServiceRegistrator : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Book>>().To<BookRepository>();
            Bind<IRepository<Genre>>().To<GenreRepository>();
            Bind<IRepository<Author>>().To<AuthorRepository>();
            Bind<IDialogFormService>().To<DialogFormService>();
        }
    }
}
