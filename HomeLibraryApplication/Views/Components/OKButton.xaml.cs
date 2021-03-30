using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HomeLibraryApplication.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для OKButton.xaml
    /// </summary>
    public partial class OKButton : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
          DependencyProperty.Register("Title", typeof(string), typeof(OKButton), new PropertyMetadata("Ok"));

        public static readonly DependencyProperty OKButtonCommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(OKButton), new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(OKButtonCommandProperty); }
            set
            {
                if (value.IsNotNull())
                    SetValue(OKButtonCommandProperty, value);
            }
        }
        public OKButton()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
