using HomeLibraryApplication.Enum;
using HomeLibraryApplication.Helper;
using HomeLibraryApplication.Validators;
using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryApplication.Views.Managements;
using HomeLibraryData.Models;
using HomeLibraryService.Interfaces;
using MathCore.WPF.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeLibraryApplication.ViewModels.Forms.Managers
{
    public class BookManagementForm : ManagementFormContextBaseVM<Book>
    {
        private ICommand _loadImageCommand;
        public ICommand LoadImageCommand => _loadImageCommand ??= new LambdaCommand(LoadImageExecute);

        public ObservableCollection<Genre> Genres { get; set; }
        public ObservableCollection<Author> Authors { get; set; }
        public ObservableCollection<ActivityVM> GenreCheckBoxFilter { get; set; }
        public ObservableCollection<ActivityVM> AuthorCheckBoxFilter { get; set; }

        public BookManagementForm(
            IRepository<Book> repository,
            ObservableCollection<Genre> genres,
            ObservableCollection<Author> authors,
            ObservableCollection<ActivityVM> genreCheckBoxFilter,
            ObservableCollection<ActivityVM> authorCheckBoxFilter

            ) : base("Create book", new BookControlContext(), repository)
        {
            Genres = genres;
            Authors = authors;
            GenreCheckBoxFilter = genreCheckBoxFilter.CloneActivityVM();
            AuthorCheckBoxFilter = authorCheckBoxFilter.CloneActivityVM();

            FormType = ManagmentType.ADD;
            Entity = new Book() { Damaged = 10 };

            Validator = new BookEntityValidator(Entity);
        }

        public BookManagementForm(
            IRepository<Book> repository,
            Book entity,
            ObservableCollection<Genre> genres,
            ObservableCollection<Author> authors,
            ObservableCollection<ActivityVM> genreCheckBoxFilter,
            ObservableCollection<ActivityVM> authorCheckBoxFilter
            ) : base("Edit book", new BookControlContext(), repository)
        {
            Genres = genres;
            Authors = authors;
            GenreCheckBoxFilter = genreCheckBoxFilter.CloneActivityVM();
            AuthorCheckBoxFilter = authorCheckBoxFilter.CloneActivityVM();

            FormType = ManagmentType.UPDATE;
            Entity = new Book()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Damaged = entity.Damaged,
                Authors = entity.Authors,
                Genres = entity.Genres,
                PicturePath = entity.PicturePath,
                PublisingDate = entity.PublisingDate
            };

            Validator = new BookEntityValidator(Entity);

            Entity.Genres.Foreach(genre =>
            {
                GenreCheckBoxFilter.SingleOrDefault(it => it.EntityID == genre.Id).IsSelected = true;
            });

            Entity.Authors.Foreach(author =>
            {
                AuthorCheckBoxFilter.SingleOrDefault(it => it.EntityID == author.Id).IsSelected = true;
            });

        }


        private void LoadImageExecute()
        {
            OpenFileDialog openFileDialog_LoadImage = new OpenFileDialog();
            openFileDialog_LoadImage.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            var file = openFileDialog_LoadImage.ShowDialog();

        }

        public override void ActionExecute()
        {

            if (!Validator.Validate())
                ValidatorErrorNotifyExecute();
            else
            {

                //Entity.Genres = Genres.Where(
                //        it =>
                //        GenreCheckBoxFilter
                //        .Where(it => it.IsSelected)
                //        .Any(c => c.EntityID == it.Id)
                //        ).ToList();

                var test = Genres.Where(
                        it =>
                        GenreCheckBoxFilter
                        .Where(it => it.IsSelected)
                        .Any(c => c.EntityID == it.Id)
                        ).ToList();
                test.ForEach(it => Entity.Genres.Add(new Genre() { Id = it.Id }));
                //Entity.Genres.AddItems(Genres.Where(
                //        it =>
                //        GenreCheckBoxFilter
                //        .Where(it => it.IsSelected)
                //        .Any(c => c.EntityID == it.Id)
                //        ).ToList());

                Entity.PicturePath = "/";
                base.ActionExecute();
            }
           
        }
    }
}
