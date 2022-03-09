// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.TestModels.JsonQuery;

namespace Microsoft.EntityFrameworkCore.Query;

public abstract class JsonQueryFixtureBase : SharedStoreFixtureBase<JsonQueryContext>, IQueryFixtureBase
{
    private JsonQueryData _expectedData;

    public Func<DbContext> GetContextCreator()
        => () => CreateContext();

    public virtual ISetSource GetExpectedData()
    {
        if (_expectedData == null)
        {
            _expectedData = new JsonQueryData();
        }

        return _expectedData;
    }

    public IReadOnlyDictionary<Type, object> GetEntitySorters()
        => new Dictionary<Type, Func<object, object>>
        {
            { typeof(JsonEntityBasic), e => ((JsonEntityBasic)e)?.Id },
            { typeof(JsonEntityCustomNaming), e => ((JsonEntityCustomNaming)e)?.Id },
            { typeof(JsonEntitySingleOwned), e => ((JsonEntitySingleOwned)e)?.Id },
        }.ToDictionary(e => e.Key, e => (object)e.Value);

    public IReadOnlyDictionary<Type, object> GetEntityAsserters()
        => new Dictionary<Type, Action<object, object>>
        {
            {
                typeof(JsonEntityBasic), (e, a) =>
                {
                    Assert.Equal(e == null, a == null);
                    if (a != null)
                    {
                        var ee = (JsonEntityBasic)e;
                        var aa = (JsonEntityBasic)a;

                        Assert.Equal(ee.Id, aa.Id);
                        Assert.Equal(ee.Name, aa.Name);

                        AssertOwnedRoot(ee.OwnedReferenceSharedRoot, aa.OwnedReferenceSharedRoot);

                        Assert.Equal(ee.OwnedCollectionSharedRoot.Count, aa.OwnedCollectionSharedRoot.Count);
                        for (var i = 0; i < ee.OwnedCollectionSharedRoot.Count; i++)
                        {
                            AssertOwnedRoot(ee.OwnedCollectionSharedRoot[i], aa.OwnedCollectionSharedRoot[i]);
                        }
                    }
                }
            },
            {
                typeof(MyOwnedRootShared), (e, a) =>
                {
                    if (a != null)
                    {
                        var ee = (MyOwnedRootShared)e;
                        var aa = (MyOwnedRootShared)a;

                        AssertOwnedRoot(ee, aa);
                    }
                }
            },
            {
                typeof(MyOwnedBranchShared), (e, a) =>
                {
                    if (a != null)
                    {
                        var ee = (MyOwnedBranchShared)e;
                        var aa = (MyOwnedBranchShared)a;

                        AssertOwnedBranch(ee, aa);
                    }
                }
            },
            {
                typeof(MyOwnedLeafShared), (e, a) =>
                {
                    if (a != null)
                    {
                        var ee = (MyOwnedLeafShared)e;
                        var aa = (MyOwnedLeafShared)a;

                        AssertOwnedLeaf(ee, aa);
                    }
                }
            },
            {
                typeof(JsonEntityCustomNaming), (e, a) =>
                {
                    Assert.Equal(e == null, a == null);
                    if (a != null)
                    {
                        var ee = (JsonEntityCustomNaming)e;
                        var aa = (JsonEntityCustomNaming)a;

                        Assert.Equal(ee.Id, aa.Id);
                        Assert.Equal(ee.Title, aa.Title);

                        AssertCustomNameRoot(ee.OwnedReferenceRoot, aa.OwnedReferenceRoot);

                        Assert.Equal(ee.OwnedCollectionRoot.Count, aa.OwnedCollectionRoot.Count);
                        for (var i = 0; i < ee.OwnedCollectionRoot.Count; i++)
                        {
                            AssertCustomNameRoot(ee.OwnedCollectionRoot[i], aa.OwnedCollectionRoot[i]);
                        }
                    }
                }
            },
            {
                typeof( JsonOwnedCustomNameRoot), (e, a) =>
                {
                    if (a != null)
                    {
                        var ee = (JsonOwnedCustomNameRoot)e;
                        var aa = (JsonOwnedCustomNameRoot)a;

                        AssertCustomNameRoot(ee, aa);
                    }
                }
            },
            {
                typeof(JsonOwnedCustomNameBranch), (e, a) =>
                {
                    if (a != null)
                    {
                        var ee = (JsonOwnedCustomNameBranch)e;
                        var aa = (JsonOwnedCustomNameBranch)a;

                        AssertCustomNameBranch(ee, aa);
                    }
                }
            },
            {
                typeof(JsonEntitySingleOwned), (e, a) =>
                {
                    Assert.Equal(e == null, a == null);
                    if (a != null)
                    {
                        var ee = (JsonEntitySingleOwned)e;
                        var aa = (JsonEntitySingleOwned)a;

                        Assert.Equal(ee.Id, aa.Id);
                        Assert.Equal(ee.Name, aa.Name);

                        Assert.Equal(ee.OwnedCollection?.Count ?? 0, aa.OwnedCollection?.Count ?? 0);
                        for (var i = 0; i < ee.OwnedCollection.Count; i++)
                        {
                            AssertOwnedLeaf(ee.OwnedCollection[i], aa.OwnedCollection[i]);
                        }
                    }
                }
            },
        }.ToDictionary(e => e.Key, e => (object)e.Value);

    private static void AssertOwnedRoot(MyOwnedRootShared expected, MyOwnedRootShared actual)
    {
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Number, actual.Number);

        AssertOwnedBranch(expected.OwnedReferenceSharedBranch, actual.OwnedReferenceSharedBranch);
        Assert.Equal(expected.OwnedCollectionSharedBranch.Count, actual.OwnedCollectionSharedBranch.Count);
        for (var i = 0; i < expected.OwnedCollectionSharedBranch.Count; i++)
        {
            AssertOwnedBranch(expected.OwnedCollectionSharedBranch[i], actual.OwnedCollectionSharedBranch[i]);
        }
    }

    private static void AssertOwnedBranch(MyOwnedBranchShared expected, MyOwnedBranchShared actual)
    {
        Assert.Equal(expected.Date, actual.Date);
        Assert.Equal(expected.Fraction, actual.Fraction);

        AssertOwnedLeaf(expected.OwnedReferenceSharedLeaf, actual.OwnedReferenceSharedLeaf);
        Assert.Equal(expected.OwnedCollectionSharedLeaf.Count, actual.OwnedCollectionSharedLeaf.Count);
        for (var i = 0; i < expected.OwnedCollectionSharedLeaf.Count; i++)
        {
            AssertOwnedLeaf(expected.OwnedCollectionSharedLeaf[i], actual.OwnedCollectionSharedLeaf[i]);
        }
    }

    private static void AssertOwnedLeaf(MyOwnedLeafShared expected, MyOwnedLeafShared actual)
    {
        Assert.Equal(expected.SomethingSomething, actual.SomethingSomething);
    }

    public static void AssertCustomNameRoot(JsonOwnedCustomNameRoot expected, JsonOwnedCustomNameRoot actual)
    {
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Number, actual.Number);
        AssertCustomNameBranch(expected.OwnedReferenceBranch, actual.OwnedReferenceBranch);
        Assert.Equal(expected.OwnedCollectionBranch.Count, actual.OwnedCollectionBranch.Count);
        for (var i = 0; i < expected.OwnedCollectionBranch.Count; i++)
        {
            AssertCustomNameBranch(expected.OwnedCollectionBranch[i], actual.OwnedCollectionBranch[i]);
        }
    }

    public static void AssertCustomNameBranch(JsonOwnedCustomNameBranch expected, JsonOwnedCustomNameBranch actual)
    {
        Assert.Equal(expected.Date, actual.Date);
        Assert.Equal(expected.Fraction, actual.Fraction);
    }

    protected override string StoreName { get; } = "JsonQueryTest";

    public new RelationalTestStore TestStore
        => (RelationalTestStore)base.TestStore;

    public TestSqlLoggerFactory TestSqlLoggerFactory
        => (TestSqlLoggerFactory)ListLoggerFactory;

    public override JsonQueryContext CreateContext()
    {
        var context = base.CreateContext();

        return context;
    }

    protected override void Seed(JsonQueryContext context)
        => JsonQueryContext.Seed(context);

    protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
    {
        modelBuilder.Entity<JsonEntityBasic>().OwnsOne(x => x.OwnedReferenceSharedRoot, b =>
        {
            b.ToJson("json_reference_shared");
            b.OwnsOne(x => x.OwnedReferenceSharedBranch, bb =>
            {
                bb.Property(x => x.Fraction).HasPrecision(18, 2);
                bb.OwnsOne(x => x.OwnedReferenceSharedLeaf);
                bb.OwnsMany(x => x.OwnedCollectionSharedLeaf);
            });

            b.OwnsMany(x => x.OwnedCollectionSharedBranch, bb =>
            {
                bb.Property(x => x.Fraction).HasPrecision(18, 2);
                bb.OwnsOne(x => x.OwnedReferenceSharedLeaf);
                bb.OwnsMany(x => x.OwnedCollectionSharedLeaf);
            });
        });

        //modelBuilder.Entity<MyEntity>().Navigation(x => x.OwnedReferenceSharedRoot).IsRequired();

        modelBuilder.Entity<JsonEntityBasic>().OwnsMany(x => x.OwnedCollectionSharedRoot, b =>
        {
            b.ToJson("json_collection_shared");
            b.OwnsOne(x => x.OwnedReferenceSharedBranch, bb =>
            {
                bb.Property(x => x.Fraction).HasPrecision(18, 2);
                bb.OwnsOne(x => x.OwnedReferenceSharedLeaf);
                bb.OwnsMany(x => x.OwnedCollectionSharedLeaf);
            });

            b.OwnsMany(x => x.OwnedCollectionSharedBranch, bb =>
            {
                bb.Property(x => x.Fraction).HasPrecision(18, 2);
                bb.OwnsOne(x => x.OwnedReferenceSharedLeaf);
                bb.OwnsMany(x => x.OwnedCollectionSharedLeaf);
            });
        });

        modelBuilder.Entity<JsonEntityCustomNaming>().OwnsOne(x => x.OwnedReferenceRoot, b =>
        {
            b.ToJson("json_reference_custom_naming");
            b.OwnsOne(x => x.OwnedReferenceBranch, bb =>
            {
                //bb.Property(x => x.Fraction).HasColumnName("CustomFraction");
            });

            b.OwnsMany(x => x.OwnedCollectionBranch, bb =>
            {
                //bb.Property(x => x.Fraction).HasColumnName("CustomFraction");
            });

            //b.Property(x => x.OwnedCollectionBranch).HasColumnName("CustomOwnedCollectionBranch");
        });

        //modelBuilder.Entity<JsonCustomNamingEntity>().Navigation(x => x.OwnedReferenceRoot).HasColumnName("CustomOwnedReferenceRoot");

        modelBuilder.Entity<JsonEntityCustomNaming>().OwnsMany(x => x.OwnedCollectionRoot, b =>
        {
            b.ToJson("json_collection_custom_naming");
            b.OwnsOne(x => x.OwnedReferenceBranch, bb =>
            {
                //bb.Property(x => x.Fraction).HasColumnName("CustomFraction");
            });

            //b.Property(x => x.OwnedCollectionBranch);//.HasColumnName("CustomOwnedCollectionBranch");
            b.OwnsMany(x => x.OwnedCollectionBranch, bb =>
            {
                //bb.Property(x => x.Fraction).HasColumnName("CustomFraction");
            });
        });

        modelBuilder.Entity<JsonEntitySingleOwned>().OwnsMany(x => x.OwnedCollection, b => b.ToJson("json_collection"));
    }
}
