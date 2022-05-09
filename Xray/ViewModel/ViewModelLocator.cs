// =============================================================
// Package:     Xray
// FileName:    ViewModelLocator.cs
// Author:      Hongwei Liu(Hongwei.Liu@pppharmapack.com)
// CreateDate:  2022/05/09
// EditDate:    2022/05/09
// CopyRight:   Copyright (c) 2022-2022 XingyangTechnology
// =============================================================

using DryIoc;

namespace Xray.ViewModel;

public class ViewModelLocator
{
    private readonly Container _container;
    public ViewModelLocator()
    {
        _container = new Container();

        _container.Register<MainViewModel>();
        _container.Register<SettingsViewModel>();
        _container.Register<DocumentStyleViewModel>();
    }

    public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();
    public SettingsViewModel SettingsViewModel => _container.Resolve<SettingsViewModel>();
    public DocumentStyleViewModel DocumentStyleViewModel => _container.Resolve<DocumentStyleViewModel>();
}