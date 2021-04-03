using HomeLibraryApplication.Service;
using HomeLibraryApplication.Service.Interfaces;
using HomeLibraryApplication.ViewModels.Pages;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;
using HomeLibraryService.Repository;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Ninject;
using System.Collections.Generic;
using System.Windows;

namespace HomeLibraryApplication.ViewModels
{
    public class ApplicationVM : ViewModel
    {
        private IRepository<Book> _bookRepo;
        private IRepository<Genre> _genreRepo;
        private IRepository<Author> _authorRepo;

        private bool _isEnable = true;

        public bool IsEnable
        {
            get => _isEnable;
            set => Set(ref _isEnable, value);
        }

        private Dictionary<string, ViewModel> _pages { get; set; }

        public ViewModel CurrentPage { get; set; }

        public LambdaCommand<string> ChangePageCommand { get; private set; }

        private void ChangePage(string pageName)
        {
            CurrentPage = _pages[pageName];
            OnPropertyChanged("CurrentPage");
        }

        [Inject]
        public ApplicationVM(
            IRepository<Book> repositoryBook,
            IRepository<Genre> repositoryGenre,
            IRepository<Author> repositoryAuthor,
            IDialogFormService dialogFormService
            )
        {
            _bookRepo = repositoryBook;
            _genreRepo = repositoryGenre;
            _authorRepo = repositoryAuthor;

            DialogFormService.OnOpenChanged += (b) => IsEnable = b;
            var genrePage = new GenrePageVM(_genreRepo, dialogFormService);
            var authorPage = new AuthorPageVM(_authorRepo, dialogFormService);
            var bookPage = new BooksPageVM(_bookRepo, dialogFormService, genrePage.EntityData, authorPage.EntityData);
            //genrePage.EntityData.CollectionChanged += (sender, e) => { MessageBox.Show("changed"); };
            _pages = new Dictionary<string, ViewModel>();

            _pages.Add("bookPage", bookPage);
            _pages.Add("genrePage", genrePage);
            _pages.Add("authorPage", authorPage);

            CurrentPage = bookPage;

            ChangePageCommand = new LambdaCommand<string>((pageName) => ChangePage(pageName));
        }
    }
}
