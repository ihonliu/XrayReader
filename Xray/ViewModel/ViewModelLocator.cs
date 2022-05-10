using System;
using System.Windows;
using DryIoc;
using Xray.Model;
using Xray.Services;

namespace Xray.ViewModel;

public class ViewModelLocator: IDisposable
{
    private readonly Container _container;

    public ViewModelLocator()
    {
        _container = new Container();

        _container.Register<MainViewModel>(new SingletonReuse());
        _container.Register<SettingsViewModel>(new SingletonReuse());
        _container.Register<DocumentStyleViewModel>(new SingletonReuse());

        _container.RegisterInstance(new PersistService<Config>());
        _container.RegisterInstance(new GlobalHotKeyService());
    }

    public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();
    public SettingsViewModel SettingsViewModel => _container.Resolve<SettingsViewModel>();
    public DocumentStyleViewModel DocumentStyleViewModel => _container.Resolve<DocumentStyleViewModel>();

    public PersistService<Config> PersistService => _container.Resolve <PersistService<Config>>();
    public GlobalHotKeyService GlobalHotKeyService => _container.Resolve<GlobalHotKeyService>();

    public void Dispose()
    {
        _container.Dispose();
    }
}