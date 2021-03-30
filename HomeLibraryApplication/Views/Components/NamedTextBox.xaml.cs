using System.Windows;
using System.Windows.Controls;

namespace HomeLibraryApplication.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для NamedTextBox.xaml
    /// </summary>
    public partial class NamedTextBox : UserControl
    {

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string TextBoxContext
        {
            get { return (string)GetValue(TextBoxContextProperty); }
            set { SetValue(TextBoxContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxContextProperty =
            DependencyProperty.Register("TextBoxContext", typeof(string), typeof(NamedTextBox), new PropertyMetadata(""));



        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NamedTextBox), new PropertyMetadata("[NamedTextBox_Title]"));


        public NamedTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
