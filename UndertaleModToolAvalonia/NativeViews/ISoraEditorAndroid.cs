using System;
using Avalonia.Platform;

namespace UndertaleModToolAvalonia.NativeViews;

public interface ISoraEditorAndroid
{
    public IPlatformHandle CreateControl(IPlatformHandle parent, Func<IPlatformHandle> createDefault);
    public void SetText(IPlatformHandle androidViewControlHandle, string text);
    public string GetText(IPlatformHandle androidViewControlHandle);
    
    public static ISoraEditorAndroid? Implementation { get; set; }
}