# Books API

## Short Description
The Books API is a robust RESTful web service built with ASP.NET Core, designed for managing a collection of books and providing secure user authentication. It features comprehensive CRUD (Create, Read, Update, Delete) operations for books, coupled with a JWT (JSON Web Token) based authentication system for user registration and login. The API is documented interactively using Swagger/OpenAPI.

## Project Tech Stack
*   **Language**: C#
*   **Framework**: ASP.NET Core
*   **Libraries**:
    *   **Microsoft.EntityFrameworkCore**: For efficient data access and management, including an in-memory database.
    *   **Microsoft.AspNetCore.Authentication.JwtBearer**: To enable JWT-based authentication and authorization.
    *   **BCrypt.Net**: For secure hashing of user passwords.
    *   **Swashbuckle.AspNetCore**: To generate and serve interactive API documentation (Swagger UI).

## Installation Instructions

### Prerequisites
*   .NET SDK 6.0 or higher

### Steps
1.  **Clone the repository:**
    ```bash
    git clone <repository_url>
    cd BooksApi.Api
    ```
    *(Navigate into the `BooksApi.Api` directory if you cloned the entire solution.)*
2.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```
3.  **Run the application:**
    ```bash
    dotnet run
    ```
    The API will typically start on `http://localhost:5084` and `https://localhost:7192`. The console output will provide the exact URLs.

## Usage Examples

Once the application is running, you can interact with the API using any HTTP client (e.g., Postman, curl, or directly via the provided Swagger UI).

### Accessing Swagger UI
Open your web browser and navigate to `https://localhost:7192/swagger` (or `http://localhost:5084/swagger`) to explore the API endpoints interactively and execute requests directly from the browser.

### Authentication Endpoints

1.  **Register a User**
    *   **Method:** `POST`
    *   **Endpoint:** `/api/Auth/register`
    *   **Request Body (JSON):**
        ```json
        {
          "username": "newuser",
          "email": "user@example.com",
          "password": "SecurePassword1!",
          "confirmPassword": "SecurePassword1!"
        }
        ```
    *   **Success Response:** `201 Created` with the newly registered user's public details.

2.  **Login User**
    *   **Method:** `POST`
    *   **Endpoint:** `/api/Auth/login`
    *   **Request Body (JSON):**
        ```json
        {
          "email": "user@example.com",
          "password": "SecurePassword1!"
        }
        ```
    *   **Success Response:** `200 OK` with a JWT token. This token must be included in the `Authorization` header for all protected book management endpoints.

### Book Management Endpoints (Require Authentication)

For all book management operations, include the JWT token obtained from the login endpoint in the `Authorization` header in the format: `Authorization: Bearer <YOUR_JWT_TOKEN>`.

1.  **Get All Books**
    *   **Method:** `GET`
    *   **Endpoint:** `/api/Books`
    *   **Optional Query Parameter:** `searchTerm` (e.g., `/api/Books?searchTerm=Things`)
    *   **Success Response:** `200 OK` with a list of book objects.

2.  **Get Book by ID**
    *   **Method:** `GET`
    *   **Endpoint:** `/api/Books/{id}` (e.g., `/api/Books/1`)
    *   **Success Response:** `200 OK` with the specific book details, or `404 Not Found` if the book does not exist.

3.  **Create a New Book**
    *   **Method:** `POST`
    *   **Endpoint:** `/api/Books`
    *   **Request Body (JSON):**
        ```json
        {
          "title": "The Name of the Wind",
          "author": "Patrick Rothfuss",
          "publicationYear": 2007
        }
        ```
    *   **Success Response:** `201 Created` with the details of the newly added book.

4.  **Update an Existing Book**
    *   **Method:** `PUT`
    *   **Endpoint:** `/api/Books/{id}` (e.g., `/api/Books/1`)
    *   **Request Body (JSON):**
        ```json
        {
          "title": "Updated Book Title",
          "author": "Updated Author Name",
          "publicationYear": 1980
        }
        ```
    *   **Success Response:** `204 No Content` if the update is successful, or `404 Not Found` if the book does not exist.

5.  **Delete a Book**
    *   **Method:** `DELETE`
    *   **Endpoint:** `/api/Books/{id}` (e.g., `/api/Books/1`)
    *   **Success Response:** `204 No Content` if the deletion is successful, or `404 Not Found` if the book does not exist.

## Features Overview

*   **RESTful API Design**: Adheres to standard REST principles, providing clear and consistent endpoints for resource manipulation.
*   **User Authentication & Authorization**: Implements a secure JWT-based system for user management, including registration, login, and authorization checks for protected routes. Passwords are securely hashed using BCrypt.
*   **Comprehensive Book Management**: Offers a full suite of CRUD operations (Create, Read, Update, Delete) for books, including the ability to search by title or author.
*   **Robust Data Validation**: Leverages data annotations on DTOs (Data Transfer Objects) to ensure that incoming request data meets defined validation rules, enhancing data integrity and API reliability.
*   **In-Memory Database**: Utilizes Entity Framework Core's in-memory database provider for easy setup and rapid development, with pre-seeded initial book data.
*   **Dependency Injection**: Follows ASP.NET Core's built-in dependency injection pattern for managing service dependencies and promoting modular, testable code.
*   **Interactive API Documentation**: Integrated Swagger/OpenAPI UI provides a user-friendly interface to explore and test API endpoints directly in the browser.

## File Structure Summary

The project follows a standard ASP.NET Core API structure for clarity and maintainability:

```
BooksApi.Api/
├── Controllers/             # API endpoints for authentication and book management (e.g., AuthController, BooksController)
├── Data/                    # Database context (ApplicationDbContext) defining entity sets and model configurations
├── Models/                  # Data models (Book, User) and Data Transfer Objects (DTOs) for API requests/responses
├── Services/                # Business logic and data access implementations (e.g., AuthService, BookService)
│   └── Interfaces/          # Interfaces for service contracts (IAuthService, IBookService)
├── appsettings.json         # Main application configuration, including JWT settings
├── appsettings.Development.json # Environment-specific configuration for development
├── Program.cs               # Application entry point, responsible for configuring services, middleware, and the HTTP request pipeline
├── Properties/              # Project-level properties, including launch profiles for development
└── .gitignore               # Standard Git ignore file for common .NET build artifacts and temporary files
```

## Important Notes

*   **In-Memory Database for Development**: This project is configured to use an in-memory database for convenience during development. This means all data is volatile and will be lost each time the application restarts. For production deployment, you would typically switch to a persistent database solution (e.g., SQL Server, PostgreSQL, MySQL) by modifying the `ApplicationDbContext` configuration. A commented-out MySQL configuration is present in `Program.cs` as an example.
*   **JWT Key Security**: The `Jwt:Key` in `appsettings.json` is provided for immediate development use. In a production environment, this key must be a strong, cryptographically secure secret that is stored and managed securely (e.g., via environment variables, cloud key management services like Azure Key Vault, AWS Secrets Manager) and never hardcoded or committed to version control.
*   **Error Handling and Logging**: While basic error handling is present, a production-ready application would benefit from more comprehensive global error handling middleware, detailed logging, and structured exception handling.
*   **Data Validation Review**: The `PublicationYear` validation in `BookUpdateDto` is currently capped at `2000`. Ensure that this range is updated and aligned with current business requirements or the actual current year for proper validation.
*   **HTTPS Usage**: The API is configured to run on both HTTP and HTTPS. It is strongly recommended to use HTTPS in production environments for secure communication.