// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Microsoft.EntityFrameworkCore.Metadata;

/// <summary>
///     Represents a stored procedure in a model in
///     the form that can be mutated while the model is being built.
/// </summary>
public interface IConventionStoredProcedure : IReadOnlyStoredProcedure, IConventionAnnotatable
{
    /// <summary>
    ///     Gets the entity type in which this stored procedure is defined.
    /// </summary>
    new IConventionEntityType EntityType { get; }

    /// <summary>
    ///     Gets the builder that can be used to configure this stored procedure.
    /// </summary>
    /// <exception cref="InvalidOperationException">If the function has been removed from the model.</exception>
    new IConventionStoredProcedureBuilder Builder { get; }

    /// <summary>
    ///     Gets the configuration source for this stored procedure.
    /// </summary>
    /// <returns>The configuration source for this function.</returns>
    ConfigurationSource GetConfigurationSource();

    /// <summary>
    ///     Sets the name of the stored procedure in the database.
    /// </summary>
    /// <param name="name">The name of the function in the database.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The configured value.</returns>
    string? SetName(string? name, bool fromDataAnnotation = false);

    /// <summary>
    ///     Gets the configuration source for <see cref="IReadOnlyStoredProcedure.Name" />.
    /// </summary>
    /// <returns>The configuration source for <see cref="IReadOnlyStoredProcedure.Name" />.</returns>
    ConfigurationSource? GetNameConfigurationSource();

    /// <summary>
    ///     Sets the schema of the stored procedure in the database.
    /// </summary>
    /// <param name="schema">The schema of the function in the database.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The configured value.</returns>
    string? SetSchema(string? schema, bool fromDataAnnotation = false);

    /// <summary>
    ///     Gets the configuration source for <see cref="IReadOnlyStoredProcedure.Schema" />.
    /// </summary>
    /// <returns>The configuration source for <see cref="IReadOnlyStoredProcedure.Schema" />.</returns>
    ConfigurationSource? GetSchemaConfigurationSource();

    /// <summary>
    ///     Gets the parameters for this stored procedure.
    /// </summary>
    new IReadOnlyList<IConventionStoredProcedureParameter> Parameters { get; }

    /// <summary>
    ///     Adds a new parameter mapped to the property with the given name.
    /// </summary>
    /// <param name="propertyName">The name of the corresponding property.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The added parameter.</returns>
    IConventionStoredProcedureParameter? AddParameter(string propertyName, bool fromDataAnnotation = false);

    /// <summary>
    ///     Adds a new parameter that will hold the original value of the property with the given name.
    /// </summary>
    /// <param name="propertyName">The name of the corresponding property.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The added parameter.</returns>
    IConventionStoredProcedureParameter? AddOriginalValueParameter(string propertyName, bool fromDataAnnotation = false);

    /// <summary>
    ///     Adds an output parameter that returns the rows affected by this stored procedure.
    /// </summary>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The added parameter.</returns>
    IConventionStoredProcedureParameter? AddRowsAffectedParameter(string parameterName, bool fromDataAnnotation = false);

    /// <summary>
    ///     Gets the columns of the result for this stored procedure.
    /// </summary>
    new IReadOnlyList<IConventionStoredProcedureResultColumn> ResultColumns { get; }

    /// <summary>
    ///     Adds a new column of the result for this stored procedure mapped to the property with the given name
    /// </summary>
    /// <param name="propertyName">The name of the corresponding property.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The added column.</returns>
    IConventionStoredProcedureResultColumn? AddResultColumn(string propertyName, bool fromDataAnnotation = false);

    /// <summary>
    ///     Adds a new column of the result that contains the rows affected by this stored procedure.
    /// </summary>
    /// <param name="columnName">The name of the column.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The added column.</returns>
    IConventionStoredProcedureResultColumn? AddRowsAffectedResultColumn(string columnName, bool fromDataAnnotation = false);

    /// <summary>
    ///     Prevents automatically creating a transaction when executing this stored procedure.
    /// </summary>
    /// <param name="areTransactionsSuppressed">A value indicating whether the automatic transactions should be prevented.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The configured value.</returns>
    bool SetAreTransactionsSuppressed(bool areTransactionsSuppressed, bool fromDataAnnotation = false);

    /// <summary>
    ///     Gets the configuration source for <see cref="IReadOnlyStoredProcedure.Schema" />.
    /// </summary>
    /// <returns>The configuration source for <see cref="IReadOnlyStoredProcedure.Schema" />.</returns>
    ConfigurationSource? GetAreTransactionsSuppressedConfigurationSource();
}
