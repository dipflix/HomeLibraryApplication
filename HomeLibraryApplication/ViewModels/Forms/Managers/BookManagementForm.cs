using HomeLibraryApplication.Enum;
using HomeLibraryApplication.Helper;
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

            //GenreCheckBoxFilter.Foreach(item =>
            //{

            //    var t = Entity.Genres.SingleOrDefault(it => it.Id == item.EntityID);
            //    if (t != null && item.IsSelected == false)
            //    {

            //        Entity.Genres.Remove(t);

            //        t = null;
            //    }
            //    if (t == null && item.IsSelected == true)
            //    {
            //        var selected = Genres.Single(it => it.Id == item.EntityID);
            //        if (selected != null)
            //        {
            //            Entity.Genres.Add(selected);

            //        }

            //        t = null;
            //        selected = null;
            //    }
            //});

            //foreach (var item in GenreCheckBoxFilter) {
            //    var genre = Genres.Single(it => it.Id == item.EntityID);
            //    var isContain = Entity.Genres.SingleOrDefault(it => it.Id == genre.Id).IsNotNull();



            //    if (!isContain && item.IsSelected == true) {
            //        Entity.Genres.Add(genre);
            //    }

            //    if (isContain && item.IsSelected == false) {
            //        var entGenre = Entity.Genres.SingleOrDefault(it => it.Id == item.EntityID);
            //        Entity.Genres.Remove(entGenre);
            //    }

            //  MessageBox.Show(isContain.ToString());
            //}

   
            //AuthorCheckBoxFilter.Foreach(item => {

            //    var t = Entity.Authors.SingleOrDefault(it => it.Id == item.EntityID);
            //    if (t == null && item.IsSelected == true)
            //    {
            //        var selected = Authors.Single(it => it.Id == item.EntityID);
            //        if (selected != null)
            //            Entity.Authors.Add(selected);
            //         t = null;
            //    }

            //    if (t != null && item.IsSelected == false)
            //    {
            //        Entity.Authors.Remove(t);
            //        t = null;
            //    }

            //});

            Entity.PicturePath = "/";
            base.ActionExecute();
        }
    }
}
