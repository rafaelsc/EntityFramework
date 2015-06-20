// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit;

namespace Microsoft.Data.Entity.Metadata.ModelConventions
{
    public class PropertyAttributeConventionTest
    {
        public class A
        {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }

            [ConcurrencyCheck]
            public Guid RowVersion { get; set; }

            [Required]
            public string Name { get; set; }
        }

        [Fact]
        public void DatabaseGeneratedAnnotation_is_applied()
        {
            var modelBuilder = new ModelBuilder(new CoreConventionSetBuilder().CreateConventionSet());
            var entityType = modelBuilder.Entity<A>();

            Assert.Equal(StoreGeneratedPattern.None, entityType.Property(e => e.Id).Metadata.StoreGeneratedPattern);
        }

        [Fact]
        public void ConcurrencyCheckAnnotation_is_applied()
        {
            var modelBuilder = new ModelBuilder(new CoreConventionSetBuilder().CreateConventionSet());
            var entityType = modelBuilder.Entity<A>();

            Assert.True(entityType.Property(e => e.RowVersion).Metadata.IsConcurrencyToken);
        }

        [Fact]
        public void RequiredAnnotation_is_applied()
        {
            var modelBuilder = new ModelBuilder(new CoreConventionSetBuilder().CreateConventionSet());
            var entityType = modelBuilder.Entity<A>();

            Assert.False(entityType.Property(e => e.Name).Metadata.IsNullable);
        }
    }
}
