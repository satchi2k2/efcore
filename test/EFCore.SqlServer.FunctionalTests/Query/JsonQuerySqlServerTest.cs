// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Query;

public class JsonQuerySqlServerTest : JsonQueryTestBase<JsonQuerySqlServerFixture>
{
    public JsonQuerySqlServerTest(JsonQuerySqlServerFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        Fixture.TestSqlLoggerFactory.Clear();
        //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

    public override async Task Basic_json_projection_owner_entity(bool async)
    {
        await base.Basic_json_projection_owner_entity(async);

        AssertSql(
            @"SELECT [j].[Id], [j].[Name], JSON_QUERY([j].[json_collection_shared],'$'), JSON_QUERY([j].[json_reference_shared],'$')
FROM [JsonBasicEntities] AS [j]");
    }

    public override async Task Basic_json_projection_owned_reference_root(bool async)
    {
        await base.Basic_json_projection_owned_reference_root(async);

        AssertSql(
            @"SELECT JSON_QUERY([j].[json_reference_shared],'$'), [j].[Id]
FROM [JsonBasicEntities] AS [j]");
    }

    public override async Task Basic_json_projection_owned_collection_root(bool async)
    {
        await base.Basic_json_projection_owned_collection_root(async);

        AssertSql(
            @"SELECT JSON_QUERY([j].[json_collection_shared],'$'), [j].[Id]
FROM [JsonBasicEntities] AS [j]");
    }

    public override async Task Basic_json_projection_owned_reference_branch(bool async)
    {
        await base.Basic_json_projection_owned_reference_branch(async);

        AssertSql(
            @"SELECT JSON_QUERY([j].[json_reference_shared],'$.OwnedReferenceSharedBranch'), [j].[Id]
FROM [JsonBasicEntities] AS [j]");
    }

    public override async Task Basic_json_projection_owned_collection_branch(bool async)
    {
        await base.Basic_json_projection_owned_collection_branch(async);

        AssertSql(
            @"SELECT JSON_QUERY([j].[json_reference_shared],'$.OwnedCollectionSharedBranch'), [j].[Id]
FROM [JsonBasicEntities] AS [j]");
    }

    public override async Task Basic_json_projection_owned_reference_leaf(bool async)
    {
        await base.Basic_json_projection_owned_reference_leaf(async);

        AssertSql(
            @"SELECT JSON_QUERY([j].[json_reference_shared],'$.OwnedReferenceSharedBranch.OwnedReferenceSharedLeaf'), [j].[Id]
FROM [JsonBasicEntities] AS [j]");
    }

    public override async Task Basic_json_projection_owned_collection_leaf(bool async)
    {
        await base.Basic_json_projection_owned_collection_leaf(async);

        AssertSql(
            @"SELECT JSON_QUERY([j].[json_reference_shared],'$.OwnedReferenceSharedBranch.OwnedCollectionSharedLeaf'), [j].[Id]
FROM [JsonBasicEntities] AS [j]");
    }

    public override async Task Basic_json_projection_scalar(bool async)
    {
        await base.Basic_json_projection_scalar(async);

        AssertSql(
            @"SELECT CAST(JSON_VALUE([j].[json_reference_shared],'$.Name') AS nvarchar(max))
FROM [JsonBasicEntities] AS [j]");
    }

    public override async Task Json_projection_with_deduplication(bool async)
    {
        await base.Json_projection_with_deduplication(async);

        AssertSql(
            @"");
    }

    private void AssertSql(params string[] expected)
        => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
}
