// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Extensions
{
    /// <summary>
    /// TODO
    /// </summary>
    public static class RelationalOwnedNavigationBuilderExtensions
    {
        /// <summary>
        /// TODO
        /// </summary>
        public static OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> ToJson<TOwnerEntity, TDependentEntity>(
            this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> builder,
            string jsonColumnName)
            where TOwnerEntity : class
            where TDependentEntity : class
        {
            builder.OwnedEntityType.SetAnnotation(RelationalAnnotationNames.MapToJsonColumnName, jsonColumnName);

            return builder;
        }
    }
}
