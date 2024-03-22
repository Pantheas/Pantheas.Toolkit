namespace Pantheas.Toolkit.Core.Interfaces.Services.Permissions;

public interface IPermissions
{
    Task<bool> RequestAsync(
        PermissionType permission);

    Task<bool> IsGrantedAsync(
        PermissionType permission);
}