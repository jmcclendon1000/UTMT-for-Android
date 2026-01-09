using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UndertaleModToolAvalonia;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        DetachedFromVisualTree += (sender, args) =>
        {
            if (DataContext is SettingsViewModel vm)
            {
                vm.MainVM.Settings?.Save();
            }
        };
    }
    
}