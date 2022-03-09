// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Relational database specific extension methods for <see cref="NavigationBuilder" />.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and examples.
/// </remarks>
public static class RelationalNavigationBuilderExtensions
{
    /// <summary>
    /// TODO
    /// </summary>
    public static NavigationBuilder HasJsonElementName(
        this NavigationBuilder navigationBuilder,
        string? name)
    {
        Check.NullButNotEmpty(name, nameof(name));

        navigationBuilder.Metadata.SetJsonElementName(name);

        return navigationBuilder;
    }

    /// <summary>
    /// TODO
    /// </summary>
    public static NavigationBuilder<TSource, TTarget> HasJsonElementName<TSource, TTarget>(
        this NavigationBuilder<TSource, TTarget> navigationBuilder,
        string? name)
        where TSource : class
        where TTarget : class
        => (NavigationBuilder<TSource, TTarget>)HasJsonElementName((NavigationBuilder)navigationBuilder, name);

    /// <summary>
    /// TODO
    /// </summary>
    public static IConventionNavigationBuilder? HasJsonElementName(
        this IConventionNavigationBuilder navigationBuilder,
        string? name,
        bool fromDataAnnotation = false)
    {
        if (!navigationBuilder.CanSetJsonElementName(name, fromDataAnnotation))
        {
            return null;
        }

        navigationBuilder.Metadata.SetJsonElementName(name, fromDataAnnotation);

        return navigationBuilder;
    }

    /// <summary>
    /// TODO
    /// </summary>
    public static bool CanSetJsonElementName(
        this IConventionNavigationBuilder navigationBuilder,
        string? name,
        bool fromDataAnnotation = false)
        => navigationBuilder.CanSetAnnotation(RelationalAnnotationNames.JsonElementName, name, fromDataAnnotation);
}
