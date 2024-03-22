using Pantheas.Toolkit.Core.Interfaces.MVVM;
using Pantheas.Toolkit.MVVM.Navigation;

using System.Collections.Concurrent;
using System.Reflection;

namespace Pantheas.Toolkit.MAUI.Helpers;

internal static class NavigationParameterHelper
{
    private static readonly ConcurrentBag<MethodInfoCache> _methodInfoCache = [];


    internal static IDictionary<string, object> SetPropertiesByAttributes(
        object bindingContext,
        IDictionary<string, object> parameters)
    {
        var bindingContextType = bindingContext.GetType();

        var navigationParameterAttributes = bindingContextType
            .GetCustomAttributes(
                typeof(NavigationParameterAttribute),
                true)
            .OfType<NavigationParameterAttribute>();

        foreach (var propertyName in navigationParameterAttributes
            .Select(attribute => attribute.PropertyName)
            .Where(name => !string.IsNullOrWhiteSpace(
                name)))
        {
            if (!parameters.TryGetValue(
                propertyName,
                out var value))
            {
                continue;
            }


            var propertyInfo = bindingContextType.GetRuntimeProperty(
                propertyName);

            SetProperty(
                bindingContext,
                propertyInfo,
                value);
        }


        return parameters
            .Where(parameter => navigationParameterAttributes?.Any(attribute => attribute.PropertyName == parameter.Key) != true)
            .ToDictionary<string, object>();
    }

    internal static void SetProperty(
        object bindingContext,
        PropertyInfo property,
        object value)
    {
        if (property is null ||
            !property.CanWrite ||
            property.SetMethod?.IsPublic != true)
        {
            return;
        }


        var castValue = Convert.ChangeType(
            value,
            property.PropertyType);

        property.SetValue(
            bindingContext,
            castValue);
    }


    internal static async Task FindAndInvokeInitializationInterfaces(
        object bindingContext,
        object value)
    {
        var bindingContextType = bindingContext.GetType();
        var valueType = value.GetType();

        var referenceInterfaceType = typeof(IRequireInitializationParameter<>)
            .MakeGenericType(valueType);

        var interfaceType = bindingContextType
            .GetInterfaces()
            .FirstOrDefault(type => type == referenceInterfaceType);

        if (interfaceType is null)
        {
            return;
        }


        await InvokeInterfaceInitializationMethod(
            bindingContext,
            interfaceType,
            value);
    }

    internal static async Task InvokeInterfaceInitializationMethod(
        object bindingContext,
        Type interfaceType,
        object value)
    {
        var bindingContextType = bindingContext.GetType();

        var cachedInfo = _methodInfoCache.FirstOrDefault(
            cache =>
                cache.InstanceType == bindingContextType &&
                cache.ValueType == value.GetType());

        if (cachedInfo == null)
        {
            var methodInfo =
                bindingContextType
                .GetInterfaceMap(
                    interfaceType)
                .TargetMethods
                .FirstOrDefault();

            if (methodInfo == null)
            {
                return;
            }

            cachedInfo = new MethodInfoCache(
                bindingContextType,
                value.GetType(),
                methodInfo);
        }

        var initializationMethod = cachedInfo.MethodInfo;

        var parameters = new object[]
        {
            value
        };

        var task = (Task)initializationMethod.Invoke(
            bindingContext,
            parameters);

        await task.ConfigureAwait(
            false);
    }
}