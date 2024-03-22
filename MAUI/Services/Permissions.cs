using Pantheas.Toolkit.Core.Interfaces.Services.Permissions;

using MauiPermissions = Microsoft.Maui.ApplicationModel.Permissions;

namespace Pantheas.Toolkit.MAUI.Services;

public class Permissions :
    IPermissions
{
    private static readonly Dictionary<PermissionType, Func<Task<PermissionStatus>>> _requestMap = new()
    {
        { PermissionType.Battery, RequestInternalAsync<MauiPermissions.Battery> },
        { PermissionType.Bluetooth, RequestInternalAsync<MauiPermissions.Bluetooth> },
        { PermissionType.Calendar, RequestInternalAsync<MauiPermissions.CalendarWrite> },
        { PermissionType.Camera, RequestInternalAsync<MauiPermissions.Camera> },
        { PermissionType.Contacts, RequestInternalAsync<MauiPermissions.ContactsWrite> },
        { PermissionType.Flashlight, RequestInternalAsync<MauiPermissions.Flashlight> },
        { PermissionType.LaunchApp, RequestInternalAsync<MauiPermissions.LaunchApp> },
        { PermissionType.LocationWhenInUse, RequestInternalAsync<MauiPermissions.LocationWhenInUse> },
        { PermissionType.LocationAlways, RequestInternalAsync<MauiPermissions.LocationAlways> },
        { PermissionType.Maps, RequestInternalAsync<MauiPermissions.Maps> },
        { PermissionType.Media, RequestInternalAsync<MauiPermissions.Media> },
        { PermissionType.Microphone, RequestInternalAsync<MauiPermissions.Microphone> },
        { PermissionType.NearbyWifiDevices, RequestInternalAsync<MauiPermissions.NearbyWifiDevices> },
        { PermissionType.NetworkState, RequestInternalAsync<MauiPermissions.NetworkState> },
        { PermissionType.Phone, RequestInternalAsync<MauiPermissions.Phone> },
        { PermissionType.Photos, RequestInternalAsync<MauiPermissions.Photos> },
        { PermissionType.PostNotifications, RequestInternalAsync<MauiPermissions.PostNotifications> },
        { PermissionType.Reminders, RequestInternalAsync<MauiPermissions.Reminders> },
        { PermissionType.Sensors, RequestInternalAsync<MauiPermissions.Sensors> },
        { PermissionType.Sms, RequestInternalAsync<MauiPermissions.Sms> },
        { PermissionType.Speech, RequestInternalAsync<MauiPermissions.Speech> },
        { PermissionType.Storage, RequestInternalAsync<MauiPermissions.StorageWrite> },
        { PermissionType.Vibrate, RequestInternalAsync<MauiPermissions.Vibrate> },
    };

    private static readonly Dictionary<PermissionType, Func<Task<PermissionStatus>>> _checkStatusMap = new()
    {
        { PermissionType.Battery, MauiPermissions.CheckStatusAsync<MauiPermissions.Battery> },
        { PermissionType.Bluetooth, MauiPermissions.CheckStatusAsync<MauiPermissions.Bluetooth> },
        { PermissionType.Calendar, MauiPermissions.CheckStatusAsync<MauiPermissions.CalendarWrite> },
        { PermissionType.Camera, MauiPermissions.CheckStatusAsync<MauiPermissions.Camera> },
        { PermissionType.Contacts, MauiPermissions.CheckStatusAsync<MauiPermissions.ContactsWrite> },
        { PermissionType.Flashlight, MauiPermissions.CheckStatusAsync<MauiPermissions.Flashlight> },
        { PermissionType.LaunchApp, MauiPermissions.CheckStatusAsync<MauiPermissions.LaunchApp> },
        { PermissionType.LocationWhenInUse, MauiPermissions.CheckStatusAsync<MauiPermissions.LocationWhenInUse> },
        { PermissionType.LocationAlways, MauiPermissions.CheckStatusAsync<MauiPermissions.LocationAlways> },
        { PermissionType.Maps, MauiPermissions.CheckStatusAsync<MauiPermissions.Maps> },
        { PermissionType.Media, MauiPermissions.CheckStatusAsync<MauiPermissions.Media> },
        { PermissionType.Microphone, MauiPermissions.CheckStatusAsync<MauiPermissions.Microphone> },
        { PermissionType.NearbyWifiDevices, MauiPermissions.CheckStatusAsync<MauiPermissions.NearbyWifiDevices> },
        { PermissionType.NetworkState, MauiPermissions.CheckStatusAsync<MauiPermissions.NetworkState> },
        { PermissionType.Phone, MauiPermissions.CheckStatusAsync<MauiPermissions.Phone> },
        { PermissionType.Photos, MauiPermissions.CheckStatusAsync<MauiPermissions.Photos> },
        { PermissionType.PostNotifications, MauiPermissions.CheckStatusAsync<MauiPermissions.PostNotifications> },
        { PermissionType.Reminders, MauiPermissions.CheckStatusAsync<MauiPermissions.Reminders> },
        { PermissionType.Sensors, MauiPermissions.CheckStatusAsync<MauiPermissions.Sensors> },
        { PermissionType.Sms, MauiPermissions.CheckStatusAsync<MauiPermissions.Sms> },
        { PermissionType.Speech, MauiPermissions.CheckStatusAsync<MauiPermissions.Speech> },
        { PermissionType.Storage, MauiPermissions.CheckStatusAsync<MauiPermissions.StorageWrite> },
        { PermissionType.Vibrate, MauiPermissions.CheckStatusAsync<MauiPermissions.Vibrate> },
    };


    public async Task<bool> RequestAsync(
        PermissionType permission)
    {
        if (!_requestMap.TryGetValue(
            permission,
            out var requestFunc))
        {
            return false;
        }

        var status = await requestFunc();


        return status == PermissionStatus.Granted;
    }

    public async Task<bool> IsGrantedAsync(
        PermissionType permission)
    {
        if (!_checkStatusMap.TryGetValue(
            permission,
            out var requestFunc))
        {
            return false;
        }

        var status = await requestFunc();


        return status == PermissionStatus.Granted;
    }

    private static async Task<PermissionStatus> RequestInternalAsync<TPermission>()
        where TPermission : MauiPermissions.BasePermission, new()
    {
        return await MauiPermissions.RequestAsync<TPermission>();
    }
}
