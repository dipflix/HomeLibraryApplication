using HomeLibraryData.Models.Base;
using MathCore.WPF.ViewModels;
using System.Collections.Generic;
using System.Windows.Controls;

namespace HomeLibraryApplication.Validators.Base
{
    public abstract class ValidatorBase<T> : ViewModel
    {
        public T Entity { get; set; }

        protected ValidatorBase(T entity)
        {
            Entity = entity;
        }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public virtual bool Validate() => ErrorCollection.Count == 0;
    }
}
