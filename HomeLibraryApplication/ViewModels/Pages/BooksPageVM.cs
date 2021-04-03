using HomeLibraryApplication.Service.Interfaces;
using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryApplication.ViewModels.Forms.Managers;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;
using MathCore.WPF.Commands;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Linq;
using HomeLibraryApplication.Helper;
using System.Windows;
using System.Collections.Generic;

namespace HomeLibraryApplication.ViewModels.Pages
{
    public class BooksPageVM : PageDataControlVM<Book>
    {
        private IRepository<Book> _repository;

        private readonly ObservableCollection<Genre> _genres;
        private readonly ObservableCollection<Author> _authors;

        public ObservableCollection<ActivityVM> GenreCheckBoxFilter { get; }
        public ObservableCollection<ActivityVM> AuthorCheckBoxFilter { get; }

        public CollectionViewSource AuthorFilterView { get; private set; }
        public CollectionViewSource GenreFilterView { get; private set; }

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

            //_genres.CollectionChanged += (s,e) => { MessageBox.Show("qweqwe"); };
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
