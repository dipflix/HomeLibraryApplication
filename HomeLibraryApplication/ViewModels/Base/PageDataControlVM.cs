using HomeLibraryApplication.Enum;
using HomeLibraryApplication.Service.Interfaces;
using HomeLibraryData.Models.Base;
using HomeLibraryService.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace HomeLibraryApplication.ViewModels.Base
{
    public abstract class PageDataControlVM<T> : ViewModel where T : Entity
    {
        private IRepository<T> _repository;
        private IDialogFormService _dialogFormService;

        public ObservableCollection<T> EntityData { get; private set; }
        public virtual CollectionViewSource RenderList { get; set; }

        private T _selectedEntity = new object() as T;
        public T SelectedEntity
        {
            get => _selectedEntity;
            set
            {
                Set(ref _selectedEntity, value);
                OnPropertyChanged("IsSelected");
            }
        }

        public bool IsSelected
        {
            get => SelectedEntity != null;
        }
        public ICommand OpenCreateManagerCommand { get; set; }

        public ICommand OpenEditManagerCommand { get; set; }

        public ICommand DeleteSelectedEntity { get; set; }

        public PageDataControlVM(IRepository<T> repository, IDialogFormService dialogFormService)
        {
            _repository = repository;
            _dialogFormService = dialogFormService;

            EntityData = new ObservableCollection<T>();
            RenderList = new CollectionViewSource();

            RenderList.Source = EntityData;

            DeleteSelectedEntity = new LambdaCommand(DeleteSelectedEntityExecute);


        }


        public void OpenForm(FormContextBaseVM form)
        {
            form.EventAfterManagement += (entity, managmentType) => ActionAfterManagment((T)entity, managmentType);
            _dialogFormService.Show(form);
        }
        public virtual async Task LoadData()
        {
            EntityData.AddItems(await _repository.Items.ToArrayAsync());
        }

        protected virtual void ActionAfterManagment(T entity, ManagmentType managmentType)
        {
            switch (managmentType)
            {
                case ManagmentType.ADD:
                    EntityData.Add(entity);
                    RenderList.View.Refresh();
                    break;
                case ManagmentType.UPDATE:

                    EntityData[
                        EntityData.IndexOf(
                            EntityData.SingleOrDefault(ent => ent.Id.Equals(entity.Id))
                            )
                        ] = entity;

                    RenderList.View.Refresh();
                    break;
                case ManagmentType.REMOVE:
                    EntityData.Remove(EntityData.SingleOrDefault(ent => ent.Id.Equals(entity.Id)));
                    RenderList.View.Refresh();
                    break;
            }

        }
        protected virtual void DeleteSelectedEntityExecute()
        {
            _repository.Remove(SelectedEntity.Id);
            ActionAfterManagment(SelectedEntity, ManagmentType.REMOVE);
        }
    }
}
