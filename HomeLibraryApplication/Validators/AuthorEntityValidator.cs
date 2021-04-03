using HomeLibraryApplication.Validators.Base;
using HomeLibraryData.Models;

namespace HomeLibraryApplication.Validators
{
    public class AuthorEntityValidator : ValidatorBase<Author>
    {
        public AuthorEntityValidator(Author entity) : base(entity)
        {
        }
        public override bool Validate()
        {
            ErrorCollection.Clear();

            if (string.IsNullOrWhiteSpace(Entity.FirstName))
                ErrorCollection.Add(nameof(Entity.FirstName), "Must not be empty.");

            if (string.IsNullOrWhiteSpace(Entity.LastName))
                ErrorCollection.Add(nameof(Entity.LastName), "Must not be empty.");

            return base.Validate();
        }
    }
}
