namespace Pantheas.Toolkit.Core.Interfaces.Services;

public interface ISecureStorage
{
    Task SetAsync<TValue>(
        string key,
        TValue value);

    Task SetAsync(
        string key,
        string value);


    Task<TValue> GetAsync<TValue>(
        string key);

    Task<string> GetAsync(
        string key);


    bool Remove(
        string key);

    void Clear();
}
