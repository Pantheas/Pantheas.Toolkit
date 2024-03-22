using Pantheas.Toolkit.Core.Interfaces.Services;

using MauiConnectivity = Microsoft.Maui.Networking.Connectivity;

namespace Pantheas.Toolkit.MAUI.Services;

public class Connectivity :
        IConnectivityService
{
    public event EventHandler? ConnectivityChanged;


    public bool HasInternetAccess =>
        MauiConnectivity.Current.NetworkAccess == NetworkAccess.Internet;


    public bool HasWiFiConnection =>
        MauiConnectivity.Current.ConnectionProfiles.Any(
            profile => profile == ConnectionProfile.WiFi);

    public bool HasCellularConnection =>
        MauiConnectivity.Current.ConnectionProfiles.Any(
            profile => profile == ConnectionProfile.Cellular);



    public Connectivity()
    {
        MauiConnectivity.Current.ConnectivityChanged += OnConnectivyChanged;
    }


    private void OnConnectivyChanged(
        object? sender,
        ConnectivityChangedEventArgs eventArgs)
    {
        RaiseConnectivyChanged();
    }

    private void RaiseConnectivyChanged()
    {
        var threadSafeCall = ConnectivityChanged;

        threadSafeCall?.Invoke(
            this,
            EventArgs.Empty);
    }
}
