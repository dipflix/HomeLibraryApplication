using System.Windows.Controls;

namespace HomeLibraryApplication.Validators.Base
{
    public abstract class ValidatorBase : ValidationRule
    {
        public bool IsSuccessValidate { get; set; }
    }
}
