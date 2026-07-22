# BinX Tech Backend Internship – Day 4

## Overview

In this task, I built a simple console application using the `Person` class created in Day 3. Instead of executing all tasks sequentially, I implemented a menu-driven application using a `switch` statement, allowing the user to choose which functionality to run.

---

## What I Learned

During this task, I practiced and improved my understanding of:

- Creating and working with `List<T>`.
- Writing LINQ queries:
  - **Where()** for filtering data.
  - **Select()** for projecting specific properties.
  - **Count()** for aggregation.
- Understanding the difference between filtering and projection.
- Creating asynchronous methods using `async` / `await`.
- Simulating I/O operations using `Task.Delay()`.
- Handling runtime errors using `try` / `catch`.
- Catching specific exceptions such as `FormatException`.
- Building a simple interactive console menu using `switch`.
- Reusing classes from another project through Project References instead of duplicating code.

---

## Project Features

The application contains a simple menu with the following options:

1. Display all persons.
2. Execute LINQ queries:
   - Filter persons whose salary is greater than 1500.
   - Display all email addresses.
   - Count the total number of persons.
3. Demonstrate asynchronous programming using `Task.Delay()`.
4. Demonstrate exception handling by validating user input.
5. Exit the application.

---

## Code Summary

The program starts by creating a list of eight `Person` objects with different data. A menu is displayed, allowing the user to select which task to execute. Depending on the selected option, the application either displays the stored data, performs LINQ queries, simulates an asynchronous operation, or demonstrates exception handling using `try/catch`. The project is designed to keep each concept separated and easy to test from a single console application.

---

## Resources

The following videos helped me understand LINQ and asynchronous programming:

- https://www.youtube.com/watch?v=Kf9YiRkj-m4
- https://www.youtube.com/watch?v=4eYx0-Zk7gw

I also used **ChatGPT** as a learning assistant to better understand concepts, clarify questions, and review my implementation while completing the task.

---

## Technologies Used

- C#
- .NET
- LINQ
- Async / Await
- Task
- Exception Handling
- Visual Studio Code
- Git & GitHub