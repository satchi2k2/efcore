// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.TestModels.JsonQuery
{
    public class MyOwnedRootShared
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public MyOwnedBranchShared OwnedReferenceSharedBranch { get; set; }
        public List<MyOwnedBranchShared> OwnedCollectionSharedBranch { get; set; }
    }
}
