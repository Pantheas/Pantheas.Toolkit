namespace Pantheas.Toolkit.MVVM.Navigation;

[AttributeUsage(
    AttributeTargets.Class,
    AllowMultiple = true)]
public class NavigationParameterAttribute :
    Attribute
{
    public string PropertyName { get; }


    public NavigationParameterAttribute(
        string propertyName)
    {
        PropertyName = propertyName;
    }
}
