using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using Xray.View;

namespace Xray.ViewModel;

public class MainViewModel : ViewModelBase
{
    private static readonly SolidColorBrush WhiteBrush = new(Colors.White);
    private static readonly SolidColorBrush TransparentBrush = new(Colors.Transparent);
    private RelayCommand? _callSettingsPageCommand;
    private FlowDocument? _document;
    private RelayCommand? _exitCommand;
    private bool _isTransparent;
    private RelayCommand? _loadDocumentCommand;
    private RelayCommand? _setBackgroundCommand;
    private ICommand? _setMainMenuVisibleCommand;
    private ICommand? _setWindowVisibility;
    private Visibility _showWindowChrome;

    public MainViewModel()
    {
        _document = new FlowDocument();

        _document.Blocks.Add(new Paragraph(new Run("E : Hide menu bar")));
        _document.Blocks.Add(new Paragraph(new Run("CTRL +1 : To transparent background")));
        _document.Blocks.Add(new Paragraph(new Run("A,D: Previous & Next page")));
        _document.Blocks.Add(new Paragraph(new Run("←,→: Previous & Next page")));
        _document.Blocks.Add(new Paragraph(new Run("CTRL+O: Open file")));
        _document.Blocks.Add(new Paragraph(new Run("CTRL+↑/↓：Font size up/down")));
        _document.Blocks.Add(new Paragraph(new Run("CTRL+`：Hide")));
        _document.Blocks.Add(new Paragraph(new Run("Space：Hide")));
        _document.Blocks.Add(new Paragraph(new Run("CTRL+ALT+`：UnHide")));
    }

    public Visibility ShowWindowChrome
    {
        get => _showWindowChrome;
        set => SetProperty(ref _showWindowChrome, value);
    }

    public ICommand SetMainMenuVisibleCommand => _setMainMenuVisibleCommand ??= new RelayCommand(SetMainMenuVisible);

    public FlowDocument? Document
    {
        get => _document;
        private set => SetProperty(ref _document, value);
    }

    public ICommand LoadDocumentCommand => _loadDocumentCommand ??= new RelayCommand(LoadDocument);

    public ICommand ExitCommand => _exitCommand ??= new RelayCommand(Exit);

    public bool IsTransparent
    {
        get => _isTransparent;
        set => SetProperty(ref _isTransparent, value);
    }

    public ICommand SetBackgroundCommand => _setBackgroundCommand ??= new RelayCommand(SetBackground);

    public ICommand CallSettingsPageCommand => _callSettingsPageCommand ??= new RelayCommand(CallSettingsPage);

    private void SetMainMenuVisible()
    {
        ShowWindowChrome = ShowWindowChrome == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }

    private void LoadDocument()
    {
        var openFileDialog = new OpenFileDialog {Filter = "文本文件|*.txt"};
        if (openFileDialog.ShowDialog() == true)
        {
            var path = openFileDialog.FileName;
            var content = File.ReadAllText(path);
            Document = new FlowDocument();
            Document.PageWidth = double.NaN;
            Document.ColumnWidth = double.NaN;

            foreach (var line in content.Split('\n')) Document.Blocks.Add(new Paragraph(new Run(line)));
        }
    }

    private void Exit()
    {
        Application.Current.Shutdown();
    }

    private void SetBackground()
    {
        IsTransparent = !IsTransparent;
    }

    private void CallSettingsPage()
    {
        var settingsPage = new Settings();
        settingsPage.Show();
    }
}