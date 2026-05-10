using System;
using System.Globalization;
using System.Windows.Data;

namespace MusicPlayer.Converters;

public class ProgressWidthConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 3 &&
            values[0] is double value &&
            values[1] is double max &&
            values[2] is double actualWidth)
        {
            if (max <= 0) return 0.0;
            return (value / max) * actualWidth;
        }
        return 0.0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}