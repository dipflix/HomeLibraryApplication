using HomeLibraryApplication.Validators.Base;
using HomeLibraryData.Models;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace HomeLibraryApplication.Validators
{
    public class GenreEntityValidator : ValidatorBase<Genre>
    {
        public GenreEntityValidator(Genre entity) : base(entity)
        {
        }

        public override bool Validate()
        {
            ErrorCollection.Clear();

            if (string.IsNullOrWhiteSpace(Entity.Name))
                ErrorCollection.Add(nameof(Entity.Name), "Must not be empty.");

            if (string.IsNullOrWhiteSpace(Entity.Description))
                ErrorCollection.Add(nameof(Entity.Description), "Must not be empty.");

          return base.Validate();
        }
    }
}
