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
       "local": "Server=myServerAddress;Port=myPort;Uid=myUsername;Pwd=myPassword;database=myDatabase"
     }
     ```

2. **Configure API Key**:
   - In the `appsettings.json` file, locate the `ApiKey` section.
   - Update the `ApiKey` with your API key details:
     ```json
     "ApiKey": "YourActualApiKey"
     ```

   - Open the `ApiService.ApiService.cs` file in the WPF project.
   - Ensure the API key is set correctly within the constructor:
     ```csharp
     public ApiService()
     {
         _httpClient = new HttpClient
         {
             BaseAddress = new Uri("https://localhost:7259/"),
             // Add API key to the header.
             DefaultRequestHeaders =
             {
                 { "x-api-key", "YourActualApiKey" }
             }
         };
     }
     ```

3. **Initialize the Database**:
   - Ensure your MySQL server is running.
   - The application will check if the `myDatabase` database exists using:
     ```sql
     SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'myDatabase';
     ```
   - If the database does not exist, it will automatically create and initialize the database with the required tables and constraints.

### To-dos
- [x] MySql intergration
- [x] Server intergration
- [ ] WPF integration (in progress)
