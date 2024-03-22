using Pantheas.Toolkit.Core.Interfaces.MVVM;
using Pantheas.Toolkit.MAUI.Helpers;

using Sharpnado.Tasks;

using System.Windows.Input;

namespace Pantheas.Toolkit.MAUI.Views;

public partial class ContentPage :
    Microsoft.Maui.Controls.ContentPage,
    IQueryAttributable
{
    private IViewModel? viewModel;
    protected virtual IViewModel? ViewModel
    {
        get => viewModel;
        set
        {
            viewModel = value;
            RegisterRoute();

            if (IsLoaded)
            {
                InitializeViewModel();
            }

            if (BindingContext != value)
            {
                BindingContext = value;
            }
        }
    }



    public static readonly BindableProperty BackCommandProperty =
        BindableProperty.Create(
            nameof(BackCommand),
            typeof(ICommand),
            typeof(ContentPage));

    public ICommand BackCommand
    {
        get => (ICommand)GetValue(BackCommandProperty);
        set => SetValue(BackCommandProperty, value);
    }


    public static readonly BindableProperty BackCommandParameterProperty =
        BindableProperty.Create(
            nameof(BackCommandParameter),
            typeof(object),
            typeof(ContentPage));

    public object BackCommandParameter
    {
        get => GetValue(BackCommandParameterProperty);
        set => SetValue(BackCommandParameterProperty, value);
    }


    public static readonly BindableProperty InfoViewProperty =
        BindableProperty.Create(
            nameof(InfoView),
            typeof(View),
            typeof(ContentPage),
            propertyChanged: OnInfoViewChanged);

    public View InfoView
    {
        get => (View)GetValue(InfoViewProperty);
        set => SetValue(InfoViewProperty, value);
    }



    public static readonly BindableProperty IsInfoViewVisibleProperty =
        BindableProperty.Create(
            nameof(IsInfoViewVisible),
            typeof(bool),
            typeof(ContentPage),
            defaultValue: false,
            propertyChanged: OnIsInfoViewVisibleChanged);

    public bool IsInfoViewVisible
    {
        get => (bool)GetValue(IsInfoViewVisibleProperty);
        set => SetValue(IsInfoViewVisibleProperty, value);
    }


    public static readonly BindableProperty ShouldInfoViewOverlapContentProperty =
        BindableProperty.Create(
            nameof(ShouldInfoViewOverlapContent),
            typeof(bool),
            typeof(ContentPage),
            defaultValue: true);

    public bool ShouldInfoViewOverlapContent
    {
        get => (bool)GetValue(ShouldInfoViewOverlapContentProperty);
        set => SetValue(ShouldInfoViewOverlapContentProperty, value);
    }


    public ContentPage()
        : base()
    {
        InitializeComponent();

        Loaded += OnPageLoaded;
    }

    public ContentPage(
        IViewModel viewModel)
        : this()
    {
        ViewModel = viewModel;
    }

    protected override void OnBindingContextChanged()
    {
        if (ViewModel is null &&
            BindingContext is IViewModel contextViewModel)
        {
            ViewModel = contextViewModel;
        }


        base.OnBindingContextChanged();
    }

    protected override bool OnBackButtonPressed()
    {
        if (BackCommand is null)
        {
            return base.OnBackButtonPressed();
        }

        if (BackCommand is not null &&
            BackCommand.CanExecute(
                BackCommandParameter))
        {
            BackCommand.Execute(
                BackCommandParameter);


            return true;
        }


        return false;
    }

    private void OnPageLoaded(
        object? sender,
        EventArgs eventArgs)
    {
        InitializeViewModel();
    }

    public virtual void ApplyQueryAttributes(
        IDictionary<string, object> query)
    {
        if (query?.Any() is true)
        {
            query = NavigationParameterHelper.SetPropertiesByAttributes(
                BindingContext,
                query);
        }


        foreach (var value in query?.Values ??
            Enumerable.Empty<object>())
        {
            TaskMonitor.Create(() =>
                NavigationParameterHelper.FindAndInvokeInitializationInterfaces(
                    BindingContext,
                    value));
        }


        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        if (ViewModel is null ||
            ViewModel.IsInitialized)
        {
            return;
        }


        TaskMonitor.Create(
            ViewModel.InitializeAsync);
    }

    private void RegisterRoute()
    {
        if (ViewModel is null)
        {
            return;
        }

        try
        {
            Routing.RegisterRoute(
                ViewModel.GetType().Name,
                GetType());
        }
        catch (ArgumentException exception)
        when (exception.Message?.ToLower().Contains(
            "duplicate",
            StringComparison.CurrentCultureIgnoreCase) == true)
        {
            // wait for MAUI API change
        }
    }


    private async Task ShowInfoViewAsync()
    {
        await InfoView.ScaleYTo(
            scale: 1,
            length: 150,
            easing: Easing.SinIn);
    }

    private async Task HideInfoViewAsync()
    {
        await InfoView.ScaleYTo(
            scale: 0,
            length: 150,
            easing: Easing.SinIn);
    }


    private static void OnInfoViewChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not ContentPage)
        {
            return;
        }

        if (newValue is not View view)
        {
            return;
        }


        view.ZIndex = int.MaxValue;
        view.ScaleY = 0;
    }


    private static async void OnIsInfoViewVisibleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not ContentPage page)
        {
            return;
        }

        if (newValue is not bool isVisible)
        {
            TaskMonitor.Create(
                page.HideInfoViewAsync);

            return;
        }


        if (isVisible)
        {
            await page.ShowInfoViewAsync();
        }
        else
        {
            await page.HideInfoViewAsync();
        }
    }
}