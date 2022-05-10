using System.Windows;
using Xray.ViewModel;

namespace Xray;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void App_OnExit(object sender, ExitEventArgs e)
    {
        var locator = Application.Current.Resources["Locator"] as ViewModelLocator ;
        if (locator != null)
        {
            locator.PersistService.Save();
        }
    }
}