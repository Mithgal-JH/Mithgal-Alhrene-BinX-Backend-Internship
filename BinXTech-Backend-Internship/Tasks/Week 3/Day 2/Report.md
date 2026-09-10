# Week 3 - Day 2
# Hands-On Lab: Database Design and Normalization

This document contains the implementation of the **Week 3 - Day 2** hands-on lab requirements.

---

# 1. List every entity your Day 1 API resources need, and their attributes.

## Domain

**Library Management System**

The database design is based on the REST API resources created in **Week 3 - Day 1**.

---

## Author

| Attribute | Description |
|-----------|-------------|
| AuthorId | Unique identifier for each author. |
| AuthorName | Stores the author's full name. |
| Phone | Stores the author's phone number (later normalized). |
| Bio | Stores a short biography of the author. |

---

## Book

| Attribute | Description |
|-----------|-------------|
| BookId | Unique identifier for each book. |
| BookName | Stores the name of the book. |
| AuthorId | References the author who wrote the book. |
| PublishedYear | Stores the publication year. |
| Price | Stores the price of the book. |

---

## Review

| Attribute | Description |
|-----------|-------------|
| ReviewId | Unique identifier for each review. |
| BookId | References the reviewed book. |
| Rating | Stores the review rating. |
| Comment | Stores the review text. |
| CreatedAt | Stores the review creation date and time. |

---

# 2. Apply 1NF, 2NF, and 3NF to the design, splitting out any column that depends on something other than the full primary key.

## First Normal Form (1NF)

The **Author** entity violates **1NF** because the **Phone** attribute can contain multiple values for a single author, making it a **multi-valued attribute**.

To satisfy **1NF**, the **Phone** attribute is removed from the **Author** table and stored in a separate table named **PhoneNumbers**.

### Updated Tables

#### Author

| Attribute |
|-----------|
| AuthorId (PK) |
| AuthorName |
| Bio |

#### PhoneNumbers

| Attribute |
|-----------|
| PhoneNumberId (PK) |
| AuthorId (FK) |
| PhoneNumber |

All remaining attributes contain atomic values, therefore the design satisfies **1NF**.

---

## Second Normal Form (2NF)

Every table in the database uses a **single-column primary key**.

Since there are **no composite primary keys**, there are **no partial dependencies**.

Therefore, the design satisfies **2NF**.

---

## Third Normal Form (3NF)

All non-key attributes depend only on their table's primary key.

There are **no transitive dependencies** between non-key attributes.

Therefore, the design satisfies **3NF**.

---

# 3. Define primary keys for every table and foreign keys for every relationship.

## Primary Keys

| Table | Primary Key |
|--------|-------------|
| Author | AuthorId |
| Book | BookId |
| Review | ReviewId |
| PhoneNumbers | PhoneNumberId |

---

## Foreign Keys

| Table | Foreign Key | References |
|--------|-------------|------------|
| Book | AuthorId | Author.AuthorId |
| Review | BookId | Book.BookId |
| PhoneNumbers | AuthorId | Author.AuthorId |

---

## Relationships

- One **Author** can write many **Books**.
- One **Book** can have many **Reviews**.
- One **Author** can have many **PhoneNumbers**.

---

# 4. Diagram the schema as an ERD using a diagramming tool or a database GUI client.

## ERD Diagram

The Entity Relationship Diagram (ERD) was created using **dbdiagram.io**.

![Library ERD](ERD.png)

---

### Design Board (Miro)

https://miro.com/welcomeonboard/THdybHRPMUZiTVRJeFhFQS9pL3U4THJkN21HOFZBaDkwRlVpTSt4aTArWm1ESU1aV1pBRDBoTGdZbm51UGFDY0pEY1JzdUpjekVpNlArMGFlZTNiWWFzMTFWZlh1VUl4SzV5SHEwdnA1bVExcnlxTUZVZXp3elNLL3d3N3lKVzF3VHhHVHd5UWtSM1BidUtUYmxycDRnPT0hdjE=?share_link_id=690515375790

The ERD represents the following relationships:

- Author (1) → (Many) Book
- Book (1) → (Many) Review
- Author (1) → (Many) PhoneNumbers

---

# 5. Choose an appropriate column type for every attribute, paying particular attention to any monetary values.

| Table | Attribute | Data Type |
|--------|-----------|-----------|
| Author | AuthorId | INT |
| Author | AuthorName | VARCHAR(100) |
| Author | Bio | TEXT |
| Book | BookId | INT |
| Book | BookName | VARCHAR(200) |
| Book | AuthorId | INT |
| Book | PublishedYear | INT |
| Book | Price | DECIMAL(10,2) |
| Review | ReviewId | INT |
| Review | BookId | INT |
| Review | Rating | TINYINT |
| Review | Comment | TEXT |
| Review | CreatedAt | DATETIME |
| PhoneNumbers | PhoneNumberId | INT |
| PhoneNumbers | AuthorId | INT |
| PhoneNumbers | PhoneNumber | VARCHAR(20) |

### Monetary Values

The **Price** attribute uses the **DECIMAL(10,2)** data type because it represents a monetary value and requires fixed precision to avoid rounding errors.

---

# Conclusion

This hands-on lab extends the **Library Management System** designed in **Week 3 - Day 1** by creating a normalized relational database schema.

The completed solution includes:

- Identification of all required entities and attributes.
- Application of **First Normal Form (1NF)**.
- Application of **Second Normal Form (2NF)**.
- Application of **Third Normal Form (3NF)**.
- Definition of all **Primary Keys** and **Foreign Keys**.
- Creation of a complete **Entity Relationship Diagram (ERD)**.
- Selection of appropriate SQL data types for every attribute, including **DECIMAL(10,2)** for monetary values.

The final database schema is normalized, consistent, and ready to be implemented in SQL Server and later integrated with **Entity Framework Core (EF Core)**.