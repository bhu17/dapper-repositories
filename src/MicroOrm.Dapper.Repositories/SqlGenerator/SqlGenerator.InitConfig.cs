using System;
using MicroOrm.Dapper.Repositories.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;

namespace MicroOrm.Dapper.Repositories.SqlGenerator
{
    /// <inheritdoc />
    public partial class SqlGenerator<TEntity>
        where TEntity : class
    {
        /// <summary>
        ///     Init type Sql provider
        /// </summary>
        private void InitConfig()
        {
            FilterData = new FilterData();
            
            if (MicroOrmConfig.UseQuotationMarks)
            {
                switch (MicroOrmConfig.SqlProvider)
                {
                    case SqlProvider.MSSQL:
                        TableName = GetTableNameWithSchemaPrefix(TableName, TableSchema, "[", "]");

                        foreach (var propertyMetadata in SqlProperties)
                            propertyMetadata.ColumnName = "[" + propertyMetadata.ColumnName + "]";

                        foreach (var propertyMetadata in KeySqlProperties)
                            propertyMetadata.ColumnName = "[" + propertyMetadata.ColumnName + "]";

                        foreach (var propertyMetadata in SqlJoinProperties)
                        {
                            propertyMetadata.TableName = GetTableNameWithSchemaPrefix(propertyMetadata.TableName, propertyMetadata.TableSchema, "[", "]");
                            propertyMetadata.ColumnName = "[" + propertyMetadata.ColumnName + "]";
                            propertyMetadata.TableAlias = "[" + propertyMetadata.TableAlias + "]";
                        }

                        if (IdentitySqlProperty != null)
                            IdentitySqlProperty.ColumnName = "[" + IdentitySqlProperty.ColumnName + "]";

                        break;

                    case SqlProvider.MySQL:
                        TableName = GetTableNameWithSchemaPrefix(TableName, TableSchema, "`", "`");

                        foreach (var propertyMetadata in SqlProperties)
                            propertyMetadata.ColumnName = "`" + propertyMetadata.ColumnName + "`";

                        foreach (var propertyMetadata in KeySqlProperties)
                            propertyMetadata.ColumnName = "`" + propertyMetadata.ColumnName + "`";

                        foreach (var propertyMetadata in SqlJoinProperties)
                        {
                            propertyMetadata.TableName = GetTableNameWithSchemaPrefix(propertyMetadata.TableName, propertyMetadata.TableSchema, "`", "`");
                            propertyMetadata.ColumnName = "`" + propertyMetadata.ColumnName + "`";
                            propertyMetadata.TableAlias = "`" + propertyMetadata.TableAlias + "`";
                        }

                        if (IdentitySqlProperty != null)
                            IdentitySqlProperty.ColumnName = "`" + IdentitySqlProperty.ColumnName + "`";

                        break;

                    case SqlProvider.PostgreSQL:
                        TableName = GetTableNameWithSchemaPrefix(TableName, TableSchema, "\"", "\"");

                        foreach (var propertyMetadata in SqlProperties)
                            propertyMetadata.ColumnName = "\"" + propertyMetadata.ColumnName + "\"";

                        foreach (var propertyMetadata in KeySqlProperties)
                            propertyMetadata.ColumnName = "\"" + propertyMetadata.ColumnName + "\"";

                        foreach (var propertyMetadata in SqlJoinProperties)
                        {
                            propertyMetadata.TableName = GetTableNameWithSchemaPrefix(propertyMetadata.TableName, propertyMetadata.TableSchema, "\"", "\"");
                            propertyMetadata.ColumnName = "\"" + propertyMetadata.ColumnName + "\"";
                            propertyMetadata.TableAlias = "\"" + propertyMetadata.TableAlias + "\"";
                        }

                        if (IdentitySqlProperty != null)
                            IdentitySqlProperty.ColumnName = "\"" + IdentitySqlProperty.ColumnName + "\"";

                        break;
                    case SqlProvider.SQLite:
                        //SQLite doesn't use it.
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException(nameof(MicroOrmConfig.SqlProvider));
                }
            }
            else
            {
                TableName = GetTableNameWithSchemaPrefix(TableName, TableSchema);
                foreach (var propertyMetadata in SqlJoinProperties)
                    propertyMetadata.TableName = GetTableNameWithSchemaPrefix(propertyMetadata.TableName, propertyMetadata.TableSchema);
            }
        }
    }
}
