using System.Reflection;

namespace Pantheas.Toolkit.MAUI;

internal class MethodInfoCache
{
    public Type InstanceType { get; }
    public Type ValueType { get; }

    public MethodInfo MethodInfo { get; }


    public MethodInfoCache(
        Type instanceType,
        Type valueType,
        MethodInfo methodInfo)
    {
        InstanceType = instanceType;
        ValueType = valueType;

        MethodInfo = methodInfo;
    }
}

