using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Xray.ViewModel;

namespace Xray.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void DocumentViewerMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space || (e.Key == Key.Oem3 && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
        {
            Hide();
        }
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var locator = App.Current.Resources["Locator"] as ViewModelLocator;
        if (locator != null)
        {
            locator.GlobalHotKeyService.RegisterHotKey(Key.Oem3, ModifierKeys.Control | ModifierKeys.Alt, ((o, args) =>
            {
                Show();
            }));
            var bounds = locator.PersistService.Instance.WindowBounds;
            Left = bounds.Left;
            Top = bounds.Top;
            Width = bounds.Width;
            Height = bounds.Height;
            //locator.PersistService.WindowBounds = new Rect(new Point(Left, Top), new Size(Width, Height));
        }
    }

    private void MainWindow_OnClosed(object? sender, EventArgs e)
    {
        var locator = App.Current.Resources["Locator"] as ViewModelLocator;
        if (locator != null)
        {
            locator.PersistService.Instance.WindowBounds = new Rect(new Point(Left, Top), new Size(Width, Height));
        }
    }
}