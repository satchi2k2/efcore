// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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
            if (name != RelationalAnnotationNames.MapToJsonColumnName)
            {
                return;
            }

            // TODO: if json type name was specified (via attribute) propagate it here and add it as annotation on the entity itself
            // needed for postgres, since it has two json types
            var jsonColumnName = annotation?.Value as string;
            if (!string.IsNullOrEmpty(jsonColumnName))
            {
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
                }
            }
            else
            {
                // TODO: unwind everything
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void ProcessNavigationAdded(
            IConventionNavigationBuilder navigationBuilder,
            IConventionContext<IConventionNavigationBuilder> context)
        {
            if (navigationBuilder.Metadata.DeclaringEntityType.MappedToJsonColumnName() is string jsonColumnName
                && navigationBuilder.Metadata.ForeignKey.IsOwnership)
            {
                navigationBuilder.Metadata.TargetEntityType.SetMappedToJsonColumnName(jsonColumnName);
            }
        }
    }
}
