// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.TestModels.JsonQuery;

namespace Microsoft.EntityFrameworkCore.Query;

public abstract class JsonQueryTestBase<TFixture> : QueryTestBase<TFixture>
    where TFixture : JsonQueryFixtureBase, new()
{
    protected JsonQueryTestBase(TFixture fixture)
        : base(fixture)
    {
    }

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Basic_json_projection_owner_entity(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>(),
            entryCount: 40);

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Basic_json_projection_owned_reference_root(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>().Select(x => x.OwnedReferenceSharedRoot).AsNoTracking());

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Basic_json_projection_owned_collection_root(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>().Select(x => x.OwnedCollectionSharedRoot).AsNoTracking(),
            elementAsserter: (e, a) => AssertCollection(e, a, ordered: true));

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Basic_json_projection_owned_reference_branch(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>().Select(x => x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch).AsNoTracking());

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Basic_json_projection_owned_collection_branch(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>().Select(x => x.OwnedReferenceSharedRoot.OwnedCollectionSharedBranch).AsNoTracking(),
            elementAsserter: (e, a) => AssertCollection(e, a, ordered: true));

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Basic_json_projection_owned_reference_leaf(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>().Select(x => x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch.OwnedReferenceSharedLeaf).AsNoTracking());

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Basic_json_projection_owned_collection_leaf(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>().Select(x => x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch.OwnedCollectionSharedLeaf).AsNoTracking(),
            elementAsserter: (e, a) => AssertCollection(e, a, ordered: true));

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Basic_json_projection_scalar(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>().Select(x => x.OwnedReferenceSharedRoot.Name));

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Json_projection_with_deduplication(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>().Select(x => new
            {
                x,
                x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch,
                x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch.OwnedReferenceSharedLeaf,
                x.OwnedReferenceSharedRoot.OwnedCollectionSharedBranch,
                x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch.OwnedReferenceSharedLeaf.SomethingSomething
            }),
            elementAsserter: (e, a) =>
            {
                AssertEqual(e.OwnedReferenceSharedBranch, a.OwnedReferenceSharedBranch);
                AssertEqual(e.OwnedReferenceSharedLeaf, a.OwnedReferenceSharedLeaf);
                AssertCollection(e.OwnedCollectionSharedBranch, a.OwnedCollectionSharedBranch, ordered: true);
                Assert.Equal(e.SomethingSomething, a.SomethingSomething);
            });


    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Json_projection_with_deduplication_reverse_order(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>().Select(x => new
            {
                x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch.OwnedReferenceSharedLeaf,
                x.OwnedReferenceSharedRoot,
                x
            }).AsNoTracking(),
            elementAsserter: (e, a) =>
            {
                AssertEqual(e.OwnedReferenceSharedLeaf, a.OwnedReferenceSharedLeaf);
                AssertEqual(e.OwnedReferenceSharedRoot, a.OwnedReferenceSharedRoot);
                AssertEqual(e.x, a.x);
            });

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Json_property_in_predicate(bool async)
        => AssertQueryScalar(
            async,
            ss => ss.Set<JsonEntityBasic>()
                .Where(x => x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch.Fraction < 20.5M).Select(x => x.Id));

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Json_subquery_property_pushdown_length(bool async)
        => AssertQueryScalar(
            async,
            ss => ss.Set<JsonEntityBasic>()
                .OrderBy(x => x.Id)
                .Select(x => x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch.OwnedReferenceSharedLeaf.SomethingSomething)
                .Take(3)
                .Distinct()
                .Select(x => x.Length));

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Json_subquery_reference_pushdown_reference(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>()
                .OrderBy(x => x.Id)
                .Select(x => x.OwnedReferenceSharedRoot)
                .Take(10)
                .Distinct()
                .Select(x => x.OwnedReferenceSharedBranch).AsNoTracking());

    [ConditionalTheory(Skip = "issue #24263")]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Json_subquery_reference_pushdown_reference_anonymous_projection(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>()
                .OrderBy(x => x.Id)
                .Select(x => new { Entity = x.OwnedReferenceSharedRoot, Scalar = x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch.OwnedReferenceSharedLeaf.SomethingSomething })
                .Take(10)
                .Distinct()
                .Select(x => new { x.Entity.OwnedReferenceSharedBranch, x.Scalar.Length }).AsNoTracking(),
            elementSorter: e => (e.OwnedReferenceSharedBranch.Date, e.OwnedReferenceSharedBranch.Fraction, e.Length),
            elementAsserter: (e, a) =>
            {
                AssertEqual(e.OwnedReferenceSharedBranch, a.OwnedReferenceSharedBranch);
                Assert.Equal(e.Length, a.Length);
            });

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Json_subquery_reference_pushdown_reference_pushdown_anonymous_projection(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>()
                .OrderBy(x => x.Id)
                .Select(x => new { Root = x.OwnedReferenceSharedRoot, Scalar = x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch.OwnedReferenceSharedLeaf.SomethingSomething })
                .Take(10)
                .Distinct()
                .Select(x => new { Branch = x.Root.OwnedReferenceSharedBranch, x.Scalar.Length })
                .OrderBy(x => x.Length)
                .Take(10)
                .Distinct()
                .Select(x => new { x.Branch.OwnedReferenceSharedLeaf, x.Branch.OwnedCollectionSharedLeaf, x.Length })
            .AsNoTracking(),
            elementSorter: e => (e.OwnedReferenceSharedLeaf.SomethingSomething, e.OwnedCollectionSharedLeaf.Count, e.Length),
            elementAsserter: (e, a) =>
            {
                AssertEqual(e.OwnedReferenceSharedLeaf, a.OwnedReferenceSharedLeaf);
                AssertCollection(e.OwnedCollectionSharedLeaf, e.OwnedCollectionSharedLeaf, ordered: true);
                Assert.Equal(e.Length, a.Length);
            });

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Json_subquery_reference_pushdown_reference_pushdown_reference(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>()
                .OrderBy(x => x.Id)
                .Select(x => x.OwnedReferenceSharedRoot)
                .Take(10)
                .Distinct()
                .OrderBy(x => x.Name)
                .Select(x => x.OwnedReferenceSharedBranch)
                .Take(10)
                .Distinct()
                .Select(x => x.OwnedReferenceSharedLeaf).AsNoTracking());

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Json_subquery_reference_pushdown_reference_pushdown_collection(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>()
                .OrderBy(x => x.Id)
                .Select(x => x.OwnedReferenceSharedRoot)
                .Take(10)
                .Distinct()
                .OrderBy(x => x.Name)
                .Select(x => x.OwnedReferenceSharedBranch)
                .Take(10)
                .Distinct()
                .Select(x => x.OwnedCollectionSharedLeaf).AsNoTracking(),
            elementAsserter: (e, a) => AssertCollection(e, a, ordered: true));

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Json_subquery_reference_pushdown_property(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityBasic>()
                .OrderBy(x => x.Id)
                .Select(x => x.OwnedReferenceSharedRoot.OwnedReferenceSharedBranch.OwnedReferenceSharedLeaf)
                .Take(10)
                .Distinct()
                .Select(x => x.SomethingSomething));

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Custom_naming_projection_owner_entity(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityCustomNaming>().Select(x => x),
            entryCount: 13);

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Custom_naming_projection_owned_reference(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityCustomNaming>().Select(x => x.OwnedReferenceRoot.OwnedReferenceBranch).AsNoTracking());

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Custom_naming_projection_owned_collection(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityCustomNaming>().OrderBy(x => x.Id).Select(x => x.OwnedCollectionRoot).AsNoTracking(),
            assertOrder: true,
            elementAsserter: (e, a) => AssertCollection(e, a, ordered: true));

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Custom_naming_projection_owned_scalar(bool async)
        => AssertQueryScalar(
            async,
            ss => ss.Set<JsonEntityCustomNaming>().Select(x => x.OwnedReferenceRoot.OwnedReferenceBranch.Fraction));

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Custom_naming_projection_everything(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntityCustomNaming>().Select(x => new
            {
                root = x,
                referece = x.OwnedReferenceRoot,
                nested_reference = x.OwnedReferenceRoot.OwnedReferenceBranch,
                collection = x.OwnedCollectionRoot,
                nested_collection = x.OwnedReferenceRoot.OwnedCollectionBranch,
                scalar = x.OwnedReferenceRoot.Name,
                nested_scalar = x.OwnedReferenceRoot.OwnedReferenceBranch.Fraction,
            }),
            elementSorter: e => e.root.Id,
            elementAsserter: (e, a) =>
            {
                AssertEqual(e.root, a.root);
                AssertEqual(e.referece, a.referece);
                AssertEqual(e.nested_reference, a.nested_reference);
                AssertCollection(e.collection, a.collection, ordered: true);
                AssertCollection(e.nested_collection, a.nested_collection, ordered: true);
                Assert.Equal(e.scalar, a.scalar);
                Assert.Equal(e.nested_scalar, a.nested_scalar);
            },
            entryCount: 13);

    [ConditionalTheory]
    [MemberData(nameof(IsAsyncData))]
    public virtual Task Project_entity_with_single_owned(bool async)
        => AssertQuery(
            async,
            ss => ss.Set<JsonEntitySingleOwned>(),
            entryCount: 8);
}
