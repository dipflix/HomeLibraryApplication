using HomeLibraryApplication.Enum;
using HomeLibraryApplication.Helper;
using HomeLibraryApplication.Validators.Base;
using HomeLibraryData.Models.Base;
using HomeLibraryService.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HomeLibraryApplication.ViewModels.Base
{
    public abstract class ManagementFormContextBaseVM<T> : FormContextBaseVM, INotifyDataErrorInfo where T : Entity

    {
        protected IRepository<T> Repository;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected ValidatorBase<T> Validator { get; set; }

        public T Entity { get; protected set; }

        public bool HasErrors => Validator.ErrorCollection.Any();

        protected void ErrorChangedExecute(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        public override void ActionExecute()
        {

            if (FormType == ManagmentType
                .ADD)
            {
                Repository.Add(Entity);
                EventAfterManagementExecute(Entity, ManagmentType.ADD);
            }
            else if (FormType == ManagmentType.UPDATE)
            {
                Repository.Update(Entity);
                EventAfterManagementExecute(Entity, ManagmentType.UPDATE);
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return Validator.ErrorCollection.GetValueOrDefault(propertyName, null);
        }

        protected void ValidatorErrorNotifyExecute() => ErrorChangedExecute(Validator.ErrorCollection.FirstOrDefault().Key);

        protected ManagementFormContextBaseVM(string title, FrameworkElement viewContext, IRepository<T> repository) : base(title, viewContext)
        {
            Repository = repository;
            ErrorsChanged += (s, e) => UserDialog.ConfirmError(Validator.ErrorCollection[e.PropertyName], e.PropertyName);
        }

    }


}
