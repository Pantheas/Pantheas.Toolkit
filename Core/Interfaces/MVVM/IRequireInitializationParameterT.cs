namespace Pantheas.Toolkit.Core.Interfaces.MVVM;

public interface IRequireInitializationParameter<in TData>
{
    Task InitializeAsync(
        TData data);
}
