# .NET Application

## Overview

# Task 1

This is a .NET application for managing orders. It connects to a PostgreSQL database and uses Entity Framework Core for data access.

## Getting Started

To get started with the OrderService application, follow these steps:

1. Clone the repository:

   ```
   git clone https://github.com/sdilnaz/OrderService.git
   ```

2. Navigate to the project directory:

   ```
   cd OrderService
   ```

3. Install the required dependencies:

   ```
   dotnet restore
   ```

4. Configure the database connection:

   - Open the `appsettings.json` file.
   - Update the `ConnectionStrings` section with your PostgreSQL database connection details.

5. Apply the database migrations:

   ```
   dotnet ef database update
   ```

6. Run the application:

   ```
   dotnet run
   ```

7. Open your web browser and navigate to `http://localhost:5021` to access the OrderService application and `http://localhost:5021/swagger` to check functionality.

## Usage

The OrderService application provides the following functionality:

- Create new orders
- Update existing orders
- Delete orders
- Retrieve order details (get all and get by id)
