using Pantheas.Toolkit.Core.Interfaces.MVVM;

namespace Pantheas.Toolkit.MVVM.Base;

public abstract partial class ViewModelBase<TData> :
    ViewModelBase,
    IRequireInitializationParameter<TData>
{
    public abstract Task InitializeAsync(
        TData data);
}


