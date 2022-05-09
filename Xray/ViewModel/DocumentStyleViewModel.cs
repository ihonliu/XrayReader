// =============================================================
// Package:     Xray
// FileName:    DocumentStyle.cs
// Author:      Hongwei Liu(Hongwei.Liu@pppharmapack.com)
// CreateDate:  2022/05/09
// EditDate:    2022/05/09
// CopyRight:   Copyright (c) 2022-2022 XingyangTechnology
// =============================================================

using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.Input;

namespace Xray.ViewModel;

public class DocumentStyleViewModel : ViewModelBase
{
    private static readonly FontFamily DefaultFont = new FontFamily("Microsoft YaHei");
    private const double DefaultFontSize = 12;

    private FontFamily _font = DefaultFont;
    private double _fontSize = DefaultFontSize;
    private RelayCommand? _increaseFontSize;

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

    public ICommand IncreaseFontSizeCommand => _increaseFontSize ??= new RelayCommand(IncreaseFont);

    private void IncreaseFont()
    {
        FontSize += 1;
    }
}