// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.ModelBuilding
{
    public partial class RelationalModelBuilderTest
    {
        public abstract class RelationalJsonTestBase : ModelBuilderTestBase
        {
            [ConditionalFact]
            public virtual void Json_entity_and_normal_owned_can_exist_side_to_side_on_same_entity()
            {
                var modelBuilder = CreateModelBuilder();

                modelBuilder.Entity<JsonEntity>(b =>
                {
                    b.OwnsOne(x => x.OwnedReference1);
                    b.OwnsOne(x => x.OwnedReference2, bb => bb.ToJson("reference"));
                    b.OwnsMany(x => x.OwnedCollection1);
                    b.OwnsMany(x => x.OwnedCollection2, bb => bb.ToJson("collection"));
                });

                var model = modelBuilder.FinalizeModel();

                var ownedEntities = model.FindEntityTypes(typeof(OwnedEntity));
                Assert.Equal(4, ownedEntities.Count());
                Assert.Equal(2, ownedEntities.Where(e => e.IsMappedToJson()).Count());
                Assert.Equal(2, ownedEntities.Where(e => e.IsOwned() && !e.IsMappedToJson()).Count());
            }

            private class JsonEntity
            {
                public int Id { get; set; }
                public string Name { get; set; }

                public OwnedEntity OwnedReference1 { get; set; }
                public OwnedEntity OwnedReference2 { get; set; }

                public List<OwnedEntity> OwnedCollection1 { get; set; }
                public List<OwnedEntity> OwnedCollection2 { get; set; }
            }

            public class OwnedEntity
            {
                public DateTime Date { get; set; }
                public double Fraction { get; set; }
            }
        }
    }
}
