using HomeLibraryApplication.ViewModels.Base;
using HomeLibraryData.Models.Base;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace HomeLibraryApplication.Helper
{
    public static class ObservableCollectionHelper
    {
        public static void MagagementCheckBoxEntity(this ObservableCollection<ActivityVM> collection, object sender, NotifyCollectionChangedEventArgs eventHandler)
        {
            switch (eventHandler.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    IEntity entity = (IEntity)eventHandler.NewItems[0];
                    collection.Add(new ActivityVM(false, entity.ToLiteText(), entity.Id));
                    break;
                case NotifyCollectionChangedAction.Replace:
                    IEntity entityOld = (IEntity)eventHandler.OldItems[0];
                    IEntity entityNew = (IEntity)eventHandler.NewItems[0];
                    collection.Single(it => it.EntityID == entityOld.Id).Name = entityNew.ToLiteText();

                    break;
                case NotifyCollectionChangedAction.Remove:
                    IEntity entityRemove = (IEntity)eventHandler.OldItems[0];
                    collection.Remove(collection.SingleOrDefault(it => it.EntityID == entityRemove.Id));
                    break;
            }
        }

        public static ObservableCollection<ActivityVM> CloneActivityVM(this ObservableCollection<ActivityVM> collection)
        {
            ObservableCollection<ActivityVM> res = new ObservableCollection<ActivityVM>();
            collection.Foreach(item => res.Add(new ActivityVM(false, item.Name, item.EntityID)));
            return res;
        }
    }
}
