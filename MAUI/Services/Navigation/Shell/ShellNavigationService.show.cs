using Pantheas.Toolkit.Core.Interfaces.MVVM;
using Pantheas.Toolkit.Core.Interfaces.Services;

namespace Pantheas.Toolkit.MAUI.Services.Navigation;

public partial class ShellNavigationService :
    INavigationService
{
    public async Task ShowAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        await Shell.Current.GoToAsync(
            typeof(TViewModel).Name);
    }

    public async Task ShowAsync<TViewModel>(
        IDictionary<string, object> parameters)
        where TViewModel : IViewModel
    {
        await Shell.Current.GoToAsync(
            typeof(TViewModel).Name,
            parameters);
    }

    public async Task ShowAsync<TViewModel, TData>(
        TData data)
        where TViewModel : IRequireInitializationParameter<TData>
    {
        var parameters = new Dictionary<string, object>
        {
            { typeof(TData).Name, data }
        };


        await Shell.Current.GoToAsync(
            typeof(TViewModel).Name,
            parameters);
    }

    public async Task ShowAsync<TViewModel, TData1, TData2>(
        TData1 data1,
        TData2 data2)
        where TViewModel : IRequireInitializationParameter<TData1>,
                            IRequireInitializationParameter<TData2>
    {
        var parameters = new Dictionary<string, object>
        {
            { typeof(TData1).Name, data1 },
            { typeof(TData2).Name, data2 }
        };


        await Shell.Current.GoToAsync(
            typeof(TViewModel).Name,
            parameters);
    }

    public async Task ShowAsync<TViewModel, TData1, TData2, TData3>(
        TData1 data1,
        TData2 data2,
        TData3 data3)
        where TViewModel : IRequireInitializationParameter<TData1>,
                            IRequireInitializationParameter<TData2>,
                            IRequireInitializationParameter<TData3>
    {
        var parameters = new Dictionary<string, object>
        {
            { typeof(TData1).Name, data1 },
            { typeof(TData2).Name, data2 },
            { typeof(TData3).Name, data3 }
        };


        await Shell.Current.GoToAsync(
            typeof(TViewModel).Name,
            parameters);
    }
}
