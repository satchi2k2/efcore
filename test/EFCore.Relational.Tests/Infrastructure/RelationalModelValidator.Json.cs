// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    public partial class RelationalModelValidatorTest
    {
        [ConditionalFact]
        public void Throw_when_non_json_entity_is_the_owner_of_json_entity_ref_ref()
        {
            var modelBuilder = CreateConventionModelBuilder();
            modelBuilder.Entity<ValidatorJsonEntityBasic>(b =>
            {
                b.OwnsOne(x => x.OwnedReference, bb =>
                {
                    bb.Ignore(x => x.NestedCollection);
                    bb.OwnsOne(x => x.NestedReference, bbb => bbb.ToJson("reference_reference"));
                });
                b.Ignore(x => x.OwnedCollection);
            });

            VerifyError(
                "Json mapped type can't be owned by a non-json owned type. Only regular entity types or json mapped types are allowed.",
                modelBuilder);
        }

        [ConditionalFact]
        public void Throw_when_non_json_entity_is_the_owner_of_json_entity_ref_col()
        {
            var modelBuilder = CreateConventionModelBuilder();
            modelBuilder.Entity<ValidatorJsonEntityBasic>(b =>
            {
                b.OwnsOne(x => x.OwnedReference, bb =>
                {
                    bb.OwnsMany(x => x.NestedCollection, bbb => bbb.ToJson("reference_collection"));
                    bb.Ignore(x => x.NestedReference);
                });
                b.Ignore(x => x.OwnedCollection);
            });

            VerifyError(
                "Json mapped type can't be owned by a non-json owned type. Only regular entity types or json mapped types are allowed.",
                modelBuilder);
        }

        [ConditionalFact]
        public void Throw_when_non_json_entity_is_the_owner_of_json_entity_col_ref()
        {
            var modelBuilder = CreateConventionModelBuilder();
            modelBuilder.Entity<ValidatorJsonEntityBasic>(b =>
            {
                b.OwnsMany(x => x.OwnedCollection, bb =>
                {
                    bb.Ignore(x => x.NestedCollection);
                    bb.OwnsOne(x => x.NestedReference, bbb => bbb.ToJson("collection_reference"));
                });
                b.Ignore(x => x.OwnedReference);
            });

            VerifyError(
                "Json mapped type can't be owned by a non-json owned type. Only regular entity types or json mapped types are allowed.",
                modelBuilder);
        }

        [ConditionalFact]
        public void Throw_when_non_json_entity_is_the_owner_of_json_entity_col_col()
        {
            var modelBuilder = CreateConventionModelBuilder();
            modelBuilder.Entity<ValidatorJsonEntityBasic>(b =>
            {
                b.OwnsMany(x => x.OwnedCollection, bb =>
                {
                    bb.Ignore(x => x.NestedReference);
                    bb.OwnsOne(x => x.NestedCollection, bbb => bbb.ToJson("collection_collection"));
                });
                b.Ignore(x => x.OwnedReference);
            });

            VerifyError(
                "Json mapped type can't be owned by a non-json owned type. Only regular entity types or json mapped types are allowed.",
                modelBuilder);
        }

        // TODO: add test for side by side



        private class ValidatorJsonEntityBasic
        {
            public int Id { get; set; }
            public ValidatorJsonOwnedRoot OwnedReference { get; set; }
            public List<ValidatorJsonOwnedRoot> OwnedCollection { get; set; }

        }

        public class ValidatorJsonOwnedRoot
        {
            public string Name { get; set; }

            public ValidatorJsonOwnedBranch NestedReference { get; set; }
            public List<ValidatorJsonOwnedBranch> NestedCollection { get; set; }
        }

        public class ValidatorJsonOwnedBranch
        {
            public double Number { get; set; }
        }

        private class ValidatorJsonEntityExplicitOrdinal
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public List<ValidatorJsonOwnedExplicitOrdinal> OwnedCollection { get; set; }
        }

        private class ValidatorJsonOwnedExplicitOrdinal
        {
            public int Ordinal { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
