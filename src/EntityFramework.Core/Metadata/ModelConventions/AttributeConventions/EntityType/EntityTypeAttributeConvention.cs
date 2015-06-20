// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Metadata.ModelConventions.AttributeConventions.EntityType
{
    public abstract class EntityTypeAttributeConvention<TAttribute> : IEntityTypeConvention
    {
        public virtual InternalEntityTypeBuilder Apply(InternalEntityTypeBuilder entityTypeBuilder)
        {
            Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));

            var attributes = entityTypeBuilder.Metadata.ClrType?.GetTypeInfo().GetCustomAttributes(true).OfType<TAttribute>();
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    Apply(entityTypeBuilder, attribute);
                }
            }

            return entityTypeBuilder;
        }

        public abstract void Apply([NotNull] InternalEntityTypeBuilder entityTypeBuilder, [NotNull] TAttribute attribute);
    }
}
