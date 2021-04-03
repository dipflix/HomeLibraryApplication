using MathCore.WPF.ViewModels;

namespace HomeLibraryApplication.Helper
{
    public class Activity : ViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Activity(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
