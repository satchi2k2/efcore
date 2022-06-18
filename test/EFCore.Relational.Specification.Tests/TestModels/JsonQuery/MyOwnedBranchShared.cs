// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.TestModels.JsonQuery
{
    public class MyOwnedBranchShared
    {
        public DateTime Date { get; set; }
        public decimal Fraction { get; set; }

        public MyOwnedLeafShared OwnedReferenceSharedLeaf { get; set; }
        public List<MyOwnedLeafShared> OwnedCollectionSharedLeaf { get; set; }
    }
}
