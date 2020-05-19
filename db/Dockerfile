FROM postgres

WORKDIR /
RUN mkdir -p /tmp/psql_data/

COPY sql/create-table.sql /tmp/psql_data/
COPY scripts/init-docker-postgres.sh /docker-entrypoint-initdb.d/
