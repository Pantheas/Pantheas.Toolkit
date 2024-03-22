using Pantheas.Toolkit.Core.Interfaces.MVVM;
using Pantheas.Toolkit.Core.Interfaces.Services;
using Pantheas.Toolkit.MAUI.Services;
using Pantheas.Toolkit.MAUI.Services.Camera;
using Pantheas.Toolkit.MAUI.Services.Navigation;

namespace Pantheas.Toolkit.MAUI;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseAmon(
        this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<INavigationService, ShellNavigationService>();

        builder.Services.AddTransient<ICamera, Camera>();

        builder.Services.AddTransient<IFileSystemHelper, FileSystemHelper>();

        builder.Services.AddTransient<IConnectivityService, Services.Connectivity>();

        builder.Services.AddTransient<IMainThreadDispatcher, MainThreadDispatcher>();

        builder.Services.AddTransient<Core.Interfaces.Services.IPreferences, Services.Preferences>();
        builder.Services.AddTransient<Core.Interfaces.Services.ISecureStorage, Services.SecureStorage>();
        builder.Services.AddTransient<Core.Interfaces.Services.Permissions.IPermissions, Services.Permissions>();


        return builder;
    }

    public static MauiAppBuilder RegisterForNavigation<TViewModel, TPage>(
        this MauiAppBuilder builder)
        where TViewModel : class, IViewModel
        where TPage : Page
    {
        builder.Services.AddTransient<TViewModel>();
        builder.Services.AddTransient<TPage>();

        Routing.RegisterRoute(
            typeof(TViewModel).Name,
            typeof(TPage));


        return builder;
    }
}
