// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Metadata.ModelConventions;
using Microsoft.Data.Entity.Relational.Metadata.ModelConventions.AttributeConventions.EntityType;
using Microsoft.Data.Entity.Relational.Metadata.ModelConventions.AttributeConventions.Property;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Relational
{
    public class RelationalConventionSetBuilder : IConventionSetBuilder
    {
        public virtual ConventionSet AddConventions(ConventionSet conventionSet)
        {
            Check.NotNull(conventionSet, nameof(conventionSet));

            conventionSet.PropertyAddedConventions.Add(new ColumnAttributeConvention());

            conventionSet.EntityTypeAddedConventions.Add(new TableAttributeConvention());
            return conventionSet;
        }
    }
}
