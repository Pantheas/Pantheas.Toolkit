namespace Pantheas.Toolkit.Core.Interfaces.MVVM;

public interface IViewModel
{
    bool IsInitialized { get; }


    Task InitializeAsync();
}
