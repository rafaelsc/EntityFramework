// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Operations;
using Microsoft.Data.Entity.Relational.Migrations.Sql;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Sqlite.Migrations
{
    public class SqliteMigrationSqlGenerator : MigrationSqlGenerator
    {
        private readonly ISqlGenerator _sql;
        private readonly MigrationOperationTransformer _transformer;

        public SqliteMigrationSqlGenerator(
            [NotNull] ISqlGenerator sqlGenerator,
            [CanBeNull] MigrationOperationTransformer transformer)
            : base(sqlGenerator)
        {
            _sql = sqlGenerator;
            _transformer = transformer;
        }

        public override IReadOnlyList<SqlBatch> Generate(IReadOnlyList<MigrationOperation> operations, IModel model = null)
        {
            Check.NotNull(operations, nameof(operations));

            if (_transformer != null)
            {
                operations = _transformer.Transform(operations, model);
            }

            return base.Generate(operations, model);
        }

        public override void Generate(DropIndexOperation operation, IModel model, SqlBatchBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            builder
                .Append("DROP INDEX ")
                .Append(_sql.DelimitIdentifier(operation.Name));
        }

        public override void Generate(RenameTableOperation operation, IModel model, SqlBatchBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (operation.NewName != null)
            {
                builder
                    .Append("ALTER TABLE ")
                    .Append(_sql.DelimitIdentifier(operation.Name))
                    .Append(" RENAME TO ")
                    .Append(_sql.DelimitIdentifier(operation.NewName));
            }
        }

        public virtual void Generate([NotNull] MoveDataOperation operation, [CanBeNull] IModel model, [NotNull] SqlBatchBuilder builder)
        {
            Check.NotNull(operation, nameof(operation));
            Check.NotNull(builder, nameof(builder));

            if (operation.Columns?.Length == 0)
            {
                return;
            }

            var columnList = string.Join(", ", operation.Columns.Select(s => _sql.DelimitIdentifier(s)));

            builder.Append("INSERT INTO ")
                .Append(_sql.DelimitIdentifier(operation.NewTable))
                .Append(" (")
                .Append(columnList)
                .AppendLine(")")
                .Append("SELECT ")
                .Append(columnList)
                .Append(" FROM ")
                .Append(_sql.DelimitIdentifier(operation.OldTable));
        }

        #region Invalid migration operations

        // These operations can be accomplished instead with a table-rebuild
        public override void Generate(AddForeignKeyOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.InvalidMigrationOperation);
        }

        public override void Generate(AddPrimaryKeyOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.InvalidMigrationOperation);
        }

        public override void Generate(AddUniqueConstraintOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.InvalidMigrationOperation);
        }

        public override void Generate(DropColumnOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.InvalidMigrationOperation);
        }

        public override void Generate(DropForeignKeyOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.InvalidMigrationOperation);
        }

        public override void Generate(DropPrimaryKeyOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.InvalidMigrationOperation);
        }

        public override void Generate(DropUniqueConstraintOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.InvalidMigrationOperation);
        }

        public override void Generate(RenameColumnOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.InvalidMigrationOperation);
        }

        public override void Generate(RenameIndexOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.InvalidMigrationOperation);
        }

        public override void Generate(AlterColumnOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.InvalidMigrationOperation);
        }

        #endregion

        #region Invalid schema operations

        public override void Generate(CreateSchemaOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.SchemasNotSupported);
        }

        public override void Generate(DropSchemaOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.SchemasNotSupported);
        }

        #endregion

        #region Sequences not supported

        // SQLite does not have sequences
        public override void Generate(RestartSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.SequencesNotSupported);
        }

        public override void Generate(CreateSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.SequencesNotSupported);
        }

        public override void Generate(RenameSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.SequencesNotSupported);
        }

        public override void Generate(AlterSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.SequencesNotSupported);
        }

        public override void Generate(DropSequenceOperation operation, IModel model, SqlBatchBuilder builder)
        {
            throw new NotSupportedException(Strings.SequencesNotSupported);
        }

        #endregion
    }
}
