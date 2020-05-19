DATABASE_NAME="MyDatabase"
DB_DUMP_LOCATION="/tmp/psql_data/create-table.sql"

psql -U postgres -d $DATABASE_NAME -a -f $DB_DUMP_LOCATION