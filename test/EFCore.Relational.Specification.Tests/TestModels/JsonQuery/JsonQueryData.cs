// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.TestModels.JsonQuery;

public class JsonQueryData : ISetSource
{
    public JsonQueryData()
    {
        JsonEntitiesBasic = CreateJsonEntitiesBasic();
        JsonEntitiesCustomNaming = CreateJsonEntitiesCustomNaming();
        JsonEntitiesSingleOwned = CreateJsonEntitiesSingleOwned();
        JsonEntitiesInheritance = CreateJsonEntitiesInheritance();
    }

    public IReadOnlyList<JsonEntityBasic> JsonEntitiesBasic { get; }
    public IReadOnlyList<JsonEntityCustomNaming> JsonEntitiesCustomNaming { get; set; }
    public IReadOnlyList<JsonEntitySingleOwned> JsonEntitiesSingleOwned { get; set; }
    public IReadOnlyList<JsonEntityInheritanceBase> JsonEntitiesInheritance { get; set; }

    public static IReadOnlyList<JsonEntityBasic> CreateJsonEntitiesBasic()
    {
        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------

        var e1_r_r_r_shared = new MyOwnedLeafShared { SomethingSomething = "e1_r_r_r_shared" };
        var e1_r_r_c1_shared = new MyOwnedLeafShared { SomethingSomething = "e1_r_r_c1_shared" };
        var e1_r_r_c2_shared = new MyOwnedLeafShared { SomethingSomething = "e1_r_r_c2_shared" };

        //-------------------------------------------------------------------------------------------

        var e1_r_r_shared = new MyOwnedBranchShared
        {
            Date = new DateTime(2100, 1, 1),
            Fraction = 10.0M,
            OwnedReferenceSharedLeaf = e1_r_r_r_shared,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { e1_r_r_c1_shared, e1_r_r_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------

        var e1_r_c1_r_shared = new MyOwnedLeafShared { SomethingSomething = "e1_r_c1_r_shared" };
        var e1_r_c1_c1_shared = new MyOwnedLeafShared { SomethingSomething = "e1_r_c1_c1_shared" };
        var e1_r_c1_c2_shared = new MyOwnedLeafShared { SomethingSomething = "e1_r_c1_c2_shared" };

        //-------------------------------------------------------------------------------------------

        var e1_r_c1_shared = new MyOwnedBranchShared
        {
            Date = new DateTime(2101, 1, 1),
            Fraction = 10.1M,
            OwnedReferenceSharedLeaf = e1_r_c1_r_shared,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { e1_r_c1_c1_shared, e1_r_c1_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------

        var e1_r_c2_r_shared = new MyOwnedLeafShared { SomethingSomething = "e1_r_c2_r_shared" };
        var e1_r_c2_c1_shared = new MyOwnedLeafShared { SomethingSomething = "e1_r_c2_c1_shared" };
        var e1_r_c2_c2_shared = new MyOwnedLeafShared { SomethingSomething = "e1_r_c2_c2_shared" };

        //-------------------------------------------------------------------------------------------

        var e1_r_c2_shared = new MyOwnedBranchShared
        {
            Date = new DateTime(2102, 1, 1),
            Fraction = 10.2M,
            OwnedReferenceSharedLeaf = e1_r_c2_r_shared,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { e1_r_c2_c1_shared, e1_r_c2_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------


        var e1_r_shared = new MyOwnedRootShared
        {
            Name = "e1_r_shared",
            Number = 10,
            OwnedReferenceSharedBranch = e1_r_r_shared,
            OwnedCollectionSharedBranch = new List<MyOwnedBranchShared> { e1_r_c1_shared, e1_r_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------


        var e1_c1_r_r_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c1_r_r_shared" };
        var e1_c1_r_c1_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c1_r_c1_shared" };
        var e1_c1_r_c2_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c1_r_c2_shared" };

        //-------------------------------------------------------------------------------------------

        var e1_c1_r_shared = new MyOwnedBranchShared
        {
            Date = new DateTime(2110, 1, 1),
            Fraction = 11.0M,
            OwnedReferenceSharedLeaf = e1_c1_r_r_shared,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { e1_c1_r_c1_shared, e1_c1_r_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------

        var e1_c1_c1_r_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c1_c1_r_shared" };
        var e1_c1_c1_c1_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c1_c1_c1_shared" };
        var e1_c1_c1_c2_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c1_c1_c2_shared" };

        //-------------------------------------------------------------------------------------------

        var e1_c1_c1_shared = new MyOwnedBranchShared
        {
            Date = new DateTime(2111, 1, 1),
            Fraction = 11.1M,
            OwnedReferenceSharedLeaf = e1_c1_c1_r_shared,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { e1_c1_c1_c1_shared, e1_c1_c1_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------

        var e1_c1_c2_r_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c1_c2_r_shared" };
        var e1_c1_c2_c1_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c1_c2_c1_shared" };
        var e1_c1_c2_c2_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c1_c2_c2_shared" };

        //-------------------------------------------------------------------------------------------

        var e1_c1_c2_shared = new MyOwnedBranchShared
        {
            Date = new DateTime(2112, 1, 1),
            Fraction = 11.2M,
            OwnedReferenceSharedLeaf = e1_c1_c2_r_shared,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { e1_c1_c2_c1_shared, e1_c1_c2_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------


        var e1_c1_shared = new MyOwnedRootShared
        {
            Name = "e1_c1_shared",
            Number = 11,
            OwnedReferenceSharedBranch = e1_c1_r_shared,
            OwnedCollectionSharedBranch = new List<MyOwnedBranchShared> { e1_c1_c1_shared, e1_c1_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------


        var e1_c2_r_r_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c2_r_r_shared" };
        var e1_c2_r_c1_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c2_r_c1_shared" };
        var e1_c2_r_c2_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c2_r_c2_shared" };

        //-------------------------------------------------------------------------------------------

        var e1_c2_r_shared = new MyOwnedBranchShared
        {
            Date = new DateTime(2120, 1, 1),
            Fraction = 12.0M,
            OwnedReferenceSharedLeaf = e1_c2_r_r_shared,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { e1_c2_r_c1_shared, e1_c2_r_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------

        var e1_c2_c1_r_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c2_c1_r_shared" };
        var e1_c2_c1_c1_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c2_c1_c1_shared" };
        var e1_c2_c1_c2_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c2_c1_c2_shared" };

        //-------------------------------------------------------------------------------------------

        var e1_c2_c1_shared = new MyOwnedBranchShared
        {
            Date = new DateTime(2121, 1, 1),
            Fraction = 12.1M,
            OwnedReferenceSharedLeaf = e1_c2_c1_r_shared,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { e1_c2_c1_c1_shared, e1_c2_c1_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------

        var e1_c2_c2_r_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c2_c2_r_shared" };
        var e1_c2_c2_c1_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c2_c2_c1_shared" };
        var e1_c2_c2_c2_shared = new MyOwnedLeafShared { SomethingSomething = "e1_c2_c2_c2_shared" };

        //-------------------------------------------------------------------------------------------

        var e1_c2_c2_shared = new MyOwnedBranchShared
        {
            Date = new DateTime(2122, 1, 1),
            Fraction = 12.2M,
            OwnedReferenceSharedLeaf = e1_c2_c2_r_shared,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { e1_c2_c2_c1_shared, e1_c2_c2_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------

        var e1_c2_shared = new MyOwnedRootShared
        {
            Name = "e1_c2_shared",
            Number = 12,
            OwnedReferenceSharedBranch = e1_c2_r_shared,
            OwnedCollectionSharedBranch = new List<MyOwnedBranchShared> { e1_c2_c1_shared, e1_c2_c2_shared }
        };

        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------

        var entity1 = new JsonEntityBasic
        {
            Id = 1,
            Name = "JsonEntityBasic1",
            OwnedReferenceSharedRoot = e1_r_shared,
            OwnedCollectionSharedRoot = new List<MyOwnedRootShared> { e1_c1_shared, e1_c2_shared }
        };

        return new List<JsonEntityBasic> { entity1 };
    }

    public static IReadOnlyList<JsonEntityCustomNaming> CreateJsonEntitiesCustomNaming()
    {
        var e1_r_r = new JsonOwnedCustomNameBranch
        {
            Date = new DateTime(2100, 1, 1),
            Fraction = 10.0,
        };

        var e1_r_c1 = new JsonOwnedCustomNameBranch
        {
            Date = new DateTime(2101, 1, 1),
            Fraction = 10.1,
        };

        var e1_r_c2 = new JsonOwnedCustomNameBranch
        {
            Date = new DateTime(2102, 1, 1),
            Fraction = 10.2,
        };

        var e1_r = new JsonOwnedCustomNameRoot
        {
            Name = "e1_r",
            Number = 10,
            OwnedReferenceBranch = e1_r_r,
            OwnedCollectionBranch = new List<JsonOwnedCustomNameBranch> { e1_r_c1, e1_r_c2 }
        };

        var e1_c1_r = new JsonOwnedCustomNameBranch
        {
            Date = new DateTime(2110, 1, 1),
            Fraction = 11.0,
        };

        var e1_c1_c1 = new JsonOwnedCustomNameBranch
        {
            Date = new DateTime(2111, 1, 1),
            Fraction = 11.1,
        };

        var e1_c1_c2 = new JsonOwnedCustomNameBranch
        {
            Date = new DateTime(2112, 1, 1),
            Fraction = 11.2,
        };

        var e1_c1 = new JsonOwnedCustomNameRoot
        {
            Name = "e1_c1",
            Number = 11,
            OwnedReferenceBranch = e1_c1_r,
            OwnedCollectionBranch = new List<JsonOwnedCustomNameBranch> { e1_c1_c1, e1_c1_c2 }
        };

        var e1_c2_r = new JsonOwnedCustomNameBranch
        {
            Date = new DateTime(2120, 1, 1),
            Fraction = 12.0,
        };

        var e1_c2_c1 = new JsonOwnedCustomNameBranch
        {
            Date = new DateTime(2121, 1, 1),
            Fraction = 12.1,
        };

        var e1_c2_c2 = new JsonOwnedCustomNameBranch
        {
            Date = new DateTime(2122, 1, 1),
            Fraction = 12.2,
        };

        var e1_c2 = new JsonOwnedCustomNameRoot
        {
            Name = "e1_c2",
            Number = 12,
            OwnedReferenceBranch = e1_c2_r,
            OwnedCollectionBranch = new List<JsonOwnedCustomNameBranch> { e1_c2_c1, e1_c2_c2 }
        };

        var entity1 = new JsonEntityCustomNaming
        {
            Id = 1,
            Title = "JsonEntityCustomNaming1",
            OwnedReferenceRoot = e1_r,
            OwnedCollectionRoot = new List<JsonOwnedCustomNameRoot> { e1_c1, e1_c2 }
        };

        return new List<JsonEntityCustomNaming> { entity1 };
    }

    public static IReadOnlyList<JsonEntitySingleOwned> CreateJsonEntitiesSingleOwned()
    {
        var e1 = new JsonEntitySingleOwned
        {
            Id = 1,
            Name = "JsonEntitySingleOwned1",
            OwnedCollection = new List<MyOwnedLeafShared>
            {
                new MyOwnedLeafShared { SomethingSomething = "owned_1_1" },
                new MyOwnedLeafShared { SomethingSomething = "owned_1_2" },
                new MyOwnedLeafShared { SomethingSomething = "owned_1_3" },
            }
        };

        var e2 = new JsonEntitySingleOwned
        {
            Id = 2,
            Name = "JsonEntitySingleOwned2",
            OwnedCollection = new List<MyOwnedLeafShared>
            {
            }
        };

        var e3 = new JsonEntitySingleOwned
        {
            Id = 3,
            Name = "JsonEntitySingleOwned3",
            OwnedCollection = new List<MyOwnedLeafShared>
            {
                new MyOwnedLeafShared { SomethingSomething = "owned_3_1" },
                new MyOwnedLeafShared { SomethingSomething = "owned_3_2" },
            }
        };

        return new List<JsonEntitySingleOwned> { e1, e2, e3 };
    }

    public static IReadOnlyList<JsonEntityInheritanceBase> CreateJsonEntitiesInheritance()
    {
        var b1_r_r = new MyOwnedLeafShared
        {
            SomethingSomething = "b1_r_r",
        };

        var b1_r_c1 = new MyOwnedLeafShared
        {
            SomethingSomething = "b1_r_c1",
        };

        var b1_r_c2 = new MyOwnedLeafShared
        {
            SomethingSomething = "b1_r_c2",
        };


        var b1_r = new MyOwnedBranchShared
        {
            Date = new DateTime(2010, 1, 1),
            Fraction = 1.0M,

            OwnedReferenceSharedLeaf = b1_r_r,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { b1_r_c1, b1_r_c2 }
        };

        var b1_c1_r = new MyOwnedLeafShared
        {
            SomethingSomething = "b1_r_r",
        };

        var b1_c1_c1 = new MyOwnedLeafShared
        {
            SomethingSomething = "b1_r_c1",
        };

        var b1_c1_c2 = new MyOwnedLeafShared
        {
            SomethingSomething = "b1_r_c2",
        };

        var b1_c1 = new MyOwnedBranchShared
        {
            Date = new DateTime(2011, 1, 1),
            Fraction = 11.1M,

            OwnedReferenceSharedLeaf = b1_c1_r,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { b1_c1_c1, b1_c1_c2 }
        };

        var b1_c2_r = new MyOwnedLeafShared
        {
            SomethingSomething = "b1_r_r",
        };

        var b1_c2_c1 = new MyOwnedLeafShared
        {
            SomethingSomething = "b1_r_c1",
        };

        var b1_c2_c2 = new MyOwnedLeafShared
        {
            SomethingSomething = "b1_r_c2",
        };

        var b1_c2 = new MyOwnedBranchShared
        {
            Date = new DateTime(2012, 1, 1),
            Fraction = 12.1M,

            OwnedReferenceSharedLeaf = b1_c2_r,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { b1_c2_c1, b1_c2_c2 }
        };

        var b2_r_r = new MyOwnedLeafShared
        {
            SomethingSomething = "b2_r_r",
        };

        var b2_r_c1 = new MyOwnedLeafShared
        {
            SomethingSomething = "b2_r_c1",
        };

        var b2_r_c2 = new MyOwnedLeafShared
        {
            SomethingSomething = "b2_r_c2",
        };

        var b2_r = new MyOwnedBranchShared
        {
            Date = new DateTime(2020, 1, 1),
            Fraction = 2.0M,

            OwnedReferenceSharedLeaf = b2_r_r,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { b2_r_c1, b2_r_c2 }
        };

        var b2_c1_r = new MyOwnedLeafShared
        {
            SomethingSomething = "b2_r_r",
        };

        var b2_c1_c1 = new MyOwnedLeafShared
        {
            SomethingSomething = "b2_r_c1",
        };

        var b2_c1_c2 = new MyOwnedLeafShared
        {
            SomethingSomething = "b2_r_c2",
        };

        var b2_c1 = new MyOwnedBranchShared
        {
            Date = new DateTime(2021, 1, 1),
            Fraction = 21.1M,

            OwnedReferenceSharedLeaf = b2_c1_r,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { b2_c1_c1, b2_c1_c2 }
        };

        var b2_c2_r = new MyOwnedLeafShared
        {
            SomethingSomething = "b2_r_r",
        };

        var b2_c2_c1 = new MyOwnedLeafShared
        {
            SomethingSomething = "b2_r_c1",
        };

        var b2_c2_c2 = new MyOwnedLeafShared
        {
            SomethingSomething = "b2_r_c2",
        };

        var b2_c2 = new MyOwnedBranchShared
        {
            Date = new DateTime(2022, 1, 1),
            Fraction = 22.1M,

            OwnedReferenceSharedLeaf = b2_c2_r,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { b2_c2_c1, b2_c2_c2 }
        };

        var d2_r_r = new MyOwnedLeafShared
        {
            SomethingSomething = "d2_r_r",
        };

        var d2_r_c1 = new MyOwnedLeafShared
        {
            SomethingSomething = "d2_r_c1",
        };

        var d2_r_c2 = new MyOwnedLeafShared
        {
            SomethingSomething = "d2_r_c2",
        };

        var d2_r = new MyOwnedBranchShared
        {
            Date = new DateTime(2220, 1, 1),
            Fraction = 22.0M,

            OwnedReferenceSharedLeaf = d2_r_r,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { d2_r_c1, d2_r_c2 }
        };

        var d2_c1_r = new MyOwnedLeafShared
        {
            SomethingSomething = "d2_r_r",
        };

        var d2_c1_c1 = new MyOwnedLeafShared
        {
            SomethingSomething = "d2_r_c1",
        };

        var d2_c1_c2 = new MyOwnedLeafShared
        {
            SomethingSomething = "d2_r_c2",
        };

        var d2_c1 = new MyOwnedBranchShared
        {
            Date = new DateTime(2221, 1, 1),
            Fraction = 221.1M,

            OwnedReferenceSharedLeaf = d2_c1_r,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { d2_c1_c1, d2_c1_c2 }
        };

        var d2_c2_r = new MyOwnedLeafShared
        {
            SomethingSomething = "d2_r_r",
        };

        var d2_c2_c1 = new MyOwnedLeafShared
        {
            SomethingSomething = "d2_r_c1",
        };

        var d2_c2_c2 = new MyOwnedLeafShared
        {
            SomethingSomething = "d2_r_c2",
        };

        var d2_c2 = new MyOwnedBranchShared
        {
            Date = new DateTime(2222, 1, 1),
            Fraction = 222.1M,

            OwnedReferenceSharedLeaf = d2_c2_r,
            OwnedCollectionSharedLeaf = new List<MyOwnedLeafShared> { d2_c2_c1, d2_c2_c2 }
        };

        var baseEntity = new JsonEntityInheritanceBase
        {
            Id = 1,
            Name = "JsonEntityInheritanceBase1",
            ReferenceOnBase = b1_r,
            CollectionOnBase = new List<MyOwnedBranchShared> { b1_c1, b1_c2 }
        };

        var derivedEntity = new JsonEntityInheritanceDerived
        {
            Id = 2,
            Name = "JsonEntityInheritanceDerived2",
            ReferenceOnBase = b2_r,
            CollectionOnBase = new List<MyOwnedBranchShared> { b2_c1, b2_c2 },

            ReferenceOnDerived = d2_r,
            CollectionOnDerived = new List<MyOwnedBranchShared> { d2_c1, d2_c2 },
        };

        return new List<JsonEntityInheritanceBase> { baseEntity, derivedEntity };
    }

    public IQueryable<TEntity> Set<TEntity>()
        where TEntity : class
    {
        if (typeof(TEntity) == typeof(JsonEntityBasic))
        {
            return (IQueryable<TEntity>)JsonEntitiesBasic.AsQueryable();
        }

        if (typeof(TEntity) == typeof(JsonEntityCustomNaming))
        {
            return (IQueryable<TEntity>)JsonEntitiesCustomNaming.AsQueryable();
        }

        if (typeof(TEntity) == typeof(JsonEntitySingleOwned))
        {
            return (IQueryable<TEntity>)JsonEntitiesSingleOwned.AsQueryable();
        }

        if (typeof(TEntity) == typeof(JsonEntityInheritanceBase))
        {
            return (IQueryable<TEntity>)JsonEntitiesInheritance.AsQueryable();
        }

        if (typeof(TEntity) == typeof(JsonEntityInheritanceDerived))
        {
            return (IQueryable<TEntity>)JsonEntitiesInheritance.OfType<JsonEntityInheritanceDerived>().AsQueryable();
        }

        throw new InvalidOperationException("Invalid entity type: " + typeof(TEntity));
    }
}
