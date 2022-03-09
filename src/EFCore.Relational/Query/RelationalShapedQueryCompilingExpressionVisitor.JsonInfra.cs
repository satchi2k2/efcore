// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;

namespace Microsoft.EntityFrameworkCore.Query;

public partial class RelationalShapedQueryCompilingExpressionVisitor
{
    //private readonly MethodInfo _materializeIncludedJsonEntityMethodInfo = typeof(ShaperProcessingExpressionVisitor).GetMethod(nameof(MaterializeIncludedJsonEntity))!;
    //private readonly MethodInfo _materializeIncludedJsonEntityCollectionMethodInfo = typeof(ShaperProcessingExpressionVisitor).GetMethod(nameof(MaterializeIncludedJsonEntityCollection))!;
    //private readonly MethodInfo _materializeRootJsonEntityMethodInfo = typeof(ShaperProcessingExpressionVisitor).GetMethod(nameof(MaterializeRootJsonEntity))!;
    //private readonly MethodInfo _materializeRootJsonEntityCollectionMethodInfo = typeof(ShaperProcessingExpressionVisitor).GetMethod(nameof(MaterializeRootJsonEntityCollection))!;

    //public static JsonElement ExtractJsonElement(DbDataReader dataReader, int index, string[] additionalPath)
    //{
    //    var jsonString = dataReader.GetString(index);
    //    var jsonDocument = JsonDocument.Parse(jsonString);
    //    var jsonElement = jsonDocument.RootElement;

    //    foreach (var pathElement in additionalPath)
    //    {
    //        jsonElement = jsonElement.GetProperty(pathElement);
    //    }

    //    return jsonElement;
    //}

    //public static object? ExtractJsonProperty(JsonElement element, string propertyName, Type returnType)
    //{
    //    var jsonElementProperty = element.GetProperty(propertyName);
    //    if (returnType == typeof(int))
    //    {
    //        return jsonElementProperty.GetInt32();
    //    }
    //    if (returnType == typeof(DateTime))
    //    {
    //        return jsonElementProperty.GetDateTime();
    //    }
    //    if (returnType == typeof(bool))
    //    {
    //        return jsonElementProperty.GetBoolean();
    //    }
    //    if (returnType == typeof(decimal))
    //    {
    //        return jsonElementProperty.GetDecimal();
    //    }
    //    if (returnType == typeof(string))
    //    {
    //        return jsonElementProperty.GetString();
    //    }
    //    else
    //    {
    //        // TODO: do for other types also
    //        // later, just codegen the propery access for better perf
    //        throw new InvalidOperationException("unsupported type");
    //    }
    //}

    //public static void MaterializeIncludedJsonEntity<TIncludingEntity, TIncludedEntity>(
    //    QueryContext queryContext,
    //    JsonElement jsonElement,
    //    object[] keyPropertyValues,
    //    TIncludingEntity entity,
    //    bool optionalDependent,
    //    Func<QueryContext, object[], JsonElement, TIncludedEntity> innerShaper,
    //    Action<TIncludingEntity, TIncludedEntity> fixup)
    //    where TIncludingEntity : class
    //    where TIncludedEntity : class
    //{
    //    if (jsonElement.ValueKind == JsonValueKind.Null)
    //    {
    //        if (optionalDependent)
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            throw new InvalidOperationException("Required Json entity not found.");
    //        }
    //    }
    //    else
    //    {
    //        var included = innerShaper(queryContext, keyPropertyValues, jsonElement);
    //        fixup(entity, included);
    //    }
    //}

    //public static void MaterializeIncludedJsonEntityCollection<TIncludingEntity, TIncludedCollectionElement>(
    //    QueryContext queryContext,
    //    JsonElement jsonElement,
    //    object[] keyPropertyValues,
    //    TIncludingEntity entity,
    //    Func<QueryContext, object[], JsonElement, TIncludedCollectionElement> innerShaper,
    //    Action<TIncludingEntity, TIncludedCollectionElement> fixup)
    //    where TIncludingEntity : class
    //    where TIncludedCollectionElement : class
    //{
    //    var newKeyPropertyValues = new object[keyPropertyValues.Length + 1];
    //    Array.Copy(keyPropertyValues, newKeyPropertyValues, keyPropertyValues.Length);

    //    var i = 0;
    //    foreach (var jsonArrayElement in jsonElement.EnumerateArray())
    //    {
    //        newKeyPropertyValues[^1] = ++i;

    //        var resultElement = innerShaper(queryContext, newKeyPropertyValues, jsonArrayElement);

    //        fixup(entity, resultElement);
    //    }
    //}

    //public static TEntity MaterializeRootJsonEntity<TEntity>(
    //    QueryContext queryContext,
    //    JsonElement jsonElement,
    //    object[] keyPropertyValues,
    //    Func<QueryContext, object[], JsonElement, TEntity> shaper)
    //    where TEntity : class
    //{
    //    var result = shaper(queryContext, keyPropertyValues, jsonElement);

    //    return result;
    //}

    //public static TResult MaterializeRootJsonEntityCollection<TEntity, TResult>(
    //    QueryContext queryContext,
    //    JsonElement jsonElement,
    //    object[] keyPropertyValues,
    //    INavigationBase navigation,
    //    Func<QueryContext, object[], JsonElement, TEntity> elementShaper)
    //    where TEntity : class
    //    where TResult : ICollection<TEntity>
    //{
    //    var collectionAccessor = navigation.GetCollectionAccessor();
    //    var result = (TResult)collectionAccessor!.Create();

    //    var newKeyPropertyValues = new object[keyPropertyValues.Length + 1];
    //    Array.Copy(keyPropertyValues, newKeyPropertyValues, keyPropertyValues.Length);

    //    var i = 0;
    //    foreach (var jsonArrayElement in jsonElement.EnumerateArray())
    //    {
    //        newKeyPropertyValues[^1] = ++i;

    //        var resultElement = elementShaper(queryContext, newKeyPropertyValues, jsonArrayElement);

    //        result.Add(resultElement);
    //    }

    //    return result;
    //}


    //private sealed class JsonCollectionResultInternalExpression : Expression
    //{
    //    private readonly Type _type;

    //    public JsonCollectionResultInternalExpression(
    //        JsonValueBufferExpression valueBufferExpression,
    //        INavigationBase? navigation,
    //        Type elementType,
    //        Type type)
    //    {
    //        ValueBufferExpression = valueBufferExpression;
    //        Navigation = navigation;
    //        ElementType = elementType;
    //        _type = type;
    //    }

    //    public JsonValueBufferExpression ValueBufferExpression { get; }

    //    public INavigationBase? Navigation { get; }

    //    public Type ElementType { get; }

    //    public override Type Type => _type;
    //}

    //private sealed class JsonValueBufferExpression : Expression
    //{
    //    public JsonValueBufferExpression(
    //        ParameterExpression keyValuesParameter,
    //        ParameterExpression jsonElementParameter,
    //        Expression entityExpression,
    //        INavigationBase? navigation)
    //    {
    //        KeyValuesParameter = keyValuesParameter;
    //        JsonElementParameter = jsonElementParameter;
    //        EntityExpression = entityExpression;
    //        Navigation = navigation;
    //    }

    //    public ParameterExpression KeyValuesParameter { get; }
    //    public ParameterExpression JsonElementParameter { get; }
    //    public Expression EntityExpression { get; }
    //    public INavigationBase? Navigation { get; }

    //    public override Type Type => typeof(ValueBuffer);
    //}
}
