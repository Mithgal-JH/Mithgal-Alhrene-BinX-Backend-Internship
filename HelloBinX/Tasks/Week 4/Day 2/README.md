# 🔐 Week 4 — Day 2: JWT Authentication

![.NET 10](https://img.shields.io/badge/.NET%2010-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)
![Postman](https://img.shields.io/badge/Postman-FF6C37?style=flat-square&logo=postman&logoColor=white)

## 📌 Overview

Implemented **Login and JWT Authentication** using ASP.NET Core Identity.

### Features

- User Registration
- User Login
- JWT generation
- JWT Bearer Authentication
- User ID & Email claims
- 15-minute token expiration

## 📡 Endpoints

### Register

```http
POST /api/Auth/register
```

### Login

```http
POST /api/Auth/login
```

Successful response:

```json
{
  "token": "JWT_TOKEN"
}
```

## 🧪 Login Tests

### 🟢 Successful Login

```json
{
  "email": "mithgaljamal@gmail.com",
  "password": "Mithgal_123"
}
```

**Result:** `200 OK` + JWT token ✅

### 🔴 Invalid Password

```json
{
  "email": "mithgaljamal@gmail.com",
  "password": "WrongPassword123"
}
```

**Result:** `401 Unauthorized` ✅

### 🔴 Non-Existing User

```json
{
  "email": "notfound@example.com",
  "password": "Password123!"
}
```

**Result:** `401 Unauthorized` ✅

## 🔎 JWT Verification

Verified claims:

| Claim | Purpose |
|---|---|
| `sub` | User ID |
| `email` | User Email |
| `iss` | `HelloBinX` |
| `aud` | `HelloBinXClient` |
| `exp` | Expiration |

**Expired token:** Rejected successfully ✅

## 🧱 Structure

```text
Controllers/
└── AuthController.cs

Services/
├── AuthService.cs
└── Interfaces/
    └── IAuthService.cs

Program.cs
appsettings.Development.json
```

## 🛠️ Tools

**ASP.NET Core Identity · JWT · SQL Server · Postman**

## ✅ Day 2 Complete

All Day 2 hands-on requirements were implemented and tested successfully.
