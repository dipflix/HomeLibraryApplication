using HomeLibraryApplication.Enum;
using HomeLibraryApplication.ViewModels.Forms.Interfaces;
using HomeLibraryData.Models.Base;
using HomeLibraryService.Interfaces;
using MathCore.CommandProcessor;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace HomeLibraryApplication.ViewModels.Base
{
    public abstract class FormContextBaseVM : ViewModel, IFormManagement
    {
        public ManagmentType FormType { get; set; } = ManagmentType.ADD;

        private ICommand _actionManagementCommand;
        public event Action<Entity, ManagmentType> EventAfterManagement;

        public string Title { get; set; }
        public FrameworkElement ViewContext { get; set; }
        public ICommand ActionManagementCommand => _actionManagementCommand ??= new LambdaCommand(ActionExecute);

        public void EventAfterManagementExecute(Entity entity, ManagmentType managmentType) => EventAfterManagement?.Invoke(entity, managmentType);

        public virtual void ActionExecute()
        {
            throw new Exception("ActionExecute not init!");
        }


        public FormContextBaseVM(string title, FrameworkElement viewContext)
        {
            Title = title;
            ViewContext = viewContext;

        }


    }
}
