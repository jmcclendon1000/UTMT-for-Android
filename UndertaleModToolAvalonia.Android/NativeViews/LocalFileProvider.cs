using System;
using System.IO;
using Android.Text.Style;
using Java.Interop;

namespace UndertaleModToolAvalonia.Android.NativeViews;

public class LocalFileProvider: Java.Lang.Object,global::IO.Github.Rosemoe.Sora.Langs.Textmate.Registry.Provider.IFileResolver
{
    private string basePath;

    public LocalFileProvider(string basePath)
    {
        this.basePath = basePath;
    }
    public Stream? ResolveStreamByPath(string? p0)
    {
        if (p0 == null) return null;
        return new FileStream(Path.Combine(basePath,p0),FileMode.Open, FileAccess.Read);
    }
}