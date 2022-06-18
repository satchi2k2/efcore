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

        context.Database.ExecuteSqlRaw("DROP TABLE [dbo].[JsonBasicEntities]");

        context.Database.ExecuteSqlRaw(
            @"CREATE TABLE [dbo].[JsonBasicEntities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[json_collection_shared] [nvarchar](max) NULL,
	[json_reference_shared] [nvarchar](max) NULL,
 CONSTRAINT [PK_MyEntities] PRIMARY KEY CLUSTERED 
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
    }
}
