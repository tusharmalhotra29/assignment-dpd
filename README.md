# Project1


# Backend System Assignment

This repository contains the code for a backend system developed using ASP.NET Core 6. The backend system includes APIs for user registration, token generation, storing data, retrieving data, updating data, and deleting data.

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

## Installation

1. Clone the repository to your local machine:

---bash
git clone https://github.com/your-username/backend-system.git


1.Navigate to the project directory:
cd backend-system

2.Build the project:
dotnet build

3.Run the application:
dotnet run

Endpoints
User Registration
POST /api/users/register
Request Body:
{
  "username": "example_user",
  "email": "user@example.com",
  "password": "secure_password123",
  "fullName": "John Doe",
  "age": 30,
  "gender": "male"
}

Success Response:
{
  "status": "success",
  "message": "User registered successfully."
}


Generate Token
POST /api/token
Request Body:
{
  "username": "example_user",
  "password": "secure_password123"
}

Success Response:
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}


Store Data
POST /api/data
Request Body:
{
  "key": "data_key",
  "value": "data_value"
}

Success Response:
{
  "status": "success",
  "message": "Data stored successfully."
}


Retrieve Data
GET /api/data/{key}
Success Response:
{
  "key": "data_key",
  "value": "data_value"
}


Update Data
PUT /api/data/{key}
Request Body:
{
  "value": "updated_data_value"
}

Success Response:
{
  "status": "success",
  "message": "Data updated successfully."
}

Delete Data
DELETE /api/data/{key}
Success Response:
{
  "status": "success",
  "message": "Data deleted successfully."
}


Error Codes
INVALID_KEY: The provided key is not valid or missing.
INVALID_VALUE: The provided value is not valid or missing.
KEY_EXISTS: The provided key already exists in the database. To update an existing key, use the update API.
INVALID_TOKEN: Invalid access token provided.
