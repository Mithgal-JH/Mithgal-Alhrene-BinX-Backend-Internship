# Week 4 --- Day 4: Input Validation with FluentValidation

## Overview

Today focused on adding **server-side input validation** to the existing
Book CRUD API using **FluentValidation** and integrating it with the
ASP.NET Core request pipeline.

The goal was to keep validation rules organized, reusable, and separate
from controller business logic.

## What We Learned

-   Creating validators with `AbstractValidator<T>`.
-   Defining validation rules using `RuleFor`.
-   Using built-in validators such as:
    -   `NotEmpty()`
    -   `Length()`
    -   `GreaterThan()`
    -   `GreaterThanOrEqualTo()`
    -   `InclusiveBetween()`
-   Using `Must()` for custom validation logic.
-   Creating separate validators for `CreateBookDto` and
    `UpdateBookDto`.
-   Registering FluentValidation in ASP.NET Core.
-   Running validation automatically before the controller action
    continues.
-   Returning structured `400 Bad Request` validation responses.
-   Testing validation rules individually with Postman.

## Validation Rules Implemented

  Field             Validation
  ----------------- -------------------------------------------
  `BookName`        Required, letters only, 2--150 characters
  `AuthorId`        Must be greater than 0
  `Price`           Must be greater than or equal to 0
  `PublishedYear`   Must be between 1000 and the current year

The same validation approach was implemented for both **Create** and
**Update** book requests.

## Postman Testing

Each validation rule was tested individually for both Create and Update
endpoints.

Invalid requests returned:

``` text
400 Bad Request
```

with structured field-specific validation errors.

Valid requests were also tested:

-   Create → `201 Created`
-   Update → `200 OK`

A separate Postman documentation PDF contains the screenshots and
detailed results for all Create and Update validation cases.

## Key Takeaway

FluentValidation keeps input-validation rules inside dedicated validator
classes instead of placing repetitive checks throughout controllers.
This improves separation of concerns, readability, and maintainability.

## References

1.  **BinX Tech Backend .NET Internship --- Week 4, Day 4**\
    Training material: *Input Validation with FluentValidation*.

2.  **FluentValidation Documentation --- ASP.NET Core**\
    https://docs.fluentvalidation.net/en/latest/aspnet.html

3.  **YouTube Reference --- FluentValidation / ASP.NET Core**\
    https://www.youtube.com/watch?v=vioEObtWwjw

## Tools Used

-   ASP.NET Core Web API
-   C#
-   FluentValidation
-   Postman
-   Git & GitHub
