using CommunityToolkit.Mvvm.ComponentModel;

using Pantheas.Toolkit.Core.Interfaces.MVVM;

namespace Pantheas.Toolkit.MVVM.Base;

public abstract partial class ViewModelBase :
    ObservableObject,
    IViewModel
{
    [ObservableProperty]
    private bool isInitialized = false;


    public virtual Task InitializeAsync()
    {
        IsInitialized = true;


        return Task.CompletedTask;
    }
}