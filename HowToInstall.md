## How to Install

### 1. Clone the Repository

```bash
git clone https://github.com/anduenescu/OrderManagementApp.git
cd OrderManagementApp
## Getting Started
````

### 2. Open the Project

You can open the solution using:

- **Visual Studio**
- **Visual Studio Code** with C# extensions

Make sure you have the following installed:

- .NET 6.0 or later
- SQL Server Express or Developer Edition
- SSMS (SQL Server Management Studio) or Azure Data Studio

---

### 3. Set Up the Database

**Run the Script Manually**

1. Go to the `Database` folder.
2. Open `OrderManagementPlatform_schema.sql` in SSMS.
3. Execute the full script.

This will:

- Create the `OrderManagementPlatform` database
- Create all necessary tables
- Seed categories, products, users, and a sample order
---
#### Admin login for testing:

- **Username**: `admin`  
- **Password**: `adminpassword` (Already hashed in the script)

> If the database already exists, skip the `CREATE DATABASE` part and only run the insert statements.

---
### 4. Configure the Connection String

In `OrderManagementApp/appsettings.json`, update the connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=YOUR_SERVER;Initial Catalog=OrderManagementPlatform;Integrated Security=True;Trust Server Certificate=True"
}
##The project currently points to a local SQL Server instance used for development/testing.
```
---

### 5. Run the App

You can run the app using:

```bash
dotnet run
```
Or simply press F5 in Visual Studio.

The app will be hosted at:
`https://localhost:{port}`

---
### 6. Get a JWT Token
Use Postman or REST Client to send a login request:

POST `/api/auth/login`
```json
{
  "username": "admin",
  "password": "adminpassword"
}
```
Use the returned token in all subsequent requests like this:
`Authorization: Bearer {your_token_here}`
7. Try Key Endpoints
- Method	Endpoint	Description
- GET	`/api/products/status`	Public test endpoint
- GET	`/api/products`	List all products (JWT required)
- POST `/api/order/addorder`	Place a new order
- GET	`/api/admin/products`	Admin product list
- GET	`/api/admin/top-products`	Top-selling products
- GET	`/api/admin/monthly-sales`	Monthly sales report
