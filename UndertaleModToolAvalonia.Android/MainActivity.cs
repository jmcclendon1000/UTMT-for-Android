using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;

namespace UndertaleModToolAvalonia.Android
{
    [Activity(
        Label = "UndertaleModToolAvalonia.Android",
        Theme = "@style/MyTheme.NoActionBar",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.UiMode)]
    public class MainActivity : AvaloniaMainActivity<App>
    {
        protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
        {
            //RequestPermissions(["android.permission.WRITE_EXTERNAL_STORAGE"],0);
            RequestFullscreenMode(FullscreenModeRequest.Enter, null);
            return base.CustomizeAppBuilder(builder)
                .WithInterFont();
        }
    }
}
