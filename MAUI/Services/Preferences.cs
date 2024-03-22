using MauiPreferences = Microsoft.Maui.Storage.Preferences;

namespace Pantheas.Toolkit.MAUI.Services;

public class Preferences :
    Core.Interfaces.Services.IPreferences
{
    public void Set<TValue>(
        string key,
        TValue value)
    {
        MauiPreferences.Default.Set(
            key,
            value);
    }

    public TValue Get<TValue>(
        string key)
    {
        return MauiPreferences.Default.Get<TValue>(
            key,
            default);
    }

    public TValue Get<TValue>(
        string key,
        TValue defaultValue)
    {
        return MauiPreferences.Default.Get<TValue>(
            key,
            defaultValue);
    }



    public void Clear()
    {
        MauiPreferences.Default.Clear();
    }
}

