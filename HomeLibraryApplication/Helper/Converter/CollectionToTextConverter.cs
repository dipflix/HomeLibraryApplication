using HomeLibraryData.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HomeLibraryApplication.Helper.Converter
{
    public class CollectionToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "Nothing";
            if (value is ICollection<Genre>)
            {
                List<Genre> list = (List<Genre>)value;
                result = "";
                list.ForEach(item => result += $"{item.Name}, ");
            } else if (value is ICollection<Author>) {
                List<Author> list = (List<Author>)value;
                result = "";
                list.ForEach(item => result += $"{item.FirstName} {item.LastName}, ");
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
