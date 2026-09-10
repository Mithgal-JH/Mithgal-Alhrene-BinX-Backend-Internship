# 🚀 BinX Backend Development Internship

# Week 1 Summary

> **Intern:** Mithgal Alhrene  
> **Track:** Backend Development (.NET)  
> **Company:** BinX Tech  
> **Week:** 1 – C# Fundamentals & Git  
> **Duration:** 5 Days

---

# 📖 Overview

The first week of the BinX Backend Development Internship was dedicated to building a strong foundation in C# and the .NET ecosystem before moving into ASP.NET Core and backend development.

Throughout the week, I learned the fundamentals of the C# programming language, practiced Object-Oriented Programming (OOP), worked with collections and LINQ, explored asynchronous programming, and followed professional Git and GitHub workflows. Every concept was reinforced through hands-on console applications and daily coding exercises.

This repository contains all tasks completed during Week 1 and documents the progress made each day.

---

# 📅 Week Timeline

| Day | Topic | Status |
|------|-------------------------------|--------|
| Day 1 | Environment Setup | ✅ |
| Day 2 | C# Fundamentals | ✅ |
| Day 3 | Object-Oriented Programming | ✅ |
| Day 4 | Collections, LINQ & Async | ✅ |
| Day 5 | Git & GitHub Workflow | ✅ |

---

# 📆 Day 1 – Environment Setup

## 🎯 Objective

The first day focused on preparing a complete professional development environment for the internship. Before writing any backend code, it was essential to install the required tools and verify that the .NET ecosystem was working correctly.

---

## 📚 Topics Covered

- Introduction to the internship program
- Installing the .NET SDK
- Understanding the .NET CLI
- Installing Visual Studio Code
- Installing the C# Dev Kit Extension
- Running the first Console Application
- Git installation
- GitHub repository setup

---

## 💻 Practical Work

During this task I successfully:

- Installed the latest .NET SDK.
- Verified the installation using:

```bash
dotnet --version
```

- Installed Visual Studio Code.
- Configured the required C# extensions.
- Learned the basic .NET CLI commands:

```bash
dotnet new console
dotnet run
dotnet build
```

- Created my first Console Application.
- Successfully compiled and executed the application.
- Created the GitHub repository that will contain all internship work.

---

## 🎓 Learning Outcome

By the end of Day 1, I had a fully configured development environment capable of creating, building, and running .NET applications. I also became familiar with the basic workflow that will be used throughout the internship.

---

# 📆 Day 2 – C# Fundamentals

## 🔗 Repository

https://github.com/Mithgal-JH/Mithgal-Alhrene-BinX-Backend-Internship/tree/main/HelloBinX/Tasks/WEEK%201/Day%202

---

## 🎯 Objective

The goal of Day 2 was to understand the core building blocks of the C# language. These concepts are essential for writing reliable backend applications and serve as the foundation for all future ASP.NET Core projects.

---

## 📚 Concepts Practiced

### Variables

- Declaring variables
- Initialization
- Naming conventions

### Data Types

Value Types

- int
- double
- bool
- char

Reference Types

- string
- arrays
- classes

### Nullable Reference Types

- string?
- Null safety
- Compiler warnings

### Control Flow

- if / else
- switch
- for loop
- while loop
- foreach loop

### Methods

- Creating reusable methods
- Passing parameters
- Returning values

### User Input

- Console.ReadLine()
- Input validation

---

## 💻 Practical Work

During this task I implemented several console applications demonstrating different C# language features.

The exercises included:

- Working with primitive data types.
- Understanding the difference between Value Types and Reference Types.
- Creating nullable reference variables.
- Reading user input safely.
- Making decisions using if and switch statements.
- Repeating operations using loops.
- Writing reusable methods to organize program logic.

These exercises helped reinforce the syntax and behavior of the C# language while improving problem-solving skills.

---

## 🎓 Learning Outcome

After completing Day 2, I became comfortable with writing basic C# programs, controlling application flow, working with different data types, and safely handling nullable references. These concepts provide the programming foundation required for Object-Oriented Programming and backend development.

---


# 📆 Day 3 – Object-Oriented Programming (OOP)


## 🔗 Repository

https://github.com/Mithgal-JH/Mithgal-Alhrene-BinX-Backend-Internship/tree/main/HelloBinX/Tasks/WEEK%201/day%203

---

## 🎯 Objective

The third day focused on Object-Oriented Programming (OOP), one of the most important concepts in modern software development. The goal was to understand how real-world entities can be represented as objects and how to organize code into reusable, maintainable components.

To apply these concepts, I developed a simple **Store Management System** using C#.

---

## 📚 Concepts Practiced

### Classes & Objects

Creating custom classes that represent real-world entities.

### Constructors

Initializing objects with valid data.

### Encapsulation

Protecting object data using properties and controlled access.

### Inheritance

Creating specialized classes from a base class.

### Records

Using immutable data models where appropriate.

### Interfaces

Defining shared behavior through contracts.

### Object Composition

Building larger objects by combining multiple related objects.

---

## 💻 Practical Work

The project was designed around a simple store system consisting of multiple interacting classes.

### Person

Represents a generic person containing:

- Name
- Email

### Customer

Inherits from the Person class and is responsible for:

- Creating orders
- Selecting products
- Purchasing items

### Product

Represents an item available in the store.

Properties include:

- Product Name
- Price

### Order

Represents a customer order containing:

- Customer information
- Selected products
- Total order price

### Store

The Store class manages:

- Available products
- Customer orders
- Displaying products
- Processing purchases

---

## ⚙️ Project Features

The application allows the user to:

- Display available products.
- Create customer objects.
- Select multiple products.
- Build customer orders.
- Calculate the total order cost.
- Display complete order information.

---

## 🎓 Learning Outcome

Through this project I learned how Object-Oriented Programming helps organize code into reusable and maintainable components. I also became comfortable working with inheritance, constructors, encapsulation, interfaces, and relationships between objects.

This project represents my first complete object-oriented application in C#.

---

# 📆 Day 4 – Collections, LINQ & Async Programming

## 🔗 Repository

https://github.com/Mithgal-JH/Mithgal-Alhrene-BinX-Backend-Internship/tree/main/HelloBinX/Tasks/WEEK%201/Day%204

---

## 🎯 Objective

The fourth day focused on working with collections, querying data using LINQ, understanding asynchronous programming, and handling runtime exceptions. Instead of creating everything from scratch, I reused the classes developed in Day 3 and expanded the project by implementing new functionality.

---

## 📚 Concepts Practiced

### Collections

- List<T>
- Working with collections of objects

### LINQ

- Where()
- Select()
- Count()
- Filtering
- Projection
- Aggregation

### Async Programming

- async
- await
- Task
- Task.Delay()

### Exception Handling

- try
- catch
- FormatException

### Project References

- Reusing classes from another project
- Avoiding duplicate code

---

## 💻 Practical Work

The application was redesigned into a menu-driven console application using a **switch statement**, allowing the user to execute different features independently.

A collection containing eight Person objects was created and used throughout the application.

The implemented features include:

### Display Persons

Displays all stored Person objects.

### LINQ Queries

Implemented several LINQ operations including:

- Filtering persons with salaries greater than a specific value.
- Displaying only email addresses.
- Counting the total number of persons.

### Async Demonstration

Created an asynchronous method that simulates an I/O operation using Task.Delay() and returns a result after awaiting completion.

### Exception Handling

Implemented input validation using try/catch while specifically catching FormatException to prevent application crashes caused by invalid user input.

---

## ⚙️ Project Features

The final application includes:

- Interactive console menu.
- Multiple LINQ queries.
- Asynchronous method execution.
- Runtime exception handling.
- Reusing Day 3 classes through Project References.
- Better project organization and cleaner code structure.

---

## 🎓 Learning Outcome

This task significantly improved my understanding of data manipulation in C#. I became comfortable using LINQ to query collections, writing asynchronous methods using async/await, handling exceptions correctly, and structuring larger applications by separating responsibilities into reusable projects.

---

# 📆 Day 5 – Git & GitHub Workflow

## 🎯 Objective

The final day focused on learning professional version control practices using Git and GitHub. These tools are essential for collaborative software development and will be used throughout the internship.

---

## 📚 Topics Covered

- Git Fundamentals
- Repository Management
- Feature Branch Workflow
- Commit Messages
- Git Push & Pull
- Pull Requests
- GitHub Collaboration

---

## 💻 Practical Work

Throughout the week, all completed tasks were managed using Git and uploaded to GitHub.

The repository was organized by week and by day, creating a clean project history with meaningful commit messages that document the learning journey.

---

# 🛠️ Technologies Used

- C#
- .NET 8
- .NET CLI
- Visual Studio Code
- Object-Oriented Programming
- LINQ
- Async / Await
- Task
- Exception Handling
- Git
- GitHub

---

# 🎯 Skills Acquired

By the end of Week 1, I gained practical experience with:

- C# Fundamentals
- Object-Oriented Programming (OOP)
- Classes & Objects
- Constructors
- Inheritance
- Encapsulation
- Interfaces
- Records
- Collections
- LINQ
- Lambda Expressions
- Async / Await
- Exception Handling
- Git Workflow
- GitHub Collaboration
- Console Application Development

---

# 📦 Week 1 Deliverables

- ✅ Configured a professional .NET development environment.
- ✅ Built multiple C# console applications.
- ✅ Implemented Object-Oriented Programming concepts.
- ✅ Created a Store Management System.
- ✅ Worked with Collections and LINQ.
- ✅ Developed asynchronous methods using async/await.
- ✅ Implemented exception handling.
- ✅ Managed projects using Git and GitHub.

---

# 📚 Resources

## Official BinX Resources

### Environment Setup

https://www.youtube.com/watch?v=ZVGutgqBMUM

### C# Fundamentals

https://www.youtube.com/watch?v=YrtFtdTTfv0

---

## Additional Learning Resources

### LINQ

https://www.youtube.com/watch?v=Kf9YiRkj-m4

### Async / Await

https://www.youtube.com/watch?v=4eYx0-Zk7gw

### Microsoft Documentation

https://learn.microsoft.com/dotnet/

### Learning Assistant

- ChatGPT (Used to understand concepts, debug code, review implementations, and improve programming practices.)

---

# 🏁 Conclusion

Week 1 established a strong foundation in C# and the .NET ecosystem. Through practical exercises and hands-on projects, I gained confidence in writing clean, structured, and maintainable code while following professional development practices.

The knowledge acquired during this week prepares me for the upcoming stages of the internship, where I will begin building REST APIs using ASP.NET Core, working with databases, implementing authentication, and developing production-ready backend applications.