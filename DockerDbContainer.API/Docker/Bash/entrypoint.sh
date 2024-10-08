#!/bin/bash

# Start your background script
/waitforsqlserverandcreatedb.sh &

# Start SQL Server or any main service
/opt/mssql/bin/sqlservr