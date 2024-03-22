namespace Pantheas.Toolkit.Core.Interfaces.Services;

public interface IConnectivityService
{
    event EventHandler ConnectivityChanged;


    public bool HasInternetAccess { get; }


    public bool HasWiFiConnection { get; }

    public bool HasCellularConnection { get; }
}