# eCommerce

## Overview
This is a .NET Web API project that provides the backend functionality for an e-commerce application. The project utilizes various design patterns and techniques to ensure a robust and scalable architecture.

## Features
The project includes the following key features:

### Authentication and Authorization
- Implement secure authentication using JWT (JSON Web Token) tokens.
- Handle user registration and authentication with encrypted passwords.
- Authorize access to endpoints based on user roles and permissions.

### Product Management
- Implement CRUD operations for managing products using the Repository Pattern.
- Retrieve all products, get a specific product, create new products, update existing products, and delete products.

### Category Management
- Manage product categories using the Repository Pattern:
  - Create, update, and delete categories.
  
### Order Management
- Implement order-related operations using the Repository Pattern:
  - Retrieve orders, create new orders, and delete orders.

### Cart Management
- Manage the user's shopping cart:
  - Retrieve products in the cart and remove products from the cart.

## Techniques Used
- **Repository Pattern**: Organize data access logic using repositories, providing a centralized approach to data operations.
- **JWT Bearer Tokens**: Securely authenticate API requests using JSON Web Tokens, ensuring authorized access to protected endpoints.
- **Authorization**: Control access to resources based on user roles and permissions, enhancing application security.

## Technologies Used
- **.NET Web API**: Framework for building HTTP services that can be accessed by a variety of clients.
- **C#**: Programming language used for developing backend logic.
- **Entity Framework**: ORM (Object-Relational Mapping) framework for database interaction.
- **JWT Bearer Authentication**: Middleware for authenticating API requests using JWT tokens.
- **ASP.NET Core Identity**: Framework for managing users, roles, and authentication.

