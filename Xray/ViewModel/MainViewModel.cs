// =============================================================
// Package:     Xray
// FileName:    MainViewModel.cs
// Author:      Hongwei Liu(Hongwei.Liu@pppharmapack.com)
// CreateDate:  2022/05/09
// EditDate:    2022/05/09
// CopyRight:   Copyright (c) 2022-2022 XingyangTechnology
// =============================================================

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Xray.ViewModel;

public class MainViewModel : ViewModelBase
{
    private ICommand? _setMainMenuVisibleCommand;
    private ICommand? _setWindowVisibility;
    private Visibility _mainMenuVisible;
    private FlowDocument? _document;
    private RelayCommand? _loadDocumentCommand;
    private RelayCommand? _exitCommand;
    private RelayCommand? _setBackgroundCommand;
    private SolidColorBrush _background = WhiteBrush;
    private bool _isTransparent;
    public MainViewModel() { }

    public Visibility MainMenuVisible
    {
        get => _mainMenuVisible;
        set => SetProperty(ref _mainMenuVisible, value);
    }

    public ICommand SetMainMenuVisibleCommand => _setMainMenuVisibleCommand ??= new RelayCommand(SetMainMenuVisible);

    private void SetMainMenuVisible()
    {
        MainMenuVisible = MainMenuVisible == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }

    public Visibility WindowVisibility { get; set; }

    public ICommand SetWindowVisibility => _setWindowVisibility ??= new RelayCommand(SetWindowVisibilityFunc);

    private void SetWindowVisibilityFunc()
    {
        WindowVisibility = WindowVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
    }

    public FlowDocument? Document
    {
        get => _document;
        private set => SetProperty(ref _document, value);
    }

    public ICommand LoadDocumentCommand => _loadDocumentCommand ??= new RelayCommand(LoadDocument);

    private void LoadDocument()
    {
        var openFileDialog = new OpenFileDialog() { Filter = "文本文件|*.txt" };
        if (openFileDialog.ShowDialog() == true)
        {
            var path = openFileDialog.FileName;
            var content = System.IO.File.ReadAllText(path);
            Document = new FlowDocument();
            Document.PageWidth = double.NaN;
            Document.ColumnWidth = double.NaN;

            foreach (var line in content.Split('\n'))
            {
                Document.Blocks.Add(new Paragraph(new Run(line)));
            }
        }
    }

    public ICommand ExitCommand => _exitCommand ??= new RelayCommand(Exit);

    private void Exit()
    {
        App.Current.Shutdown();
    }

    private static readonly SolidColorBrush WhiteBrush = new SolidColorBrush(Colors.White);
    private static readonly SolidColorBrush TransparentBrush = new SolidColorBrush(Colors.Transparent);

    public bool IsTransparent
    {
        get => _isTransparent;
        set => SetProperty(ref _isTransparent, value);
    }

    public ICommand SetBackgroundCommand => _setBackgroundCommand ??= new RelayCommand(SetBackground);

    private void SetBackground()
    {
        IsTransparent = !IsTransparent;
    }
}