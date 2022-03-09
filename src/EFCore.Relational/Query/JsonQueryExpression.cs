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
        private readonly IDictionary<IProperty, ColumnExpression> _keyPropertyMap;

        /// <summary>
        /// TODO
        /// </summary>
        public JsonQueryExpression(
            IEntityType entityType,
            ColumnExpression jsonColumn,
            INavigation navigation,
            IDictionary<IProperty, ColumnExpression> keyPropertyMap)
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
            IDictionary<IProperty, ColumnExpression> keyPropertyMap,
            List<string> jsonPath)
        {
            Check.DebugAssert(entityType.FindPrimaryKey() != null, "primary key is null.");

            EntityType = entityType;
            JsonColumn = jsonColumn;
            Navigation = navigation;
            _keyPropertyMap = keyPropertyMap;
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
            if (_keyPropertyMap.TryGetValue(property, out var match))
            {
                return match;
            }

            var pathSegment = property.GetJsonElementName();
            var newPath = JsonPath.ToList();
            newPath.Add(pathSegment);

            var typeMapping = property.FindRelationalTypeMapping();
            if (typeMapping == null)
            {
                // TODO: resource string (or not even check this and just bang in the line below?)
                throw new InvalidOperationException("type mapping not found for property");
            }

            return new JsonScalarExpression(
                JsonColumn,
                property.ClrType,
                typeMapping,
                newPath);
        }

        /// <summary>
        /// TODO
        /// </summary>
        public virtual JsonQueryExpression BindNavigation(INavigation navigation)
        {
            var targetEntityType = navigation.TargetEntityType;

            var newJsonPath = JsonPath.ToList();
            newJsonPath.Add(navigation.GetJsonElementName());

            var newKeyPropertyMap = new Dictionary<IProperty, ColumnExpression>();
            var foreignKeyProperties = navigation.ForeignKey.Properties.ToList();
            var primaryKeyProperties = EntityType.FindPrimaryKey()!.Properties;

            foreach (var (property, column) in _keyPropertyMap)
            {
                var keyIndex = primaryKeyProperties.IndexOf(property);
                newKeyPropertyMap[foreignKeyProperties[keyIndex]] = column;
            }

            return new JsonQueryExpression(targetEntityType, JsonColumn, navigation, newKeyPropertyMap, newJsonPath);
        }

        /// <inheritdoc />
        public void Print(ExpressionPrinter expressionPrinter)
        {
            expressionPrinter.Append("JsonQueryExpression(");
            expressionPrinter.Visit(JsonColumn);
            expressionPrinter.Append($", \"{string.Join(".", JsonPath)}\")");
        }
    }
}
