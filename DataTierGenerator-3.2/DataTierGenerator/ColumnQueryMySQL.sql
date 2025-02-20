SELECT INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME,
INFORMATION_SCHEMA.COLUMNS.DATA_TYPE,
INFORMATION_SCHEMA.COLUMNS.NUMERIC_PRECISION,
INFORMATION_SCHEMA.COLUMNS.NUMERIC_SCALE,
INFORMATION_SCHEMA.COLUMNS.IS_Nullable,
INFORMATION_SCHEMA.COLUMNS.CHARACTER_OCTET_LENGTH As CHARACTER_MAXIMUM_LENGTH,
	COALESCE(CHARACTER_OCTET_LENGTH,0) AS COLUMN_LENGTH,
	0 AS IS_COMPUTED,
	0 AS IS_IDENTITY,
	0 AS IS_ROWGUIDCOL
FROM
	INFORMATION_SCHEMA.COLUMNS
WHERE
	INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '#TableName#'