using Pantheas.Toolkit.Core.Interfaces.MVVM;

namespace Pantheas.Toolkit.Core.Interfaces.Services;

public interface INavigationService
{
    void SetRoot<TViewModel>();


    Task ShowAsync<TViewModel>()
        where TViewModel : IViewModel;

    Task ShowAsync<TViewModel>(
        IDictionary<string, object> parameters)
        where TViewModel : IViewModel;

    Task ShowAsync<TViewModel, TData>(
        TData data)
        where TViewModel : IRequireInitializationParameter<TData>;

    Task ShowAsync<TViewModel, TData1, TData2>(
        TData1 data1,
        TData2 data2)
        where TViewModel : IRequireInitializationParameter<TData1>,
                            IRequireInitializationParameter<TData2>;

    Task ShowAsync<TViewModel, TData1, TData2, TData3>(
        TData1 data1,
        TData2 data2,
        TData3 data3)
        where TViewModel : IRequireInitializationParameter<TData1>,
                            IRequireInitializationParameter<TData2>,
                            IRequireInitializationParameter<TData3>;



    Task GoHomeAsync();
    Task GoBackAsync();


    Task GoBackToAsync<TViewModel>()
        where TViewModel : IViewModel;

    Task GoBackToAsync<TViewModel, TData>(
        TData data)
        where TViewModel : IRequireInitializationParameter<TData>;
}
