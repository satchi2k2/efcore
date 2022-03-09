// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.TestModels.JsonQuery
{
    public class JsonQueryContext : DbContext
    {
        public JsonQueryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<JsonEntityBasic> JsonEntitiesBasic { get; set; }
        public DbSet<JsonEntityCustomNaming> JsonEntitiesCustomNaming { get; set; }
        public DbSet<JsonEntitySingleOwned> JsonEntitiesSingleOwned { get; set; }

        public static void Seed(JsonQueryContext context)
        {
            // TODO: implement update
            //var jsonBasicEntities = JsonQueryData.CreateJsonBasicEntities();

            //context.JsonBasicEntities.AddRange(jsonBasicEntities);
            //context.SaveChanges();
        }
    }
}
