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

        [ConditionalFact]
        public void Tpt_not_supported_for_owner_of_json_entity_on_base()
        {
            var modelBuilder = CreateConventionModelBuilder();
            modelBuilder.Entity<ValidatorJsonEntityInheritanceBase>(b =>
            {
                b.ToTable("Table1");
                b.OwnsOne(x => x.ReferenceOnBase, bb =>
                {
                    bb.ToJson("reference");
                });
            });

            modelBuilder.Entity<ValidatorJsonEntityInheritanceDerived>(b =>
            {
                b.HasBaseType<ValidatorJsonEntityInheritanceBase>();
                b.ToTable("Table2");
                b.Ignore(x => x.CollectionOnDerived);
            });

            VerifyError(
                "Json mapped type can't be owned by a non-json owned type. Only regular entity types or json mapped types are allowed.",
                modelBuilder);
        }

        public void Tpt_not_supported_for_owner_of_json_entity_on_derived()
        {
            var modelBuilder = CreateConventionModelBuilder();
            modelBuilder.Entity<ValidatorJsonEntityInheritanceBase>(b =>
            {
                b.ToTable("Table1");
                b.Ignore(x => x.ReferenceOnBase);
                b.OwnsOne(x => x.ReferenceOnBase, bb => bb.ToJson("reference"));
            });

            modelBuilder.Entity<ValidatorJsonEntityInheritanceDerived>(b =>
            {
                b.ToTable("Table2");
                b.OwnsMany(x => x.CollectionOnDerived, bb => bb.ToJson("collection"));;
            });

            VerifyError(
                "Json mapped type can't be owned by a non-json owned type. Only regular entity types or json mapped types are allowed.",
                modelBuilder);
        }

        private class ValidatorJsonEntityBasic
        {
            public int Id { get; set; }
            public ValidatorJsonOwnedRoot OwnedReference { get; set; }
            public List<ValidatorJsonOwnedRoot> OwnedCollection { get; set; }
        }

        private class ValidatorJsonEntityInheritanceBase
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public ValidatorJsonOwnedBranch ReferenceOnBase { get; set; }
        }

        private class ValidatorJsonEntityInheritanceDerived : ValidatorJsonEntityInheritanceBase
        {
            public bool Switch { get; set; }

            public List<ValidatorJsonOwnedBranch> CollectionOnDerived { get; set; }
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
