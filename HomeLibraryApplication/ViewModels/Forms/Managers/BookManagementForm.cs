﻿using HomeLibraryApplication.Data;
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
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

        public Image ImageLoaded { get; set; }
        private FileInfo _imageFile;

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
            Entity.Genres = new ObservableCollection<Genre>();
            Entity.Authors = new ObservableCollection<Author>();
            Validator = new BookEntityValidator(Entity);
            ImageLoaded = new Image();
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
                PictureName = entity.PictureName,
                PublisingDate = entity.PublisingDate
            };

            Validator = new BookEntityValidator(Entity);
            ImageLoaded = new Image();

            if (Entity.PictureName.IsNotNullOrEmpty())
            {

                string uriText = Directory.GetCurrentDirectory() + AppData.PathResourceImages + Entity.PictureName;
                ImageLoaded.Source = new BitmapImage(new Uri(uriText));
                OnPropertyChanged("ImageLoaded");

            }

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

            if (Directory.Exists(AppData.PathResourceImages))
            {
                ImageLoaded = new Image();
                OpenFileDialog openFileDialog_LoadImage = new OpenFileDialog();
                openFileDialog_LoadImage.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                var openedFile = openFileDialog_LoadImage.ShowDialog();
                if (openedFile == true)
                {
                    var file = new FileInfo(openFileDialog_LoadImage.FileName);
                    if (file.Exists)
                    {
                        _imageFile = file;
                        ImageLoaded.Source = new BitmapImage(new Uri(file.FullName));
                        OnPropertyChanged("ImageLoaded");

                    }
                }

            }
            else
            {
                Directory.CreateDirectory(AppData.PathResourceImages);
                LoadImageExecute();
            }

        }

        public override void ActionExecute()
        {


            var addedItemGenre =
                Genres.Where(
                    genre =>
                    GenreCheckBoxFilter
                    .Where(checkbox => checkbox.IsSelected)
                    .Any(checkbox => checkbox.EntityID == genre.Id)
                    ).ToObservableCollection();

            addedItemGenre = addedItemGenre.Where(genre => !Entity.Genres.Contains(genre)).ToObservableCollection();

            if(addedItemGenre.Count > 0)
                Entity.Genres.AddItems(addedItemGenre);


            var addedItemAuthor =
                Authors.Where(
                    author =>
                    AuthorCheckBoxFilter
                    .Where(checkbox => checkbox.IsSelected)
                    .Any(checkbox => checkbox.EntityID == author.Id)
                    ).ToObservableCollection();

            addedItemAuthor = addedItemAuthor.Where(author => !Entity.Authors.Contains(author)).ToObservableCollection();

            if (addedItemAuthor.Count > 0)
                Entity.Authors.AddItems(addedItemAuthor);

            var removedItemGenre =
              Entity.Genres.Where(
                  genre =>
                      GenreCheckBoxFilter
                      .Where(checkbox => !checkbox.IsSelected)
                      .Any(checkbox => checkbox.EntityID == genre.Id)
                  ).ToObservableCollection();


            if (removedItemGenre.Count > 0)
                Entity.Genres.RemoveItems(removedItemGenre);

            var removedItemAuthor =
             Entity.Authors.Where(
                 author =>
                     AuthorCheckBoxFilter
                     .Where(checkbox => !checkbox.IsSelected)
                     .Any(checkbox => checkbox.EntityID == author.Id)
                 ).ToObservableCollection();

            if (removedItemAuthor.Count > 0)
                Entity.Authors.RemoveItems(removedItemAuthor);


            if (_imageFile != null)
            {
                var hash = new ImageHash().GetHash(_imageFile.FullName);
                var hashImage = hash + ".jpg";
                var fpname = AppData.PathResourceImages + hashImage;

                if (!File.Exists(fpname))
                    _imageFile.CopyTo(fpname);

                Entity.PictureName = hashImage;
            }

            base.ActionExecute();


        }
    }
}
