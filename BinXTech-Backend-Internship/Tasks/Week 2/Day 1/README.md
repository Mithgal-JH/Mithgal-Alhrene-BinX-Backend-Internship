# Week 2 - Day 1 | Generics & Advanced Collections

## Overview

On the first day of Week 2, I learned the fundamentals of **Generics** in C# and why they are important for writing reusable, type-safe code. I also implemented a **Generic Repository** that can work with different object types while avoiding duplicated code.

---

## Topics Covered

- Why Generics exist
- Generic Classes
- Generic Methods
- Generic Constraints (`where T : class`)
- Collection Interfaces
  - `IEnumerable<T>`
  - `IReadOnlyList<T>`
  - `IList<T>`
- Repository Pattern (Introduction)

---

## What I Implemented

### Generic Repository

Created a reusable generic repository:

```csharp
Repository<T> where T : class
```

Implemented the following methods:

- `Add(T item)`
- `GetAll()`
- `Find(Predicate<T> predicate)`

---

### Generic Constraint

Applied the following constraint:

```csharp
where T : class
```

This ensures the repository only works with reference types (classes), such as `Person` and `Customer`, instead of value types like `int` or `double`.

---

### IReadOnlyList

Changed the return type of `GetAll()` from:

```csharp
List<T>
```

to:

```csharp
IReadOnlyList<T>
```

This prevents callers from modifying the returned collection directly while still allowing them to read its contents.

---

### Reusability

Tested the repository with multiple domain models from previous tasks:

- `Person`
- `Customer`

This demonstrates that the same repository implementation can be reused with different classes without rewriting the code.

---

## What I Learned

- The purpose of Generics and how they improve code reusability.
- How generic type parameters work.
- How generic constraints restrict acceptable types.
- Why `IReadOnlyList<T>` is preferred over exposing `List<T>` in public APIs.
- How the Repository Pattern reduces code duplication.

---

## Resources

- Microsoft Learn Documentation
  - https://learn.microsoft.com/dotnet/csharp/programming-guide/generics/

- YouTube
  - https://www.youtube.com/watch?v=l6s7AvZx5j8

- ChatGPT
  - Used for explanations, code review, understanding generic constraints, repository implementation, and best practices.

---

## Result

✅ Successfully implemented a reusable Generic Repository.

✅ Applied generic constraints.

✅ Used `IReadOnlyList<T>` to protect collection data.

✅ Tested the repository with multiple object types.

✅ Completed all required Day 1 tasks.