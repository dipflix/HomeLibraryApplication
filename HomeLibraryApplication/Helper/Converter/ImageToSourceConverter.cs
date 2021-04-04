﻿using HomeLibraryApplication.Data;
using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace HomeLibraryApplication.Helper.Converter
{
    public class ImageToSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value)) throw new ArgumentNullException();

            return new BitmapImage(new Uri(Directory.GetCurrentDirectory() + AppData.PathResourceImages + (string)value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
