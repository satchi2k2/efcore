// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data;

namespace Microsoft.EntityFrameworkCore.Metadata.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public class InternalCurrentValuePropertyStoredProcedureParameterBuilder :
    AnnotatableBuilder<CurrentValuePropertyStoredProcedureParameter, IConventionModelBuilder>,
    IConventionStoredProcedureParameterBuilder
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public InternalCurrentValuePropertyStoredProcedureParameterBuilder(
        CurrentValuePropertyStoredProcedureParameter parameter, IConventionModelBuilder modelBuilder)
        : base(parameter, modelBuilder)
    {
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual InternalCurrentValuePropertyStoredProcedureParameterBuilder? HasName(
        string name,
        ConfigurationSource configurationSource)
    {
        if (!CanSetName(name, configurationSource))
        {
            return null;
        }

        Metadata.SetName(name, configurationSource);
        
        return this;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual bool CanSetName(
        string? name,
        ConfigurationSource configurationSource)
        => configurationSource.Overrides(Metadata.GetNameConfigurationSource())
            || Metadata.Name == name;
    
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual InternalCurrentValuePropertyStoredProcedureParameterBuilder? HasDirection(
        ParameterDirection direction,
        ConfigurationSource configurationSource)
    {
        if (!CanSetDirection(direction, configurationSource))
        {
            return null;
        }

        Metadata.SetDirection(direction, configurationSource);
        
        return this;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual bool CanSetDirection(
        ParameterDirection direction,
        ConfigurationSource configurationSource)
        => configurationSource == ConfigurationSource.Explicit
            || Metadata.Direction == direction
            || (configurationSource.Overrides(Metadata.GetDirectionConfigurationSource()) && Metadata.IsValid(direction));

    /// <inheritdoc />
    IConventionStoredProcedureParameter IConventionStoredProcedureParameterBuilder.Metadata
    {
        [DebuggerStepThrough]
        get => Metadata;
    }
}
