# 🔐 Week 4 — Day 3: Authorization & Role-Based Access Control

![.NET 10](https://img.shields.io/badge/.NET%2010-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)
![Identity](https://img.shields.io/badge/ASP.NET%20Core%20Identity-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Postman](https://img.shields.io/badge/Postman-FF6C37?style=flat-square&logo=postman&logoColor=white)

## 📌 Overview

Implemented **Authorization and Role-Based Access Control** using ASP.NET Core Identity and JWT.

### Features

- `[Authorize]` protected endpoints
- User and Admin roles
- Role claims inside JWT
- Claims-Based Authorization
- Policy-Based Authorization
- Protected API testing with Postman
- Automatic JWT reuse in Postman

---

## 🔒 Protected Routes

The `BooksController` is protected using:

```csharp
[Authorize]
[ApiController]
[Route("api/[controller]")]
```

Requests without a valid JWT are rejected.

```text
Without Token → 401 Unauthorized
With Valid JWT → 200 OK
```

---

## 👥 Roles

Two roles were created:

```text
User
Admin
```

Roles are managed using ASP.NET Core Identity:

```csharp
UserManager<IdentityUser>
RoleManager<IdentityRole>
```

Test users were created and assigned different roles.

---

## 🎫 Role Claims

During login, the user's roles are retrieved:

```csharp
var roles = await _userManager.GetRolesAsync(user);
```

Then added to the JWT:

```csharp
claims.AddRange(
    roles.Select(role =>
        new Claim(ClaimTypes.Role, role))
);
```

This allows ASP.NET Core to identify the user's roles from the JWT.

---

## 📜 Policy-Based Authorization

A named policy was registered in `Program.cs`:

```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanManageBooks", policy =>
    {
        policy.RequireClaim("Permission", "ManageBooks");
    });
});
```

Admin users receive the required permission claim:

```csharp
if (roles.Contains("Admin"))
{
    claims.Add(new Claim("Permission", "ManageBooks"));
}
```

The policy can then be applied using:

```csharp
[Authorize(Policy = "CanManageBooks")]
```

---

## 🧪 Temporary Seed Endpoint

A temporary endpoint was added to create the test roles and users:

```http
POST /api/Auth/seed-users
```

It calls the `IdentitySeeder` to create:

- `User` role
- `Admin` role
- Test users
- Role assignments

After the database was seeded successfully, the endpoint was **commented out** because it was only needed for development and testing.

---

## 📮 Postman Testing

### Without Token

```text
Auth → No Auth
```

Result:

```text
401 Unauthorized
```

### With JWT

```text
Auth → Bearer Token
Token → {{token}}
```

Result:

```text
200 OK
```

The login response token is automatically stored in the Postman environment:

```javascript
const response = pm.response.json();

pm.environment.set("token", response.token);
```

Protected requests then reuse:

```text
{{token}}
```

---

## 🔄 Authorization Flow

```text
Login
  ↓
Identity verifies user
  ↓
Get user roles
  ↓
Create JWT
  ↓
Add roles / claims
  ↓
Bearer Token
  ↓
[Authorize]
  ↓
Authorization
  ↓
Allow / Reject request
```

---

## 📁 Project Structure

```text
HelloBinX-CRUD_Operations/
├── Controllers/
│   ├── AuthController.cs
│   └── BooksController.cs
├── Services/
│   ├── AuthService.cs
│   └── Interfaces/
├── Data/
│   └── AppDbContext.cs
├── Models/
├── Migrations/
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```

---

## 🛠️ Tools

**ASP.NET Core Identity · JWT · SQL Server · Postman**

---

## ✅ Day 3 Completion Checklist

- [x] Protect CRUD controller with `[Authorize]`
- [x] Create `User` and `Admin` roles
- [x] Assign roles to test users
- [x] Add roles to JWT claims
- [x] Implement Claims-Based Authorization
- [x] Define a named authorization policy
- [x] Test protected endpoints
- [x] Capture JWT automatically in Postman
- [x] Reuse JWT for protected requests
- [x] Verify `401` without a token
- [x] Verify `200 OK` with a valid token
- [x] Comment out the temporary seed endpoint

---

## 🏁 Day 3 Complete

Successfully implemented **JWT-based Authorization, Roles, Claims, and Policy-Based Authorization** and verified protected endpoints using Postman.
