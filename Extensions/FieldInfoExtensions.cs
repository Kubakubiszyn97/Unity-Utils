using System;
using System.Collections;
using System.Reflection;

public static class FieldInfoExtensions
{
    public static Type GetFieldType(this FieldInfo fieldInfo)
    {
        var propertyType = fieldInfo.FieldType;
        if (propertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(propertyType))
        {
            return propertyType.GenericTypeArguments[0];
        }
        else if (propertyType.IsArray)
        {
            return propertyType.GetElementType();
        }
        return propertyType;
    }
}
