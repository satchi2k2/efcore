// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// TODO
    /// </summary>
    public class RelationalMapToJsonConvention : IEntityTypeAnnotationChangedConvention, INavigationAddedConvention
    {
        /// <summary>
        ///     Creates a new instance of <see cref="RelationalMapToJsonConvention" />.
        /// </summary>
        /// <param name="dependencies">Parameter object containing dependencies for this convention.</param>
        /// <param name="relationalDependencies">Parameter object containing relational dependencies for this convention.</param>
        public RelationalMapToJsonConvention(
            ProviderConventionSetBuilderDependencies dependencies,
            RelationalConventionSetBuilderDependencies relationalDependencies)
        {
            Dependencies = dependencies;
            RelationalDependencies = relationalDependencies;
        }

        /// <summary>
        ///     Dependencies for this service.
        /// </summary>
        protected virtual ProviderConventionSetBuilderDependencies Dependencies { get; }

        /// <summary>
        ///     Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalConventionSetBuilderDependencies RelationalDependencies { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public virtual void ProcessEntityTypeAnnotationChanged(
            IConventionEntityTypeBuilder entityTypeBuilder,
            string name,
            IConventionAnnotation? annotation,
            IConventionAnnotation? oldAnnotation,
            IConventionContext<IConventionAnnotation> context)
        {
            if (name == RelationalAnnotationNames.MapToJsonColumnName)
            {
                // TODO: if json type name was specified (via attribute) propagate it here and add it as annotation on the entity itself
                // needed for postgres, since it has two json types
                var jsonColumnName = annotation?.Value as string;
                if (!string.IsNullOrEmpty(jsonColumnName))
                {
                    var jsonColumnTypeName = entityTypeBuilder.Metadata.MappedToJsonColumnTypeName();
                    var jsonColumnTypeMapping = RelationalDependencies.RelationalTypeMappingSource!.FindMapping(
                        typeof(JsonElement),
                        jsonColumnTypeName);

                    if (jsonColumnTypeMapping == null)
                    {
                        // TODO: debug.assert or bang?
                        throw new InvalidOperationException($"Couldn't find mapping for json column.");
                    }

                    entityTypeBuilder.Metadata.SetMapToJsonTypeMapping(jsonColumnTypeMapping);

                    foreach (var navigation in entityTypeBuilder.Metadata.GetDeclaredNavigations()
                        .Where(n => n.ForeignKey.IsOwnership
                            && n.DeclaringEntityType == entityTypeBuilder.Metadata
                            && n.TargetEntityType.IsOwned()))
                    {
                        var currentJsonColumnName = navigation.TargetEntityType.MappedToJsonColumnName();
                        if (currentJsonColumnName == null || currentJsonColumnName != jsonColumnName)
                        {
                            navigation.TargetEntityType.SetMappedToJsonColumnName(jsonColumnName);
                        }

                        var currentJsonColumnTypeName = navigation.TargetEntityType.MappedToJsonColumnTypeName();
                        if (jsonColumnTypeName != null && currentJsonColumnTypeName != jsonColumnTypeName)
                        {
                            navigation.TargetEntityType.SetMappedToJsonColumnTypeName(jsonColumnTypeName);
                        }

                        navigation.TargetEntityType.SetMapToJsonTypeMapping(jsonColumnTypeMapping);
                    }
                }
                else
                {
                    // TODO: unwind everything
                }
            }

            if (name == RelationalAnnotationNames.MapToJsonColumnTypeName)
            {
                var jsonColumnTypeName = annotation?.Value as string;
                if (!string.IsNullOrEmpty(jsonColumnTypeName))
                {
                    foreach (var navigation in entityTypeBuilder.Metadata.GetDeclaredNavigations()
                        .Where(n => n.ForeignKey.IsOwnership
                            && n.DeclaringEntityType == entityTypeBuilder.Metadata
                            && n.TargetEntityType.IsOwned()))
                    {
                        var currentJsonColumnTypeName = navigation.TargetEntityType.MappedToJsonColumnTypeName();
                        if (currentJsonColumnTypeName == null || currentJsonColumnTypeName != jsonColumnTypeName)
                        {
                            navigation.TargetEntityType.SetMappedToJsonColumnTypeName(jsonColumnTypeName);
                            var jsonColumnTypeMapping = RelationalDependencies.RelationalTypeMappingSource!.FindMapping(
                                typeof(JsonElement),
                                jsonColumnTypeName);

                            if (jsonColumnTypeMapping == null)
                            {
                                throw new InvalidOperationException("Couldn't find mapping for json column.");
                            }

                            navigation.TargetEntityType.SetMapToJsonTypeMapping(jsonColumnTypeMapping);
                        }
                    }
                }
                else
                {
                    // undo
                }
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void ProcessNavigationAdded(
            IConventionNavigationBuilder navigationBuilder,
            IConventionContext<IConventionNavigationBuilder> context)
        {
            if (navigationBuilder.Metadata.ForeignKey.IsOwnership)
            {
                if (navigationBuilder.Metadata.DeclaringEntityType.MappedToJsonColumnName() is string jsonColumnName)
                {
                    navigationBuilder.Metadata.TargetEntityType.SetMappedToJsonColumnName(jsonColumnName);
                }

                if (navigationBuilder.Metadata.DeclaringEntityType.MappedToJsonColumnTypeName() is string jsonColumnTypeName)
                {
                    navigationBuilder.Metadata.TargetEntityType.SetMappedToJsonColumnTypeName(jsonColumnTypeName);
                }

                if (navigationBuilder.Metadata.DeclaringEntityType.MapToJsonTypeMapping() is RelationalTypeMapping jsonColumnTypeMapping)
                {
                    navigationBuilder.Metadata.TargetEntityType.SetMapToJsonTypeMapping(jsonColumnTypeMapping);
                }
            }
        }
    }
}
