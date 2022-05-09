using DryIoc;

namespace Xray.ViewModel;

public class ViewModelLocator
{
    private readonly Container _container;

    public ViewModelLocator()
    {
        _container = new Container();

        _container.Register<MainViewModel>(new SingletonReuse());
        _container.Register<SettingsViewModel>(new SingletonReuse());
        _container.Register<DocumentStyleViewModel>(new SingletonReuse());
    }

    public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();
    public SettingsViewModel SettingsViewModel => _container.Resolve<SettingsViewModel>();
    public DocumentStyleViewModel DocumentStyleViewModel => _container.Resolve<DocumentStyleViewModel>();
}