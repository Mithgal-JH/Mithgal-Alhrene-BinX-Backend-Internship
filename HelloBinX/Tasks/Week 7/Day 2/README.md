# 🔐 Week 7 — Day 2: JWT Login & Registration

![.NET 10](https://img.shields.io/badge/.NET%2010-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Identity](https://img.shields.io/badge/ASP.NET%20Core%20Identity-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)
![Postman](https://img.shields.io/badge/Postman-FF6C37?style=flat-square&logo=postman&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat-square&logo=postgresql&logoColor=white)

## 📌 Overview

Day 2 of Week 7 focused on implementing the authentication flow for the **Cardiac Patient Monitoring System** using **ASP.NET Core Identity** and **JWT**.

The authentication layer was organized around an `AuthService`, with separate registration flows for **Patients** and **Doctors**, and a login endpoint that verifies credentials and issues a signed JWT.

The implemented roles are:

- `Admin`
- `Doctor`
- `Patient`

The complete flow was tested through **Postman**.

---

## 🎯 Objectives

- Implement patient registration through the authentication service.
- Implement doctor registration through the authentication service.
- Create Identity users and assign the appropriate roles.
- Link the Identity user to the corresponding domain entity using `UserId`.
- Implement login using ASP.NET Core Identity.
- Generate a signed JWT after successful authentication.
- Include user ID, email, and roles in the JWT claims.
- Configure JWT Bearer authentication.
- Protect registration endpoints using the `Admin` role.
- Test successful and unsuccessful authentication flows with Postman.

---

## 🛠️ Technologies Used

- ASP.NET Core 10
- ASP.NET Core Identity
- Entity Framework Core
- PostgreSQL
- JWT Bearer Authentication
- `System.IdentityModel.Tokens.Jwt`
- Postman
- FluentValidation
- ASP.NET Core Rate Limiting

---

# 🔐 Authentication Architecture

```text
Client
   │
   │ Email + Password / Registration Data
   ▼
AuthController
   │
   ▼
IAuthService
   │
   ▼
AuthService
   │
   ├── UserManager
   │     ├── Create IdentityUser
   │     ├── Verify Password
   │     └── Assign Role
   │
   ├── ApplicationDbContext
   │     ├── Create Patient
   │     └── Create Doctor
   │
   └── JWT Generation
          ├── User ID
          ├── Email
          └── Role
   │
   ▼
JWT Token
```

---

# 👥 Identity Roles

The system seeds three application roles:

| Role | Purpose |
|---|---|
| `Admin` | Administrative operations |
| `Doctor` | Doctor-specific operations |
| `Patient` | Patient-specific operations |

The roles are created by `IdentitySeeder`.

---

# 🧱 Domain Entities & Identity

The `Patient` and `Doctor` entities contain a `UserId` property:

```text
Patient
 └── UserId → IdentityUser.Id

Doctor
 └── UserId → IdentityUser.Id
```

This keeps authentication data managed by ASP.NET Core Identity while the medical/domain information remains inside the application's own entities.

### Patient

The Patient entity stores information such as:

- Medical Record Number
- Name
- Date of Birth
- Gender
- Phone
- Email
- Address
- Emergency Contact
- Medical Notes
- Vital Signs
- Appointments
- Medications

### Doctor

The Doctor entity stores:

- Name
- Email
- Phone
- Specialization
- License Number
- Appointments

---

# 📡 API Endpoints

## Register Patient

```http
POST /api/Auth/register/patient
```

This endpoint creates:

1. An `IdentityUser`
2. Assigns the `Patient` role
3. Creates the corresponding `Patient` domain record

The endpoint is protected with:

```csharp
[Authorize(Roles = "Admin")]
```

### Successful Response

```json
{
  "message": "Patient registered successfully."
}
```

---

## Register Doctor

```http
POST /api/Auth/register/doctor
```

This endpoint creates:

1. An `IdentityUser`
2. Assigns the `Doctor` role
3. Creates the corresponding `Doctor` domain record

The endpoint is also restricted to administrators:

```csharp
[Authorize(Roles = "Admin")]
```

### Successful Response

```json
{
  "message": "Doctor registered successfully."
}
```

---

## Login

```http
POST /api/Auth/login
```

The login process:

1. Finds the user by email.
2. Verifies the password using Identity.
3. Gets the user's assigned roles.
4. Creates JWT claims.
5. Signs the token using the configured JWT key.
6. Applies the configured expiration time.
7. Returns the token to the client.

### Successful Response

```json
{
  "token": "JWT_TOKEN"
}
```

---

# 🪪 JWT Claims

The generated JWT contains:

| Claim | Purpose |
|---|---|
| `sub` | Identity user ID |
| `email` | User email |
| `role` | Assigned application role |
| `iss` | Configured JWT issuer |
| `aud` | Configured JWT audience |
| `exp` | Token expiration time |

Roles are added using:

```csharp
new Claim(ClaimTypes.Role, role)
```

This allows ASP.NET Core authorization to recognize the user's role when the JWT is used on protected endpoints.

---

# 🔄 Registration Flow

```text
POST /api/Auth/register/patient
              │
              ▼
        AuthController
              │
              ▼
     RegisterPatientAsync()
              │
              ├── Create IdentityUser
              │
              ├── Assign "Patient" role
              │
              ├── Create Patient entity
              │
              └── Save changes
              │
              ▼
     Patient registered
```

The Doctor registration flow follows the same structure with the `Doctor` role and `Doctor` entity.

Both registration methods use a database transaction so that failure during the domain-record creation can roll back the operation.

---

# 🔑 Login Flow

```text
POST /api/Auth/login
        │
        ▼
   AuthController
        │
        ▼
   AuthService
        │
        ├── Find user
        │
        ├── Check password
        │
        ├── Get roles
        │
        ├── Build claims
        │
        ├── Create signing key
        │
        ├── Create JWT
        │
        └── Serialize token
        │
        ▼
     200 OK
     JWT Token
```

For invalid credentials, the service returns no token and the controller responds with:

```http
401 Unauthorized
```

---

# 🧪 Postman Testing

The authentication endpoints were tested using Postman.

## 🟢 1. Patient Registration

### Request

```http
POST /api/Auth/register/patient
```

### Result

```http
200 OK
```

Response:

```json
{
  "message": "Patient registered successfully."
}
```

**Status: ✅ Passed**

---

## 🟢 2. Doctor Registration

### Request

```http
POST /api/Auth/register/doctor
```

### Result

```http
200 OK
```

Response:

```json
{
  "message": "Doctor registered successfully."
}
```

**Status: ✅ Passed**

---

## 🔴 3. Login With Invalid Password

### Request

```http
POST /api/Auth/login
```

Example:

```json
{
  "email": "patient.test@example.com",
  "password": "WrongPassword123"
}
```

### Result

```http
401 Unauthorized
```

Response:

```json
{
  "message": "Invalid email or password."
}
```

**Status: ✅ Passed**

---

## 🟢 4. Doctor Login

### Request

```json
{
  "email": "doctor.test@example.com",
  "password": "Doctor@12345"
}
```

### Result

```http
200 OK
```

Response contains a JWT token.

**Status: ✅ Passed**

---

## 🟢 5. Patient Login

### Request

```json
{
  "email": "patient.test@example.com",
  "password": "Patient@12345"
}
```

### Result

```http
200 OK
```

Response contains a JWT token.

**Status: ✅ Passed**

---

## 🟢 6. Admin Login

### Request

```json
{
  "email": "admin@cardiac.com",
  "password": "Admin@12345"
}
```

### Result

```http
200 OK
```

Response contains a JWT token.

**Status: ✅ Passed**

---

# 🛡️ JWT Bearer Configuration

JWT authentication is configured in `Program.cs`.

The validation configuration includes:

```text
ValidateIssuer
ValidateAudience
ValidateLifetime
ValidateIssuerSigningKey
```

The API uses the configured:

- JWT Issuer
- JWT Audience
- JWT Signing Key
- Token Expiration

This allows ASP.NET Core to validate incoming Bearer tokens before allowing access to protected endpoints.

---

# 🚦 Login Rate Limiting

The login endpoint also uses the existing `login` rate-limiting policy:

```csharp
[EnableRateLimiting("login")]
```

The configured policy allows:

```text
5 requests / minute
```

with no request queue.

When the limit is exceeded, the API returns:

```http
429 Too Many Requests
```

with:

```json
{
  "message": "Too many requests. Please try again later."
}
```

---

# 📁 Main Project Structure

```text
CardiacPatientMonitoringSystem/
│
├── Controllers/
│   └── AuthController.cs
│
├── Data/
│   ├── ApplicationDbContext.cs
│   └── IdentitySeeder.cs
│
├── DTOs/
│   └── Auth/
│       ├── LoginDto.cs
│       ├── RegisterPatientDto.cs
│       └── RegisterDoctorDto.cs
│
├── Models/
│   ├── Patient.cs
│   └── Doctor.cs
│
├── Services/
│   ├── AuthService.cs
│   └── Interfaces/
│       └── IAuthService.cs
│
├── Validators/
│   └── Auth/
│
└── Program.cs
```

---

# 🧩 Key Components

### `IdentitySeeder`

Responsible for:

- Creating the `Admin` role.
- Creating the `Doctor` role.
- Creating the `Patient` role.
- Seeding the configured admin account.
- Ensuring the admin account has the `Admin` role.

### `AuthService`

Responsible for:

- Patient registration.
- Doctor registration.
- Identity user creation.
- Role assignment.
- Password verification.
- JWT generation.
- JWT claims creation.

### `AuthController`

Responsible for exposing the authentication endpoints:

```text
POST /api/Auth/register/patient
POST /api/Auth/register/doctor
POST /api/Auth/login
```

It also applies role-based authorization to the registration endpoints.

---

# 🔒 Security Notes

- Passwords are handled by ASP.NET Core Identity rather than stored directly.
- JWTs are signed using a secret signing key.
- JWT lifetime validation is enabled.
- Registration of Patients and Doctors is restricted to the `Admin` role.
- Login returns a generic `Invalid email or password` response for invalid credentials.
- JWT secrets are configuration values and should not be committed to source control.
- Login requests are protected by rate limiting.

---

# ✅ Day 2 Completion

| Task | Status |
|---|:---:|
| Patient registration | ✅ |
| Doctor registration | ✅ |
| Identity user creation | ✅ |
| Role assignment | ✅ |
| Patient/Doctor linked with `UserId` | ✅ |
| Login | ✅ |
| JWT generation | ✅ |
| JWT claims | ✅ |
| JWT Bearer validation | ✅ |
| Admin-protected registration | ✅ |
| Invalid login handling | ✅ |
| Postman testing | ✅ |
| Login rate limiting configuration | ✅ |

## 🎉 Week 7 — Day 2 Complete

The Cardiac Patient Monitoring System now has a working authentication flow based on **ASP.NET Core Identity + JWT**, with separate Patient/Doctor registration, role assignment, protected registration endpoints, and successful authentication testing through Postman.
