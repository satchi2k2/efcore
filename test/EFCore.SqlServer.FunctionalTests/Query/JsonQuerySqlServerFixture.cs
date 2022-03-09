// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Reflection.Emit;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.TestModels.JsonQuery;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.EntityFrameworkCore.Query;

public class JsonQuerySqlServerFixture : JsonQueryFixtureBase
{
    protected override ITestStoreFactory TestStoreFactory
        => SqlServerTestStoreFactory.Instance;

    protected override void Seed(JsonQueryContext context)
    {
        base.Seed(context);

        context.Database.ExecuteSqlRaw("DROP TABLE [dbo].[JsonBasicEntities]");

        context.Database.ExecuteSqlRaw(
            @"CREATE TABLE [dbo].[JsonBasicEntities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[json_collection_shared] [nvarchar](max) NULL,
	[json_reference_shared] [nvarchar](max) NULL,
 CONSTRAINT [PK_JsonBasicEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))");

        var jsonBasicEntities = JsonQueryData.CreateJsonBasicEntities();
        foreach (var jsonBasicEntity in jsonBasicEntities)
        {
            var jsonReference = JsonSerializer.Serialize(jsonBasicEntity.OwnedReferenceSharedRoot);
            var jsonCollection = JsonSerializer.Serialize(jsonBasicEntity.OwnedCollectionSharedRoot);

            var sql = $@"INSERT INTO [dbo].[JsonBasicEntities]
           ([Name]
           ,[json_collection_shared]
           ,[json_reference_shared])
     VALUES
           ('JsonBasicEntity1'
           ,'{jsonCollection}'
           ,'{jsonReference}')";

            context.Database.ExecuteSqlRaw(sql.Replace("{", "{{").Replace("}", "}}"));
        }

        context.Database.ExecuteSqlRaw("DROP TABLE [dbo].[JsonCustomNamingEntities]");

        context.Database.ExecuteSqlRaw(
            @"CREATE TABLE [dbo].[JsonCustomNamingEntities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[json_collection_custom_naming] [nvarchar](max) NULL,
	[json_reference_custom_naming] [nvarchar](max) NULL,
 CONSTRAINT [PK_JsonCustomNamingEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))");

        var jsonCustomNamingEntities = JsonQueryData.CreateJsonCustomNamingEntities();
        foreach (var jsonCustomNamingEntity in jsonCustomNamingEntities)
        {
            var jsonReference = JsonSerializer.Serialize(jsonCustomNamingEntity.OwnedReferenceRoot);
            var jsonCollection = JsonSerializer.Serialize(jsonCustomNamingEntity.OwnedCollectionRoot);

            var sql = $@"INSERT INTO [dbo].[JsonCustomNamingEntities]
           ([Title]
           ,[json_collection_custom_naming]
           ,[json_reference_custom_naming])
     VALUES
           ('JsonCustomNamingEntity1'
           ,'{AddCustomNaming(jsonCollection)}'
           ,'{AddCustomNaming(jsonReference)}')";

            context.Database.ExecuteSqlRaw(sql.Replace("{", "{{").Replace("}", "}}"));
        }

        static string AddCustomNaming(string json)
            => json;
            //.Replace("OwnedReferenceRoot", "CustomOwnedReferenceRoot")
            //.Replace("OwnedCollectionRoot", "CustomOwnedCollectionRoot")
            //.Replace("Name", "CustomName")
            //.Replace("Number", "CustomNumber")
            //.Replace("OwnedReferenceBranch", "CustomOwnedReferenceBranch")
            //.Replace("OwnedCollectionBranch", "CustomOwnedCollectionBranch")
            //.Replace("Date", "CustomDate")
            //.Replace("Fraction", "CustomFraction");
    }
}
