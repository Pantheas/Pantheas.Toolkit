using Pantheas.Toolkit.Core.Interfaces.MVVM;
using Pantheas.Toolkit.Core.Interfaces.Services;

namespace Pantheas.Toolkit.MAUI.Services.Navigation;

public partial class ShellNavigationService :
    INavigationService
{
    public async Task GoHomeAsync()
    {
        if (Shell.Current.CurrentPage == Application.Current?.MainPage)
        {
            return;
        }


        await Shell.Current.GoToAsync(
            "/");
    }


    public async Task GoBackAsync()
    {
        if (Shell.Current.CurrentPage == Application.Current?.MainPage)
        {
            return;
        }


        await Shell.Current.GoToAsync(
            "..");
    }



    /// <summary>
    /// Looks for the specified type in the navigation stack
    /// Replaces the stack with a new view of the given type
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <returns></returns>
    public async Task GoBackToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        await Shell.Current.GoToAsync(
            $"///{typeof(TViewModel).Name}");
    }

    /// <summary>
    /// Looks for the specified type in the navigation stack
    /// Replaces the stack with a new view of the given type
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <typeparam name="TData">Type of initialization parameter</typeparam>
    /// <param name="data">Initialization parameter for <see cref="IRequireInitializationParameter{TData}"/></param>
    /// <returns></returns>
    public async Task GoBackToAsync<TViewModel, TData>(
        TData data)
        where TViewModel : IRequireInitializationParameter<TData>
    {
        var parameters = new Dictionary<string, object>
        {
            { string.Empty, data }
        };


        await Shell.Current.GoToAsync(
            $"///{typeof(TViewModel).Name}",
            parameters);
    }
}
