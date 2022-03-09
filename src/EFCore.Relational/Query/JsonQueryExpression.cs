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
        private readonly IReadOnlyDictionary<IProperty, ColumnExpression> _keyPropertyMap;
        private readonly Type _type;

        /// <summary>
        /// TODO
        /// </summary>
        public JsonQueryExpression(
            IEntityType entityType,
            ColumnExpression jsonColumn,
            bool collection,
            IReadOnlyDictionary<IProperty, ColumnExpression> keyPropertyMap,
            Type type)
            : this(entityType, jsonColumn, collection, keyPropertyMap, type, new List<string>())
        {
        }

        private JsonQueryExpression(
            IEntityType entityType,
            ColumnExpression jsonColumn,
            bool collection,
            IReadOnlyDictionary<IProperty, ColumnExpression> keyPropertyMap,
            Type type,
            IReadOnlyList<string> jsonPath)
        {
            Check.DebugAssert(entityType.FindPrimaryKey() != null, "primary key is null.");

            EntityType = entityType;
            JsonColumn = jsonColumn;
            IsCollection = collection;
            _keyPropertyMap = keyPropertyMap;
            _type = type;
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
        public virtual bool IsCollection { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public virtual IReadOnlyList<string> JsonPath { get; }

        /// <inheritdoc />
        public override ExpressionType NodeType => ExpressionType.Extension;

        /// <inheritdoc />
        public override Type Type => _type;

        /// <summary>
        /// TODO
        /// </summary>
        public virtual SqlExpression BindProperty(IProperty property)
        {
            if (!EntityType.IsAssignableFrom(property.DeclaringEntityType)
                && !property.DeclaringEntityType.IsAssignableFrom(EntityType))
            {
                throw new InvalidOperationException(
                    RelationalStrings.UnableToBindMemberToEntityProjection("property", property.Name, EntityType.DisplayName()));
            }

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
            var sourcePrimaryKeyProperties = EntityType.FindPrimaryKey()!.Properties.Take(_keyPropertyMap.Count).ToList();
            var targetPrimaryKeyProperties = targetEntityType.FindPrimaryKey()!.Properties.Take(_keyPropertyMap.Count).ToList();
            for (var i = 0; i < sourcePrimaryKeyProperties.Count; i++)
            {
                newKeyPropertyMap[targetPrimaryKeyProperties[i]] = _keyPropertyMap[sourcePrimaryKeyProperties[i]];
            }

            //var newKeyPropertyMap = new Dictionary<IProperty, ColumnExpression>();
            //var foreignKeyProperties = navigation.ForeignKey.Properties;
            //var primaryKeyProperties = EntityType.FindPrimaryKey()!.Properties;

            //foreach (var (property, column) in _keyPropertyMap)
            //{
            //    var keyIndex = primaryKeyProperties.IndexOf(property);
            //    newKeyPropertyMap[foreignKeyProperties[keyIndex]] = column;
            //}

            return new JsonQueryExpression(targetEntityType, JsonColumn, navigation.IsCollection, newKeyPropertyMap, navigation.ClrType, newJsonPath);
        }

        /// <inheritdoc />
        public virtual void Print(ExpressionPrinter expressionPrinter)
        {
            expressionPrinter.Append("JsonQueryExpression(");
            expressionPrinter.Visit(JsonColumn);
            expressionPrinter.Append($", \"{string.Join(".", JsonPath)}\")");
        }

        /// <inheritdoc />
        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            // TODO: implement proper visit children?
            return this;
        }

        /// <summary>
        /// TODO
        /// </summary>
        public virtual JsonQueryExpression Update(ColumnExpression jsonColumn, IReadOnlyDictionary<IProperty, ColumnExpression> keyPropertyMap, IReadOnlyList<string> jsonPath)
            => jsonColumn != JsonColumn
            || keyPropertyMap.Count != _keyPropertyMap.Count
            || keyPropertyMap.Zip(_keyPropertyMap, (n, o) => n.Value != o.Value).Any(x => x)
            || !jsonPath.SequenceEqual(JsonPath)
                ? new JsonQueryExpression(EntityType, jsonColumn, IsCollection, keyPropertyMap, _type, jsonPath)
                : this;
    }
}
