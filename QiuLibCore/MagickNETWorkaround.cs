using System.Runtime.InteropServices;
using ImageMagick;

namespace UTMTdrid;

public unsafe class MagickNETWorkaround
{
    [DllImport("libc")]
    static extern int dlinfo(IntPtr handle, int request, IntPtr info);

    [DllImport("libc")]
    static extern IntPtr dlopen(string filename, int flags);

    private const int RTLD_DI_ORIGIN = 6;
    private const int RTLD_NOW = 2;
    private const int RTLD_DEEPBIND = 8;

    public static void Apply()
    {
        if (RuntimeInformation.RuntimeIdentifier.Contains("musl"))
            throw new PlatformNotSupportedException("musl doesn't support RTLD_DEEPBIND");

        var libraryPathBytes = Marshal.AllocHGlobal(4096);
        var handle = NativeLibrary.Load("Magick.Native-Q8-arm64", typeof(MagickSettings).Assembly, null);
        dlinfo(handle, RTLD_DI_ORIGIN, libraryPathBytes);
        var libraryOrigin = Marshal.PtrToStringUTF8(libraryPathBytes) ?? string.Empty;
        Marshal.FreeHGlobal(libraryPathBytes);
        var libraryPath = Path.Combine(libraryOrigin, "Magick.Native-Q8-arm64.dll.so");

        NativeLibrary.Free(handle);
        var forceLoadedHandle = dlopen(libraryPath, RTLD_NOW | RTLD_DEEPBIND);
        if (forceLoadedHandle == IntPtr.Zero)
            throw new DllNotFoundException($"Unable to load {libraryPath} via dlopen");

        NativeLibrary.SetDllImportResolver(typeof(MagickSettings).Assembly, (name, assembly, searchPath) =>
        {
            if (name.Contains("Magick.Native-Q8-arm64"))
                return dlopen(libraryPath, RTLD_NOW | RTLD_DEEPBIND);
            return NativeLibrary.Load(name, assembly, searchPath);
        });
    }
}