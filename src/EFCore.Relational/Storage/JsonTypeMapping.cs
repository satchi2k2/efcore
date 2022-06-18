// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data;
using System.Text.Json;

namespace Microsoft.EntityFrameworkCore.Storage
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class JsonTypeMapping : RelationalTypeMapping
    {
        /// <summary>
        /// TODO
        /// </summary>
        protected JsonTypeMapping(string storeType, Type clrType, DbType? dbType)
            : base(storeType, clrType, dbType)
        {
        }

        /// <summary>
        /// TODO
        /// </summary>
        protected JsonTypeMapping(RelationalTypeMappingParameters parameters)
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

        /// <summary>
        ///     Generates the SQL representation of a literal value.
        /// </summary>
        /// <param name="value">The literal value.</param>
        /// <returns>
        ///     The generated json string.
        /// </returns>
        protected override string GenerateNonNullSqlLiteral(object value)
            => $"'{EscapeSqlLiteral((string)value)}'";

        ///// <summary>
        ///// TODO
        ///// </summary>
        //public override Expression CustomizeDataReaderExpression(Expression expression)
        //{
        //    if (expression is UnaryExpression unary
        //        && unary.NodeType == ExpressionType.Convert)
        //    {
        //        var parse = Expression.Call(
        //            _jsonDocumentParseMethod,
        //            Expression.Convert(
        //                unary.Operand,
        //                typeof(string)),
        //            Expression.Default(typeof(JsonDocumentOptions)));

        //        return Expression.Property(parse, _jsonDocumentRootElementProperty);
        //    }

        //    return base.CustomizeDataReaderExpression(expression);
        //}
    }
}
