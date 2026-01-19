using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Util;
using Avalonia;
using Avalonia.Android;
using Avalonia.Maui;
using Avalonia.Media;
using Microsoft.Maui.ApplicationModel;
using UndertaleModToolAvalonia.Android.NativeViews;
using UndertaleModToolAvalonia.NativeViews;
using UTMTdrid;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace UndertaleModToolAvalonia.Android
{
    [Activity(
        Label = "QiuUTMTv4",
        Theme = "@style/MyTheme.NoActionBar",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.FullUser,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.UiMode)]
    public class MainActivity : AvaloniaMainActivity<App>
    {
        
        public const string Font = "avares://UndertaleModToolAvalonia/Assets/unifont.ttf#Unifont";

        public MainActivity()
        {
            try
            {
                ISoraEditorAndroid.Implementation = new SoraEditorAndroid(this);
            }
            catch (Exception e)
            {
                Log.Error("ISoraEditorAndroid", e.Message);
            }
            
        }
        protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
        {
            Com.Kongzue.Dialogx.DialogX.Init(Application);
            MAUIBridge.AskDialog = Bindme.dAskDialog;
            MAUIBridge.InputDialog = Bindme.dInputDialog;
            MAUIBridge.HasRequiredStoragePermission = HasStoragePermission;
            return base.CustomizeAppBuilder(builder)
                .UseMaui<MauiApplication>(this)
                .With(new FontManagerOptions
                {
                    DefaultFamilyName = Font
                })
                .LogToTrace()
                .UseAndroid()
                .UseSkia();
        }
        public bool CheckExternalStoragePermission()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
#pragma warning disable CA1416
                var result = Environment.IsExternalStorageManager;
                if (!result)
                {
                    var manage = Settings.ActionManageAppAllFilesAccessPermission;
                    Intent intent = new Intent(manage);
                    Uri? uri = Uri.Parse("package:" + AppInfo.Current.PackageName);
                    intent.SetData(uri);
                    StartActivity(intent);
                }
                return result;
#pragma warning restore CA1416
            }

            return true;
        }

        public async Task<bool> HasStoragePermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
                return false;
            if (!CheckExternalStoragePermission())
                return false;
            return true;
        }
    }
}
