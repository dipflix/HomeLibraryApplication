using HomeLibraryApplication.Service.Interfaces;
using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryApplication.ViewModels.Forms.Managers;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;
using MathCore.WPF.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Linq;
using HomeLibraryApplication.Helper;
using System.Windows.Input;
using System.Collections.Generic;

namespace HomeLibraryApplication.ViewModels.Pages
{
    public class BooksPageVM : PageDataControlVM<Book>
    {
        private IRepository<Book> _repository;

        private ICommand _filterCommand;

        private readonly ObservableCollection<Genre> _genres;
        private readonly ObservableCollection<Author> _authors;
        private string _bookTitleFitler;
        public ObservableCollection<ActivityVM> GenreCheckBoxFilter { get; }
        public ObservableCollection<ActivityVM> AuthorCheckBoxFilter { get; }

        public string BookTitleFilter
        {
            get => _bookTitleFitler;
            set => Set(ref _bookTitleFitler, value);
        }

        private ObservableCollection<Book> _bookRenderList;

        protected ObservableCollection<Book> BookRenderList
        {
            get => _bookRenderList;
            set
            {
                Set(ref _bookRenderList, value);
            }
        }
        public CollectionViewSource AuthorFilterView { get; private set; }
        public CollectionViewSource GenreFilterView { get; private set; }

        public ICommand FilterCommand => _filterCommand ??= new LambdaCommand(FilterExecute);
        public ICommand FilterClearCommand => new LambdaCommand(FilterResetFull);

        private void FilterExecute()
        {
            ObservableCollection<Book> filter = EntityData;

            if (
                !GenreCheckBoxFilter.Any(it => it.IsSelected)
                && !AuthorCheckBoxFilter.Any(it => it.IsSelected)
                && BookTitleFilter.IsNullOrWhiteSpace()
                )
            {
                FilterReset();
                return;
            }


            if (GenreCheckBoxFilter.Any(it => it.IsSelected))
                filter = filter.Where(
                    book =>
                    GenreCheckBoxFilter
                    .Where(checkbox => checkbox.IsSelected)
                    .All(checkbox => book.Genres.Any(genre => genre.Id == checkbox.EntityID))
                    ).ToObservableCollection();

            if (AuthorCheckBoxFilter.Any(it => it.IsSelected))
                filter = filter.Where(
                    book =>
                    AuthorCheckBoxFilter
                    .Where(checkbox => checkbox.IsSelected)
                    .All(checkbox => book.Authors.Any(author => author.Id == checkbox.EntityID))
                    ).ToObservableCollection();


            if (BookTitleFilter.IsNotNullOrWhiteSpace())
                filter = filter.Where(it => it.Title.ToLower().Contains(BookTitleFilter.ToLower())).ToObservableCollection();

            RenderList.Source = filter;
            RenderList.View.Refresh();
        }

        private void FilterReset() => RenderList.Source = EntityData;
        private void FilterResetFull()
        {
            FilterReset();
            BookTitleFilter = "";
            GenreCheckBoxFilter.Foreach(it => it.IsSelected = false);
            AuthorCheckBoxFilter.Foreach(it => it.IsSelected = false);

        }
        public BooksPageVM(
            IRepository<Book> repository,
            IDialogFormService dialogFormService,
            ObservableCollection<Genre> genres,
            ObservableCollection<Author> authors

            ) : base(repository, dialogFormService)
        {

            _repository = repository;
            _genres = genres;
            _authors = authors;

            AuthorFilterView = new CollectionViewSource();

            GenreFilterView = new CollectionViewSource();

            GenreCheckBoxFilter = new ObservableCollection<ActivityVM>();
            AuthorCheckBoxFilter = new ObservableCollection<ActivityVM>();

            BookRenderList = EntityData;
            RenderList.Source = BookRenderList;

            _genres.CollectionChanged += GenreCheckBoxFilter.MagagementCheckBoxEntity;
            _authors.CollectionChanged += AuthorCheckBoxFilter.MagagementCheckBoxEntity;


            OpenCreateManagerCommand = new LambdaCommand(()
                => OpenForm(
                new BookManagementForm(_repository, _genres, _authors, GenreCheckBoxFilter, AuthorCheckBoxFilter)));

            OpenEditManagerCommand = new LambdaCommand(
                () => OpenForm(
                    new BookManagementForm(_repository, SelectedEntity, _genres, _authors, GenreCheckBoxFilter, AuthorCheckBoxFilter)));

            _ = LoadData();

        }


    }
}
