using Pantheas.Toolkit.Core.Interfaces.Services;

namespace Pantheas.Toolkit.MAUI.Services;

public class MainThreadDispatcher :
    IMainThreadDispatcher
{
    public bool IsMainThread =>
        MainThread.IsMainThread;



    public void BeginInvokeOnMainThread(
        Action action)
    {
        if (IsMainThread)
        {
            action.Invoke();
        }


        MainThread.BeginInvokeOnMainThread(
            action);
    }


    public async Task InvokeOnMainThreadAsync(
        Action action)
    {
        if (IsMainThread)
        {
            action.Invoke();
        }


        await MainThread.InvokeOnMainThreadAsync(
            action);
    }

    public async Task InvokeOnMainThreadAsync(
        Func<Task> func)
    {
        if (IsMainThread)
        {
            await func.Invoke();
        }

        await MainThread.InvokeOnMainThreadAsync(
            func);
    }
}

