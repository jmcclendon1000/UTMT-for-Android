using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Platform;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Rendering;

namespace UndertaleModToolAvalonia.NativeViews;

public class SoraEditorControl: NativeControlHost
{
    private IPlatformHandle? _impl;
    private string _initialText = string.Empty;
    private TextDocument? _asmTextDocument;

    public SoraEditorControl()
    {
    }

    public string Text
    {
        get => Factory.GetText(_impl) ?? _initialText;
        set
        {
            if (_impl is not null)
                Factory.SetText(_impl,value);
            else
                _initialText = value;
        }
    }

    // public TextDocument Document
    // {
    //     get {
    //         var a=_asmTextDocument ?? new TextDocument();
    //         a.Text = Text;
    //         return a;
    //     }
    //     set => _asmTextDocument = value;
    // }
    
    public static readonly StyledProperty<TextDocument> DocumentProperty = TextView.DocumentProperty.AddOwner<SoraEditorControl>();
    public TextDocument Document
    {
        get => this.GetValue<TextDocument>(SoraEditorControl.DocumentProperty);
        set
        {
            this.SetValue<TextDocument>(SoraEditorControl.DocumentProperty, value, BindingPriority.LocalValue);
        }
    }

    public static ISoraEditorAndroid? Factory
    {
        get
        {
            return ISoraEditorAndroid.Implementation;
        }
    }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        if (Factory is null)
            return base.CreateNativeControlCore(parent);

        _impl = Factory.CreateControl(parent, () => base.CreateNativeControlCore(parent));
        return _impl;
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        base.DestroyNativeControlCore(control);
    }
}