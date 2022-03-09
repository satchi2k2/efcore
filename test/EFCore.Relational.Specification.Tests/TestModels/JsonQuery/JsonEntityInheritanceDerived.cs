// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.TestModels.JsonQuery
{
    public  class JsonEntityInheritanceDerived : JsonEntityInheritanceBase
    {
        public double Fraction { get; set; }
        public MyOwnedBranchShared ReferenceOnDerived { get; set; }
        public List<MyOwnedBranchShared> CollectionOnDerived { get; set; }
    }
}
