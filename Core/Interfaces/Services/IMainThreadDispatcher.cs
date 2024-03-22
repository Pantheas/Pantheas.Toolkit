namespace Pantheas.Toolkit.Core.Interfaces.Services;

public interface IMainThreadDispatcher
{
    bool IsMainThread { get; }



    void BeginInvokeOnMainThread(
        Action action);


    Task InvokeOnMainThreadAsync(
        Action action);

    Task InvokeOnMainThreadAsync(
        Func<Task> func);
}
