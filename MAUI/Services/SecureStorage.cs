using System.Text.Json;

using MauiSecureStorage = Microsoft.Maui.Storage.SecureStorage;

namespace Pantheas.Toolkit.MAUI.Services;

public class SecureStorage :
    Core.Interfaces.Services.ISecureStorage
{
    public async Task SetAsync<TValue>(
        string key,
        TValue value)
    {
        string valueAsString = JsonSerializer.Serialize(
            value);

        await MauiSecureStorage.Default.SetAsync(
            key,
            valueAsString);
    }

    public async Task SetAsync(
        string key,
        string value)
    {
        await MauiSecureStorage.Default.SetAsync(
            key,
            value);
    }


    public async Task<TValue> GetAsync<TValue>(
        string key)
    {
        string value = await MauiSecureStorage.Default.GetAsync(
            key);

        if (string.IsNullOrWhiteSpace(
            value))
        {
            return default;
        }


        return JsonSerializer.Deserialize<TValue>(
            value);
    }

    public async Task<string> GetAsync(
        string key)
    {
        return await MauiSecureStorage.Default.GetAsync(
            key);
    }



    public bool Remove(
        string key)
    {
        return MauiSecureStorage.Default.Remove(
            key);
    }

    public void Clear()
    {
        MauiSecureStorage.Default.RemoveAll();
    }
}