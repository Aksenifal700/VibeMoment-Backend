VibeMoment 
Social media photo sharing API built with Clean Architecture

# Overview:

VibeMoment is a modern RESTful Web API for photo sharing social media platform. Built following Clean Architecture principles with Dependency Inversion Pattern, ensuring scalable, maintainable, and testable codebase.

Architecture
The project implements Clean Architecture with dependency injection and clear separation of concerns

# Design Patterns

Clean Architecture - Dependency rule with clear layer separation

Repository Pattern - Data access abstraction

Service Layer Pattern - Business logic encapsulation

Dependency Inversion Principle - Abstractions over implementations

DTO Pattern - Safe data transfer between layer

# 🛠️ Tech Stack

ASP.NET Core 9.0 - Web API framework

Entity Framework Core - ORM for database operations

PostgreSQL - Primary database

ASP.NET Identity - Authentication & authorization

AutoMapper - Object-to-object mapping

Swagger/OpenAPI - Interactive API documentation

# 📋 API Endpoints

Authentication

httpPOST /api/auth/register    # User registration

POST /api/auth/signin      # User login  

POST /api/auth/signout     # User logout

Photos

httpPOST   /api/photos/upload           # Upload new photo

GET    /api/photos/{id}             # Get photo by ID

PUT    /api/photos/{id}             # Update photo metadata

DELETE /api/photos/{id}             # Delete photo

GET    /api/photos/user/{userId}    # Get user's photos

# 🏃‍♂️ Getting Started

Prerequisites

Docker & Docker Compose (recommended)

Git

# 🐳 Option 1: Docker (Recommended)

1.Clone the repository:

bashgit clone https://github.com/Aksenifal700/VibeMoment.git
cd VibeMoment

2.Run with Docker Compose:

bash docker-compose up -d

Access the application

API: http://localhost:8080

Swagger: http://localhost:8080/swagger

# 🧪 Development

Running Tests

dotnet test

Database Commands

# Add new migration
dotnet ef migrations add MigrationName --project VibeMoment.Infrastructure

# Update database
dotnet ef database update --project VibeMoment.Infrastructure

# Remove last migration
dotnet ef migrations remove --project VibeMoment.Infrastructure

# 📚 API Documentation
Interactive API documentation is available via Swagger UI:

Development: https://localhost:8080/swagger

OpenAPI Spec: https://localhost:8080/swagger/v1/swagger.json