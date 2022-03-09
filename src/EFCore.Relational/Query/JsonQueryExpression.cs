// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    /// TODO
    /// </summary>
    public class JsonQueryExpression : Expression, IPrintableExpression
    {
        /// <summary>
        /// TODO
        /// </summary>
        public JsonQueryExpression(
            IEntityType entityType,
            ColumnExpression jsonColumn,
            INavigation navigation,
            List<(IProperty, ColumnExpression)> keyPropertyMap)
            : this(entityType, jsonColumn, navigation, keyPropertyMap, new List<string>())
        {
        }

        /// <summary>
        /// TODO
        /// </summary>
        public JsonQueryExpression(
            IEntityType entityType,
            ColumnExpression jsonColumn,
            INavigation navigation,
            List<(IProperty, ColumnExpression)> keyPropertyMap,
            List<string> jsonPath)
        {
            // or just store type instead?
            EntityType = entityType;
            JsonColumn = jsonColumn;
            Navigation = navigation;
            KeyPropertyMap = keyPropertyMap;
            JsonPath = jsonPath;
        }

        /// <summary>
        ///     The entity type being projected out.
        /// </summary>
        public virtual IEntityType EntityType { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public virtual ColumnExpression JsonColumn { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public INavigation Navigation { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public virtual IReadOnlyList<(IProperty, ColumnExpression)> KeyPropertyMap { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public virtual IReadOnlyList<string> JsonPath { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public override ExpressionType NodeType => ExpressionType.Extension;

        /// <inheritdoc />
        public override Type Type => Navigation.ClrType;

        /// <summary>
        /// TODO
        /// </summary>
        public virtual SqlExpression BindProperty(IProperty property)
        {
            var match = KeyPropertyMap.Where(x => x.Item1 == property).FirstOrDefault().Item2;
            if (match != null)
            {
                return match;
            }

            var pathSegment = property.Name;
            var newPath = JsonPath.ToList();
            newPath.Add(pathSegment);

            return new JsonScalarExpression(
                JsonColumn,
                property.ClrType,
                property.FindRelationalTypeMapping(), // TODO: use column information we should have somewhere
                newPath);
        }

        /// <inheritdoc />
        public void Print(ExpressionPrinter expressionPrinter)
        {
            expressionPrinter.Append($"JsonQueryExpression({JsonColumn.Name}, \"{string.Join(".", JsonPath)}\")");
        }
    }
}
