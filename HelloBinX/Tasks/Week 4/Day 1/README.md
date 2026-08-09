# Week 4 --- Day 1

## ASP.NET Core Identity & User Registration

This project is the first day of Week 4 of the BinX Tech Backend .NET
Internship.

The focus of Day 1 was integrating **ASP.NET Core Identity** with the
existing Entity Framework Core application and implementing a **user
registration endpoint**.

------------------------------------------------------------------------

## Learning Objectives

-   Understand what ASP.NET Core Identity provides.
-   Integrate Identity with Entity Framework Core.
-   Extend the existing `AppDbContext` with `IdentityDbContext`.
-   Create the Identity database schema using EF Core migrations.
-   Configure `IdentityUser` and `IdentityRole`.
-   Implement user registration using `UserManager<IdentityUser>`.
-   Understand Identity password validation and secure password hashing.
-   Test registration success and validation/error scenarios using
    Postman.

The official Week 4 plan defines Day 1 as an 8-hour session focused on
ASP.NET Core Identity and user registration. fileciteturn3file5

------------------------------------------------------------------------

## Technologies Used

-   C#
-   ASP.NET Core Web API
-   ASP.NET Core Identity
-   Entity Framework Core
-   SQL Server
-   Postman
-   Git / GitHub

------------------------------------------------------------------------

## 1. ASP.NET Core Identity

ASP.NET Core Identity provides built-in functionality for:

-   User management
-   Password hashing
-   Role management
-   User/role relationships
-   Account-related functionality

Instead of creating a custom users table and implementing password
handling manually, Identity provides the required infrastructure on top
of Entity Framework Core. fileciteturn3file5

------------------------------------------------------------------------

## 2. Identity + Entity Framework Core

The existing `AppDbContext` was changed from:

``` csharp
public class AppDbContext : DbContext
```

to:

``` csharp
public class AppDbContext : IdentityDbContext<IdentityUser>
```

This allows the application to manage its existing entities together
with Identity entities.

Identity adds tables such as:

-   `AspNetUsers`
-   `AspNetRoles`
-   `AspNetUserRoles`
-   `AspNetUserClaims`
-   `AspNetUserLogins`
-   `AspNetUserTokens`
-   `AspNetRoleClaims`

The Week 4 training material specifically requires extending the
existing DbContext and generating the Identity schema through a
migration. fileciteturn3file5

------------------------------------------------------------------------

## 3. Identity Configuration

Identity was registered in `Program.cs` using:

``` csharp
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
```

This configures:

-   `IdentityUser` as the application user type.
-   `IdentityRole` as the role type.
-   `AppDbContext` as the Entity Framework Core store for Identity.

------------------------------------------------------------------------

## 4. Database Migration

After integrating Identity with `AppDbContext`, a migration was created
and applied:

``` powershell
dotnet ef migrations add AddIdentity
```

Then:

``` powershell
dotnet ef database update
```

This generated and applied the Identity schema alongside the existing
application tables.

------------------------------------------------------------------------

## 5. User Registration

A registration endpoint was implemented in `AuthController`.

### Endpoint

``` http
POST /api/Auth/register
```

### Request Body

``` json
{
  "email": "user@example.com",
  "password": "StrongPassword123!"
}
```

### User Creation

The endpoint creates an `IdentityUser`:

``` csharp
var user = new IdentityUser
{
    UserName = registerRequest.Email,
    Email = registerRequest.Email
};
```

The user is then created through Identity:

``` csharp
var result = await _userManager.CreateAsync(
    user,
    registerRequest.Password
);
```

`UserManager.CreateAsync` handles password processing and persistence.
The registration endpoint checks the returned `IdentityResult` and
returns either a successful response or the Identity validation errors.
fileciteturn3file5

------------------------------------------------------------------------

## 6. UserManager

`UserManager<IdentityUser>` is used to manage Identity users.

It is injected into the controller through Dependency Injection:

``` csharp
private readonly UserManager<IdentityUser> _userManager;

public AuthController(UserManager<IdentityUser> userManager)
{
    _userManager = userManager;
}
```

The important operation used on Day 1 is:

``` csharp
await _userManager.CreateAsync(user, password);
```

The returned `IdentityResult` is checked using:

``` csharp
if (!result.Succeeded)
{
    return BadRequest(result.Errors);
}
```

------------------------------------------------------------------------

## 7. Password Validation

Identity automatically validates passwords according to its configured
password policy.

During testing, a weak password produced validation errors such as:

-   `PasswordRequiresNonAlphanumeric`
-   `PasswordRequiresUpper`

A valid test password must satisfy the configured Identity requirements.

Identity also handles password hashing internally; custom password
hashing code is not implemented in this project. fileciteturn3file5

------------------------------------------------------------------------

## 8. Duplicate User Testing

The registration endpoint was tested twice with the same user
information.

The first request successfully created the user.

When the same user was registered again, Identity returned:

``` text
DuplicateUserName
```

This demonstrated that `CreateAsync` returns an unsuccessful
`IdentityResult` when the user cannot be created.

------------------------------------------------------------------------

## 9. Registration Responses

### Successful Registration

``` http
200 OK
```

Example:

``` json
{
  "message": "User registered successfully"
}
```

### Validation / Duplicate User

``` http
400 Bad Request
```

The response contains the Identity errors returned by:

``` csharp
result.Errors
```

------------------------------------------------------------------------

## 10. Postman Testing

The registration endpoint was tested using Postman.

### Valid Request

``` json
{
  "email": "user@example.com",
  "password": "StrongPassword123!"
}
```

Expected result:

``` text
200 OK
```

### Weak Password

A deliberately weak password was tested to verify Identity validation.

Expected result:

``` text
400 Bad Request
```

### Existing User

The same email/username was registered again.

Expected result:

``` text
400 Bad Request
```

with an Identity error such as:

``` text
DuplicateUserName
```

------------------------------------------------------------------------

## 11. Existing Book API

The Day 1 project is based on the existing CRUD API from Week 3 Day 5.

The existing Book API remains available, including:

-   Get all books
-   Get book by ID
-   Create book
-   Update book
-   Delete book
-   Not-found and invalid-input test cases

The API documentation was updated to include the new registration
endpoint.

------------------------------------------------------------------------

## 12. Project Structure

The relevant structure is:

``` text
Week 4/
└── Day 1/
    └── HelloBinX-CRUD_Operations/
        ├── Controllers/
        │   ├── AuthController.cs
        │   └── BooksController.cs
        ├── Data/
        │   └── AppDbContext.cs
        ├── Dtos/
        ├── Migrations/
        ├── Models/
        ├── Services/
        ├── Program.cs
        └── HelloBinX-CRUD_Operations.csproj
```

Postman requests and the API documentation are maintained with the Day 1
project.

------------------------------------------------------------------------

## 13. Key Concepts Learned

### Authentication

Authentication answers:

> Who is the user?

Identity provides the user-management foundation needed for
authentication.

### UserManager

`UserManager<IdentityUser>` provides operations for managing users.

### IdentityResult

`CreateAsync` returns an `IdentityResult`.

``` csharp
result.Succeeded
```

indicates whether the operation succeeded.

``` csharp
result.Errors
```

contains the validation or creation errors when it fails.

### Password Handling

Passwords are not stored as plain text. Identity handles password
hashing internally. fileciteturn3file5

------------------------------------------------------------------------

## 14. Day 1 Completion Checklist

-   [x] Add ASP.NET Core Identity package.
-   [x] Change `AppDbContext` to inherit from
    `IdentityDbContext<IdentityUser>`.
-   [x] Configure Identity in `Program.cs`.
-   [x] Create `AddIdentity` migration.
-   [x] Apply the migration to the database.
-   [x] Generate Identity tables.
-   [x] Implement `POST /api/Auth/register`.
-   [x] Use `UserManager<IdentityUser>`.
-   [x] Use `CreateAsync(user, password)`.
-   [x] Handle `IdentityResult` errors.
-   [x] Test valid registration.
-   [x] Test weak password validation.
-   [x] Test duplicate user registration.
-   [x] Update API documentation.
-   [x] Update Postman collection.

------------------------------------------------------------------------

## 15. Next Step

**Week 4 --- Day 2** focuses on **JWT Authentication & Token Issuance**,
including:

-   JWT structure and claims.
-   Login endpoint.
-   Token generation.
-   JWT Bearer authentication middleware.
-   Token expiry and refresh tokens. fileciteturn3file0
