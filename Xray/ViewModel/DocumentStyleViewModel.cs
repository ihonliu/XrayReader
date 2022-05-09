using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.Input;
using WPF.ColorPicker;
using WPF.ColorPicker.Code;

namespace Xray.ViewModel;

public class DocumentStyleViewModel : ViewModelBase
{
    private const double DefaultFontSize = 12;
    private static readonly FontFamily DefaultFont = new("Microsoft YaHei");
    private List<FontFamily> _availableFonts;
    private RelayCommand? _decreaseFontSizeCommand;

    private FontFamily _font = DefaultFont;
    private SolidColorBrush _fontColor = new(Colors.Gray);
    private double _fontSize = DefaultFontSize;
    private RelayCommand? _increaseFontSize;
    private RelayCommand _pickColor;

    public DocumentStyleViewModel()
    {
        RefreshFonts();
    }

    public FontFamily Font
    {
        get => _font;
        set => SetProperty(ref _font, value);
    }

    public double FontSize
    {
        get => _fontSize;
        set => SetProperty(ref _fontSize, value);
    }

    public SolidColorBrush FontColor
    {
        get => _fontColor;
        set => SetProperty(ref _fontColor, value);
    }

    public ICommand IncreaseFontSizeCommand => _increaseFontSize ??= new RelayCommand(IncreaseFontSize);
    public ICommand DecreaseFontSizeCommand => _decreaseFontSizeCommand ?? new RelayCommand(DecreaseFontSize);

    public IReadOnlyList<FontFamily> AvailableFonts => _availableFonts;

    public ICommand PickColorCommand => _pickColor ??= new RelayCommand(PickColor);

    private void IncreaseFontSize()
    {
        FontSize += 1;
    }

    private void DecreaseFontSize()
    {
        FontSize -= 1;
    }

    private void PickColor()
    {
        if (ColorPickerWindow.ShowDialog(out var color, ColorPickerDialogOptions.SimpleView))
            FontColor = new SolidColorBrush(color);
    }


    private void RefreshFonts()
    {
        _availableFonts = new List<FontFamily>(Fonts.SystemFontFamilies.Count);
        foreach (var font in Fonts.SystemFontFamilies) _availableFonts.Add(font);
        OnPropertyChanged(nameof(AvailableFonts));
    }
}