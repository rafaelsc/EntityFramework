// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Operations;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Relational.Migrations
{
    public abstract class MigrationOperationTransformer
    {
        public virtual IReadOnlyList<MigrationOperation> Transform(
            [NotNull] IReadOnlyList<MigrationOperation> operations,
            [CanBeNull] IModel model = null)
        {
            Check.NotNull(operations, nameof(operations));

            var finalOperations = new List<MigrationOperation>();
            foreach (var operation in operations)
            {
                finalOperations.AddRange(TransformOperation(operation, model));
            }
            return finalOperations.AsReadOnly();
        }

        protected abstract IList<MigrationOperation> TransformOperation(MigrationOperation operation, IModel model);
    }
}
