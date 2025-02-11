    SELECT  current_database()as Table_Catalog,
            c.relname AS table_name,
            a.attname AS column_name,
            a.attnum AS ordinal_position,
            pg_get_expr(ad.adbin, ad.adrelid) AS column_default,
           (CASE WHEN a.attnotnull OR (t.typtype = 'd' AND t.typnotnull) THEN 'NO' ELSE 'YES' END) AS is_nullable,
           (CASE WHEN t.typtype = 'd' THEN
            CASE WHEN bt.typelem <> 0 AND bt.typlen = -1 THEN 'ARRAY'
                 WHEN nbt.nspname = 'pg_catalog' THEN format_type(t.typbasetype, null)
                 ELSE 'USER-DEFINED' END
            ELSE
            CASE WHEN t.typelem <> 0 AND t.typlen = -1 THEN 'ARRAY'
                 WHEN nt.nspname = 'pg_catalog' THEN format_type(a.atttypid, null)
            ELSE 'USER-DEFINED' END
            END) AS data_type,
            information_schema._pg_char_max_length(information_schema._pg_truetypid(a, t), information_schema._pg_truetypmod(a, t)) AS character_maximum_length,
            information_schema._pg_char_octet_length(information_schema._pg_truetypid(a, t), information_schema._pg_truetypmod(a, t)) AS character_octet_length,
            information_schema._pg_numeric_precision(information_schema._pg_truetypid(a, t), information_schema._pg_truetypmod(a, t)) AS numeric_precision,
            information_schema._pg_numeric_precision_radix(information_schema._pg_truetypid(a, t), information_schema._pg_truetypmod(a, t)) AS numeric_precision_radix,
            information_schema._pg_numeric_scale(information_schema._pg_truetypid(a, t), information_schema._pg_truetypmod(a, t)) AS numeric_scale,
            information_schema._pg_datetime_precision(information_schema._pg_truetypid(a, t), information_schema._pg_truetypmod(a, t)) AS datetime_precision,
            (CASE WHEN (information_schema._pg_char_max_length(information_schema._pg_truetypid(a, t), information_schema._pg_truetypmod(a, t))) IS NULL THEN (information_schema._pg_numeric_precision(information_schema._pg_truetypid(a, t), information_schema._pg_truetypmod(a, t))) ELSE (information_schema._pg_char_max_length(information_schema._pg_truetypid(a, t), information_schema._pg_truetypmod(a, t))) END) as COLUMN_LENGTH,
            0 as is_computed,
            0 as is_identity,
            0 as is_rowguidcol
    FROM (pg_attribute a LEFT JOIN pg_attrdef ad ON attrelid = adrelid AND attnum = adnum),
          pg_class c, pg_namespace nc,
         (pg_type t JOIN pg_namespace nt ON (t.typnamespace = nt.oid))
           LEFT JOIN (pg_type bt JOIN pg_namespace nbt ON (bt.typnamespace = nbt.oid))
           ON (t.typtype = 'd' AND t.typbasetype = bt.oid)
    WHERE a.attrelid = c.oid
    AND a.atttypid = t.oid
    AND nc.oid = c.relnamespace
    AND (NOT pg_is_other_temp_schema(nc.oid))
    AND c.relname = '#TableName#'
    AND a.attnum > 0 AND NOT a.attisdropped AND c.relkind in ('r', 'v')
    AND nc.nspname='public'
    AND (pg_has_role(c.relowner, 'USAGE')
    OR has_table_privilege(c.oid, 'SELECT')
    OR has_table_privilege(c.oid, 'INSERT')
    OR has_table_privilege(c.oid, 'UPDATE')
    OR has_table_privilege(c.oid, 'REFERENCES') )
    Order By a.attnum