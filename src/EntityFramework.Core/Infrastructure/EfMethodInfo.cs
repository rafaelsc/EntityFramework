// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Query;
using System.Reflection;

namespace Microsoft.Data.Entity.Infrastructure
{
    public static class EfMethodInfo
    {
        public static readonly MethodInfo PropertyMethodInfo
            = typeof(Ef).GetTypeInfo().GetDeclaredMethod(nameof(Ef.Property));
    }
}
