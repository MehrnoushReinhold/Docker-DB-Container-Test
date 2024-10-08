# Configuration
DB_NAME="WeatherForecast"
MAX_RETRIES=60
DELAY=1  # Delay in seconds 
SQL_FILES_DIR="/sql"  # Directory containing your SQL files

# Function to execute SQL commands using sqlcmd
  execute_sql() {
  /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "Your_password123" -C -Q "$1"
}

# Check if SQL Server is ready
attempt=0
while [ $attempt -lt $MAX_RETRIES ]; do
  echo "Checking if SQL Server is ready... Attempt $(($attempt + 1)) of $MAX_RETRIES"

if execute_sql "SELECT 1 FROM sys.databases WHERE name = 'master'"; then
  echo "SQL Server is ready. Creating database '$DB_NAME'..."
break
else
echo "SQL Server is not ready yet. Retrying in $DELAY seconds..."
  ((attempt++))
sleep $DELAY
  fi
done

if [ $attempt -eq $MAX_RETRIES ]; then
  echo "Failed to connect to SQL Server after $MAX_RETRIES attempts."
exit 1
fi

# Create the new database
  execute_sql "CREATE DATABASE $DB_NAME;"
if [ $? -eq 0 ]; then
  echo "Database '$DB_NAME' created successfully."

else
echo "Failed to create database '$DB_NAME'."
fi

# Loop through all SQL files in the specified directory and execute them
for sql_file in "$SQL_FILES_DIR"/*.sql; do
    if [ -f "$sql_file" ]; then  # Check if it is a file
        echo "Executing SQL file: $sql_file"
        /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "Your_password123" -d $DB_NAME -C -i $sql_file
        if [ $? -eq 0 ]; then
            echo "Successfully executed: $sql_file"
        else
            echo "Failed to execute: $sql_file"
        fi
    else
        echo "$sql_file is not a valid file."
    fi
done
