# ToDoApplicationAPI
ToDoApplicationAPI is a REST API dedicated to managing and applying CRUD operations on todos and their corresponding subtasks. The API supports authentication and authorization features and uses JWT Token for validating the user requests. The database for the API was implemented using Code First Approach of EF Core Framework. Additionally, the application archives modifications made on the entities and stores in an Audit Table.

## Architecture
The API follows clean architecture principles, separating Presentation(API Controllers),Application(Domains and Services of the API) and Infrastructure(Persistence - DB layer and Repositoy implementations) layers.

Besides, application follows SOLID principles and uses repository pattern.

## Additional Features
* Archive Log - Audit Table
* Database Seeding
* Global exception handler middleware
* Request and response logging middleware

## Technologies
* ASP.NET Core
* EF Core
* Swagger Documentation
* MSSQL Server
* Fluent Validation for request models
* Mapster
* JWT Token
