using HomeLibraryApplication.Validators.Base;
using HomeLibraryData.Models;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace HomeLibraryApplication.Validators
{
    public class GenreEntityValidator : ValidatorBase
    {
        public Type ValidationType { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Genre genre = (Genre)value;
            bool isSuccessValidate = false;






            return new ValidationResult(true, null);
        }
    }
}
