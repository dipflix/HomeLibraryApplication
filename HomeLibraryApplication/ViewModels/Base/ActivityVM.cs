using HomeLibraryApplication.Helper;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibraryApplication.ViewModels.Base
{
    public class ActivityVM: ViewModel
    {

        private bool _isSelected = false;
        public int EntityID { get; set; }
        public string Name { get; set; }

        public bool IsSelected { 
            get => _isSelected;
            set =>Set(ref _isSelected, value);
        }

        public ActivityVM(bool isSelected, string name, int id)
        {
            EntityID = id;
            Name = name;
            IsSelected = isSelected;
        }

 
    }
}
