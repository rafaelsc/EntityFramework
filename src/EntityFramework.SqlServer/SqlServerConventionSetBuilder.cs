// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Metadata.ModelConventions;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.SqlServer.Metadata.ModelConventions;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServer
{
    public class SqlServerConventionSetBuilder : RelationalConventionSetBuilder
    {
        public override ConventionSet AddConventions(ConventionSet conventionSet)
        {
            Check.NotNull(conventionSet, nameof(conventionSet));

            base.AddConventions(conventionSet);

            conventionSet.ModelInitializedConventions.Add(new SqlServerValueGenerationStrategyConvention());
            return conventionSet;
        }
    }
}