using HomeLibraryApplication.Service.Interfaces;
using HomeLibraryData.Models;
using MathCore.WPF.ViewModels;

namespace HomeLibraryApplication.ViewModels
{
    public class DataVM : ViewModel
    {
        private IDataService<Book> _dataBookService;
        private IDataService<Genre> _dataGenreService;
        private IDataService<Author> _dataAuthorService;


    }
}
