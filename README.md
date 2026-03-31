# Backend Test - Myoffice_ACPD Management API

This is a RESTful Web API built with .NET 8 for managing the `MyOffice_ACPD` database. This project was developed as part of a technical assessment for a Backend Engineer position.

## Tech Stack
- **Framework:** .NET 8 Web API
- **Database:** SQL Server
- **ORM:** Dapper
- **Documentation:** Swagger UI

## Project Structure
- `Controllers/`: Contains the `MyofficeacpdController` with full CRUD endpoints.
- `Models/`: Contains the `Acpd.cs` data model mapping to the SQL table.
- `Myoffice_ACPD.bak`: SQL Server database backup file for restoration.

## Setup Instructions
1. **Database Restoration:**
   - Open SQL Server Management Studio (SSMS).
   - Right-click "Databases" -> "Restore Database".
   - Select "Device" and choose the `Myoffice_ACPD.bak` file provided in this repository.
2. **Connection String:**
   - Open `appsettings.json`.
   - Update the `DefaultConnection` string with your local SQL Server credentials.
3. **Running the App:**
   - Open `Lisye_BackendTest_MidLevel.sln` in Visual Studio 2022.
   - Press **F5** to run the application.
   - Swagger UI will automatically open at `/swagger/index.html`.

## API Endpoints
- `GET /api/Myofficeacpd`: Retrieve all records.
- `GET /api/Myofficeacpd/{id}`: Retrieve a single record by SID.
- `POST /api/Myofficeacpd`: Create a new record.
- `PUT /api/Myofficeacpd/{id}`: Update an existing record.
- `DELETE /api/Myofficeacpd/{id}`: Delete a record.