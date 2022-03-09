// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using Microsoft.EntityFrameworkCore.TestModels.JsonQuery;

namespace Microsoft.EntityFrameworkCore.Query;

public class JsonQuerySqlServerFixture : JsonQueryFixtureBase
{
    protected override ITestStoreFactory TestStoreFactory
        => SqlServerTestStoreFactory.Instance;

    protected override void Seed(JsonQueryContext context)
    {
        base.Seed(context);

        context.Database.ExecuteSqlRaw("DROP TABLE [dbo].[JsonEntitiesBasic]");

        context.Database.ExecuteSqlRaw(
            @"CREATE TABLE [dbo].[JsonEntitiesBasic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[json_collection_shared] [nvarchar](max) NULL,
	[json_reference_shared] [nvarchar](max) NULL,
 CONSTRAINT [PK_JsonEntitiesBasic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))");

        var jsonEntitiesBasic = JsonQueryData.CreateJsonEntitiesBasic();
        foreach (var jsonEntityBasic in jsonEntitiesBasic)
        {
            var jsonReference = JsonSerializer.Serialize(jsonEntityBasic.OwnedReferenceSharedRoot);
            var jsonCollection = JsonSerializer.Serialize(jsonEntityBasic.OwnedCollectionSharedRoot);

            var sql = $@"INSERT INTO [dbo].[JsonEntitiesBasic]
           ([Name]
           ,[json_collection_shared]
           ,[json_reference_shared])
     VALUES
           ('{jsonEntityBasic.Name}'
           ,'{jsonCollection}'
           ,'{jsonReference}')";

            context.Database.ExecuteSqlRaw(sql.Replace("{", "{{").Replace("}", "}}"));
        }

        context.Database.ExecuteSqlRaw("DROP TABLE [dbo].[JsonEntitiesCustomNaming]");

        context.Database.ExecuteSqlRaw(
            @"CREATE TABLE [dbo].[JsonEntitiesCustomNaming](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[json_collection_custom_naming] [nvarchar](max) NULL,
	[json_reference_custom_naming] [nvarchar](max) NULL,
 CONSTRAINT [PK_JsonEntitiesCustomNaming] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))");

        var jsonEntitiesCustomNaming = JsonQueryData.CreateJsonEntitiesCustomNaming();
        foreach (var jsonEntityCustomNaming in jsonEntitiesCustomNaming)
        {
            var jsonReference = JsonSerializer.Serialize(jsonEntityCustomNaming.OwnedReferenceRoot);
            var jsonCollection = JsonSerializer.Serialize(jsonEntityCustomNaming.OwnedCollectionRoot);

            var sql = $@"INSERT INTO [dbo].[JsonEntitiesCustomNaming]
           ([Title]
           ,[json_collection_custom_naming]
           ,[json_reference_custom_naming])
     VALUES
           ('{jsonEntityCustomNaming.Title}'
           ,'{AddCustomNaming(jsonCollection)}'
           ,'{AddCustomNaming(jsonReference)}')";

            context.Database.ExecuteSqlRaw(sql.Replace("{", "{{").Replace("}", "}}"));
        }

        context.Database.ExecuteSqlRaw("DROP TABLE [dbo].[JsonEntitiesSingleOwned]");

        context.Database.ExecuteSqlRaw(
            @"CREATE TABLE [dbo].[JsonEntitiesSingleOwned](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[json_collection] [nvarchar](max) NULL,
 CONSTRAINT [PK_JsonEntitiesSingleOwned] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))");

        var jsonEntitiesSingleOwned = JsonQueryData.CreateJsonEntitiesSingleOwned();
        foreach (var jsonEntitySingleOwned in jsonEntitiesSingleOwned)
        {
            var jsonCollection = JsonSerializer.Serialize(jsonEntitySingleOwned.OwnedCollection);

            var sql = $@"INSERT INTO [dbo].[JsonEntitiesSingleOwned]
           ([Name]
           ,[json_collection])
     VALUES
           ('{jsonEntitySingleOwned.Name}'
           ,'{AddCustomNaming(jsonCollection)}')";

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
