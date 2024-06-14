## Tech Demo

### Overview

This tech demo showcases the integration of a MySQL server API with a WPF application, emphasizing database management and interaction.
The demo includes automatic initialization of the MySQL database.

### Key Features
- Automatic Database Initialization: Sets up the MySQL database with necessary tables and constraints upon launch.
- SalesUsers Table: Stores information on sales personnel with unique user IDs.
- Districts Table: Lists various sales districts.
- SecondaryRoles Table: Contains additional roles assigned to users.
- Data Integrity: Implements foreign key relationships and role constraints to ensure valid data entries.

#### How to Run
1. **Setup MySQL Connection**:
   - Open the `appsettings.json` file.
   - Locate the `ConnectionStrings` section.
   - Set the `local` connection string to your MySQL database details:
     ```json
     "ConnectionStrings": {
       "local": "Server=myServerAddress;Port=myPort;Uid=myUsername;Pwd=myPassword;database=TechDemo"
     }
     ```

2. **Initialize the Database**:
   - Ensure your MySQL server is running.
   - The application will automatically initialize the database with the required tables and constraints upon first run.


### To-dos
- [x] MySql intergration
- [x] Server intergration
- [ ] WPF integration (in progress)
