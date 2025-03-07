﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.TestModels.ManyToManyModel;

public class UnidirectionalEntityRoot
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual ICollection<UnidirectionalEntityThree> ThreeSkipShared { get; set; }
}
