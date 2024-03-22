using Pantheas.Toolkit.Core.Interfaces.Services;

namespace Pantheas.Toolkit.MAUI.Services.Navigation;

public partial class ShellNavigationService :
    INavigationService
{
    private readonly IServiceProvider _serviceProvider;


    public ShellNavigationService(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public void SetRoot<TViewModel>()
    {
        var content = Routing.GetOrCreateContent(
            typeof(TViewModel).Name,
            _serviceProvider);

        if (content is not Page mainPage)
        {
            throw new InvalidOperationException();
        }

        if (mainPage.BindingContext is not TViewModel)
        {
            var viewModel = _serviceProvider.GetService<TViewModel>();
            mainPage.BindingContext = viewModel;
        }


        MainThread.BeginInvokeOnMainThread(() =>
        {
            Application.Current.MainPage = mainPage;
        });
    }
}
