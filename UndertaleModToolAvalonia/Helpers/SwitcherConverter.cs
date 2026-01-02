using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace UndertaleModToolAvalonia;

public class SwitcherConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int v1 && parameter is string v2)
            return v1.ToString()==v2;
        return BindingOperations.DoNothing;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(parameter is string s && value is true)
            return int.Parse(s);
        return BindingOperations.DoNothing;
    }
}
