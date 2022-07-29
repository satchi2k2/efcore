﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal
{
    /// <summary>
    /// TODO
    /// </summary>
    public class SqlServerJsonTypeMapping : JsonTypeMapping
    {
        private static readonly MethodInfo _getStringMethod
            = typeof(DbDataReader).GetRuntimeMethod(nameof(DbDataReader.GetString), new[] { typeof(int) })!;

        private static readonly MethodInfo _jsonDocumentParseMethod
            = typeof(JsonDocument).GetRuntimeMethod(nameof(JsonDocument.Parse), new[] { typeof(string), typeof(JsonDocumentOptions) })!;

        private static readonly MemberInfo _jsonDocumentRootElementMember
            = typeof(JsonDocument).GetRuntimeProperty(nameof(JsonDocument.RootElement))!;

        /// <summary>
        /// TODO
        /// </summary>
        public SqlServerJsonTypeMapping(string storeType)
            : base(storeType, typeof(JsonElement), System.Data.DbType.String)
        {
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public override MethodInfo GetDataReaderMethod()
            => _getStringMethod;

        /// <summary>
        /// TODO
        /// </summary>
        public override Expression CustomizeDataReaderExpression(Expression expression)
        {
            if (expression is UnaryExpression unary
                && unary.NodeType == ExpressionType.Convert)
            {
                var parse = Expression.Call(
                    _jsonDocumentParseMethod,
                    Expression.Convert(
                        unary.Operand,
                        typeof(string)),
                    Expression.Default(typeof(JsonDocumentOptions)));

                return Expression.MakeMemberAccess(parse, _jsonDocumentRootElementMember);
            }

            return base.CustomizeDataReaderExpression(expression);
        }

        /// <summary>
        /// TODO
        /// </summary>
        protected SqlServerJsonTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        /// <summary>
        ///     Generates the escaped SQL representation of a literal value.
        /// </summary>
        /// <param name="literal">The value to be escaped.</param>
        /// <returns>
        ///     The generated json string.
        /// </returns>
        protected virtual string EscapeSqlLiteral(string literal)
            => literal.Replace("'", "''");

        /// <inheritdoc />
        protected override string GenerateNonNullSqlLiteral(object value)
            // TODO: flow serializer options somehow
            => $"'{EscapeSqlLiteral(JsonSerializer.Serialize(value))}'";

        /// <summary>
        /// TODO
        /// </summary>
        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new SqlServerJsonTypeMapping(parameters);
    }
}
