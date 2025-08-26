# Restaurant API Project

This is a comprehensive Restaurant API built with .NET Core, following the principles of Clean Architecture. It provides a robust backend for managing restaurants, dishes, and users, with a focus on separation of concerns, testability, and maintainability.

---

## 🚀 Features

* **Restaurant Management**: Perform full CRUD (Create, Read, Update, Delete) operations on restaurants.
* **Dish Management**: Manage dishes associated with specific restaurants.
* **User Authentication & Authorization**: Secure endpoints using JWT-based authentication and role-based authorization.
* **Pagination and Sorting**: Efficiently query and navigate through large datasets of restaurants.
* **Error Handling**: Centralized error handling middleware for consistent API responses.
* **Logging**: Middleware for logging request and response times.
* **Testing**: Includes unit and integration tests for different layers of the application.

---

## 🛠️ Technologies & Frameworks

* **.NET 8**: The core framework for building the application.
* **ASP.NET Core**: For building the RESTful API.
* **Entity Framework Core**: For data access and object-relational mapping (ORM).
* **MediatR**: To implement the CQRS (Command Query Responsibility Segregation) pattern.
* **AutoMapper**: For object-to-object mapping.
* **FluentValidation**: For validating incoming requests.
* **Serilog**: For structured logging.
* **Swagger/OpenAPI**: For API documentation and testing.
* **xUnit**: As the testing framework.
* **Moq**: For creating mock objects in tests.

---

## 🏛️ Project Structure

The project is organized into four main layers, following the principles of **Clean Architecture**:

* **`Restaurant.Domain`**: Contains the core business entities, enums, exceptions, and repository interfaces. This layer has no dependencies on other layers.
* **`Restaurant.Application`**: Implements the business logic of the application. It contains commands, queries, DTOs, and business logic services. It depends only on the `Domain` layer.
* **`Restaurant.Infrastructure`**: Provides implementations for the interfaces defined in the `Domain` layer. This includes data persistence with Entity Framework Core, repositories, and other external services. It depends on the `Application` and `Domain` layers.
* **`Restaurant.API`**: The entry point of the application. It exposes the API endpoints and handles HTTP requests and responses. It depends on the `Application` and `Infrastructure` layers.
* **`tests`**: Contains various test projects for the different layers, ensuring the quality and correctness of the code.

---

## 🏁 Getting Started

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or another compatible database)
* A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/m-mongy/restaurant.api_project.git](https://github.com/m-mongy/restaurant.api_project.git)
    cd restaurant.api_project
    ```

2.  **Configure the database connection:**
    * Open `src/Restaurant.API/appsettings.json`.
    * Modify the `ConnectionStrings` section with your database credentials.

3.  **Apply database migrations:**
    * Open a terminal in the `src/Restaurant.Infrastructure` directory.
    * Run the following command to apply the migrations and create the database schema:
        ```bash
        dotnet ef database update
        ```

4.  **Run the application:**
    * Open a terminal in the `src/Restaurant.API` directory.
    * Run the project:
        ```bash
        dotnet run
        ```

5.  **Access the API:**
    * The API will be running at `https://localhost:5001` (or the port specified in `launchSettings.json`).
    * Access the Swagger UI for API documentation and testing at `https://localhost:5001/swagger`.

---

## 🧪 Testing

The solution includes several test projects to ensure the code is working as expected. To run the tests, you can use the Test Explorer in Visual Studio or run the following command from the root directory:

```bash
dotnet test
