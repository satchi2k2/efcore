// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public class RuntimeStoredProcedure : AnnotatableBase, IRuntimeStoredProcedure
{
    private readonly List<RuntimeStoredProcedureParameter> _parameters = new();
    private readonly List<RuntimeStoredProcedureResultColumn> _resultColumns = new();
    private readonly string? _schema;
    private readonly string _name;
    private readonly bool _areTransactionsSuppressed;
    private IStoreStoredProcedure? _storeStoredProcedure;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RuntimeStoredProcedure" /> class.
    /// </summary>
    /// <param name="entityType">The mapped entity type.</param>
    /// <param name="name">The name.</param>
    /// <param name="schema">The schema.</param>
    /// <param name="areTransactionsSuppressed">Whether the automatic transactions are surpressed.</param>
    public RuntimeStoredProcedure(
        RuntimeEntityType entityType,
        string name,
        string? schema,
        bool areTransactionsSuppressed)
    {
        EntityType = entityType;
        _name = name;
        _schema = schema;
        _areTransactionsSuppressed = areTransactionsSuppressed;
    }

    /// <summary>
    ///     Gets the entity type in which this stored procedure is defined.
    /// </summary>
    public virtual RuntimeEntityType EntityType { get; set; }

    /// <summary>
    ///     Adds a new parameter mapped to the property with the given name.
    /// </summary>
    /// <param name="propertyName">The name of the corresponding property.</param>
    public virtual RuntimeStoredProcedureParameter AddParameter(string propertyName)
    {
        _parameters.Add(propertyName);
    }

    /// <summary>
    ///     Adds a new parameter that will hold the original value of the property with the given name.
    /// </summary>
    /// <param name="propertyName">The name of the corresponding property.</param>
    /// <returns>The added parameter.</returns>
    public virtual RuntimeStoredProcedureParameter AddOriginalValueParameter(string propertyName);

    /// <summary>
    ///     Adds an output parameter that returns the rows affected by this stored procedure.
    /// </summary>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <returns>The added parameter.</returns>
    public virtual RuntimeStoredProcedureParameter AddRowsAffectedParameter(string parameterName);

    /// <summary>
    ///     Adds a new column of the result for this stored procedure mapped to the property with the given name
    /// </summary>
    /// <param name="propertyName">The name of the corresponding property.</param>
    public virtual RuntimeStoredProcedureResultColumn AddResultColumn(string propertyName)
    {
        _resultColumns.Add(propertyName);
    }

    /// <summary>
    ///     Adds a new column of the result that contains the rows affected by this stored procedure.
    /// </summary>
    /// <param name="columnName">The name of the column.</param>
    /// <returns>The added column.</returns>
    public virtual RuntimeStoredProcedureResultColumn AddRowsAffectedResultColumn(string columnName);

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public override string ToString()
        => ((IStoredProcedure)this).ToDebugString(MetadataDebugStringOptions.SingleLineDefault);

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [EntityFrameworkInternal]
    public virtual DebugView DebugView
        => new(
            () => ((IStoredProcedure)this).ToDebugString(),
            () => ((IStoredProcedure)this).ToDebugString(MetadataDebugStringOptions.LongDefault));

    /// <inheritdoc />
    IReadOnlyEntityType IReadOnlyStoredProcedure.EntityType
    {
        [DebuggerStepThrough]
        get => EntityType;
    }

    /// <inheritdoc />
    IEntityType IStoredProcedure.EntityType
    {
        [DebuggerStepThrough]
        get => EntityType;
    }

    /// <inheritdoc />
    string? IReadOnlyStoredProcedure.Name
    {
        [DebuggerStepThrough]
        get => _name;
    }
    
    /// <inheritdoc />
    string IStoredProcedure.Name
    {
        [DebuggerStepThrough]
        get => _name;
    }

    /// <inheritdoc />
    string? IReadOnlyStoredProcedure.Schema
    {
        [DebuggerStepThrough]
        get => _schema;
    }

    /// <inheritdoc />
    bool IReadOnlyStoredProcedure.AreTransactionsSuppressed
    {
        [DebuggerStepThrough]
        get => _areTransactionsSuppressed;
    }

    /// <inheritdoc />
    IReadOnlyList<IReadOnlyStoredProcedureParameter> IReadOnlyStoredProcedure.Parameters
    {
        [DebuggerStepThrough]
        get => _parameters;
    }

    /// <inheritdoc />
    IReadOnlyList<IReadOnlyStoredProcedureResultColumn> IReadOnlyStoredProcedure.ResultColumns
    {
        [DebuggerStepThrough]
        get => _resultColumns;
    }

    /// <inheritdoc />
    bool IReadOnlyStoredProcedure.FindParameter(string propertyName)
        => _parameters.Contains(propertyName);

    /// <inheritdoc />
    bool IReadOnlyStoredProcedure.FindResultColumn(string propertyName)
        => _resultColumns.Contains(propertyName);
    
    /// <inheritdoc />
    IStoreStoredProcedure IStoredProcedure.StoreStoredProcedure
    {
        [DebuggerStepThrough]
        get => _storeStoredProcedure!;
    }
    
    /// <inheritdoc />
    IStoreStoredProcedure IRuntimeStoredProcedure.StoreStoredProcedure
    {
        get => _storeStoredProcedure!;
        set => _storeStoredProcedure = value;
    }
}
