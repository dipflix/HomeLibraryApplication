using HomeLibraryApplication.Enum;
using HomeLibraryData.Models.Base;
using HomeLibraryService.Interfaces;
using System.Windows;

namespace HomeLibraryApplication.ViewModels.Base
{
    public abstract class ManagementFormContextBaseVM<T> : FormContextBaseVM where T : Entity
    {
        protected IRepository<T> Repository;
        public T Entity { get; protected set; }
        protected ManagementFormContextBaseVM(string title, FrameworkElement viewContext, IRepository<T> repository) : base(title, viewContext)
        {
            Repository = repository;
        }

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
    }


}
