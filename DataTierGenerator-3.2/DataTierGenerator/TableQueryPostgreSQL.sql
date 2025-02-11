SELECT current_database()AS TABLE_CATALOG, relname AS TABLE_NAME
FROM pg_class WHERE relname='#TableName#'
