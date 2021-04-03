using HomeLibraryApplication.Validators.Base;
using HomeLibraryData.Models;

namespace HomeLibraryApplication.Validators
{
    public class BookEntityValidator : ValidatorBase<Book>
    {
        public BookEntityValidator(Book entity) : base(entity)
        {
        }

        public override bool Validate()
        {
            ErrorCollection.Clear();

            if(string.IsNullOrWhiteSpace(Entity.Title))
                ErrorCollection.Add(nameof(Entity.Title), "Must not be empty.");

            if (string.IsNullOrWhiteSpace(Entity.Description))
                ErrorCollection.Add(nameof(Entity.Description), "Must not be empty.");

            if(Entity.Genres.Count < 1)
                ErrorCollection.Add(nameof(Entity.Genres), "Genres count must not be less than 1, select more please!");

            if (Entity.Authors.Count < 1)
                ErrorCollection.Add(nameof(Entity.Authors), "Authors count must not be less than 1, select more please!");

            if(string.IsNullOrEmpty(Entity.PicturePath))
                ErrorCollection.Add(nameof(Entity.PicturePath), "Book image must not be enmpty. Please load image!");

            return base.Validate();
        }
    }
}
