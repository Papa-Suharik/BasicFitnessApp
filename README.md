# Basic Fitness-App

Workouts Managment API

![image alt](Images/Preview.png)

A RESTful backend application for managing user profiles, authentication and personalized workout generation.

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![OpenAI API](https://img.shields.io/badge/OpenAI_API-412991?style=for-the-badge&logo=openai&logoColor=white)

## Overview

Fitness-App is a backend application built with ASP.NET Core.

It provides user authentication, profile managment and workout generatio using OpenAi integration.

## Features

- User registration and authentication.
- JWT-based authentication.
- Refresh token management.
- User profile management.
- Database persistence with EF Core.
- AI-powered workout generation.
- Centralized exception handling.
- Request validation.

## Architecture

![image alt](Images/BasicFitnessApp.drawio.svg)

## Tech Stack

| Category | Technologies |
|:---:|:---:|
| Language | C# |
| Framework | ASP.NET Core |
| Authentication | JWT Bearer |
| Validation | Fluent Validation |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Integration | REST API, OpenAI API |
| API Documentation | Swagger |
| Logging | Serilog |
| Deployment | Docker, Docker Compose |

## Get Started 

```bash
git clone https://github.com/Papa-Suharik/BasicFitnessApp.git
cd BasicFitnessApp
```
Configure your environment variables and database connection, then run:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

### Configuration

| Variable | Description |
|:---|:---:|
| `OPEN_AI_APIKEY` | API Key to external OpenAI language model |
| `JWT_KEY` | Unqie Key used to sign the JWT|
| `JWT_ISSUER` | Identifies who created and signed the JWT |
| `JWT_AUDIENCE` | Identifies who means to accept the JWT |
| `JWT_EXPIRATION_MINUTES` | Identifies lifetime of the JWT |
| `DB_HOST` | PostgreSQL database host |
| `DB_PORT` | PostgreSQL database port |
| `DB_NAME` | PostgreSQL database name |
| `DB_USER` | PostgreSQL database username |
| `DB_PASSWORD` | PostgreSQL database password |

## Deployment

The application can be containerized and deployed using Docker Compose.


