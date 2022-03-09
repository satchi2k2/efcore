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

        var jsonEntitiesInheritance = JsonQueryData.CreateJsonEntitiesInheritance();
        foreach (var jsonEntityInheritance in jsonEntitiesInheritance)
        {
            var jsonReferenceOnBase = JsonSerializer.Serialize(jsonEntityInheritance.ReferenceOnBase);
            var jsonCollectionOnBase = JsonSerializer.Serialize(jsonEntityInheritance.CollectionOnBase);

            var jsonReferenceOnDerived = default(string);
            var jsonCollectionOnDerived = default(string);

            var values = $@"
           ('{jsonEntityInheritance.Name}'
           ,NULL
           ,'JsonEntityInheritanceBase'
           ,'{jsonReferenceOnBase}'
           ,'{jsonCollectionOnBase}'
           ,NULL
           ,NULL)";

            if (jsonEntityInheritance is JsonEntityInheritanceDerived derived)
            {
                //                fraction = derived.Fraction;
                jsonReferenceOnDerived = JsonSerializer.Serialize(derived.ReferenceOnDerived);
                jsonCollectionOnDerived = JsonSerializer.Serialize(derived.CollectionOnDerived);

                values = $@"
    ('{jsonEntityInheritance.Name}'
    ,{derived.Fraction}
    ,'JsonEntityInheritanceDerived'
    ,'{jsonReferenceOnBase}'
    ,'{jsonCollectionOnBase}'
    ,'{jsonReferenceOnDerived}'
    ,'{jsonCollectionOnDerived}')";
            }

            var sql = $@"INSERT INTO [dbo].[JsonEntityInheritanceBase]
    ([Name]
    ,[Fraction]
    ,[Discriminator]
    ,[reference_on_base]
    ,[collection_on_base]
    ,[reference_on_derived]
    ,[collection_on_derived])
VALUES" + values;

            context.Database.ExecuteSqlRaw(sql.Replace("{", "{{").Replace("}", "}}"));
        }

        static string AddCustomNaming(string json)
            => json;
    }
}
