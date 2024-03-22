namespace Pantheas.Toolkit.Core.Interfaces.Services;

public interface IPreferences
{
    void Set<TValue>(
        string key,
        TValue value);

    TValue Get<TValue>(
        string key);

    TValue Get<TValue>(
        string key,
        TValue defaultValue);


    void Clear();
}