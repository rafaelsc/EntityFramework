﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Metadata.ModelConventions.AttributeConventions.Property;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Relational.Metadata.ModelConventions.AttributeConventions.Property
{
    public class ColumnAttributeConvention : PropertyAttributeConvention<ColumnAttribute>
    {
        public override void Apply(InternalPropertyBuilder propertyBuilder, ColumnAttribute attribute)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NotNull(attribute, nameof(attribute));

            if (!string.IsNullOrWhiteSpace(attribute.Name))
            {
                propertyBuilder.Metadata.Relational().Column = attribute.Name;
            }

            if (!string.IsNullOrWhiteSpace(attribute.TypeName))
            {
                propertyBuilder.Metadata.Relational().ColumnType = attribute.TypeName;
            }
        }
    }
}
