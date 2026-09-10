# Week 7 --- Day 5: Sprint 2 Close-Out

## Overview

Day 5 closes **Sprint 2** of the Cardiac Patient Monitoring System.

The focus of this day was to demonstrate the completed authentication
and authorization flow, verify the implemented RBAC and ownership rules
through Postman, document the Sprint 2 retrospective, and prepare the
sprint for final close-out.

------------------------------------------------------------------------

## Objectives

-   Demonstrate the complete authentication and authorization flow.
-   Verify successful and rejected authorization scenarios.
-   Confirm the implemented RBAC and ownership behavior.
-   Complete the Sprint 2 retrospective.
-   Capture Postman evidence for the final Sprint 2 review.

------------------------------------------------------------------------

## Tasks Completed

### 1. Authentication & Authorization Demo

The complete authentication flow was demonstrated using Postman:

1.  Register a Patient.
2.  Login as Patient and obtain a JWT token.
3.  Access the Patient's own record.
4.  Attempt to access another Patient's record.
5.  Attempt a protected delete operation using a Patient token.
6.  Login as Admin.
7.  Perform the protected delete operation using the Admin token.

------------------------------------------------------------------------

## 2. Postman Demo Results

  ------------------------------------------------------------------------
  \#                Request           Result             Purpose
  ----------------- ----------------- ------------------ -----------------
  1                 Register Patient  `200 OK`           Verify patient
                                                         registration

  2                 Patient Login     `200 OK`           Verify JWT
                                                         authentication

  3                 Get Own Patient   `200 OK`           Verify
                                                         authenticated
                                                         access

  4                 Get Another       `403 Forbidden`    Verify ownership
                    Patient                              authorization

  5                 Delete Patient as `403 Forbidden`    Verify role-based
                    Patient                              authorization

  6                 Admin Login       `200 OK`           Verify Admin
                                                         authentication

  7                 Delete Patient as `204 No Content`   Verify protected
                    Admin                                Admin operation
  ------------------------------------------------------------------------

### Authorization Rejection Cases

Two deliberate rejection cases were demonstrated:

-   A Patient attempted to access another Patient's record →
    `403 Forbidden`.
-   A Patient attempted to delete a Patient record → `403 Forbidden`.

These cases confirm that authorization is not only allowing valid
requests but also correctly rejecting unauthorized operations.

------------------------------------------------------------------------

## 3. Sprint 2 Retrospective

### What Went Well

-   JWT authentication was successfully demonstrated for Patient and
    Admin users.
-   RBAC behavior was verified through real API requests.
-   Patient ownership restrictions were successfully tested.
-   Protected Admin functionality was successfully demonstrated.
-   Postman evidence was captured for the Sprint 2 close-out.

### What Could Be Improved

The Postman collection and supporting documentation should be kept
synchronized with implementation changes throughout the sprint so that
final verification is faster and easier.

### Action for Sprint 3

Continue applying a verification-first approach by keeping API testing
and evidence aligned with each important feature implemented in the next
sprint.

------------------------------------------------------------------------

## 4. Deliverables

-   [x] Sprint 2 authentication and authorization demo
-   [x] Patient JWT authentication
-   [x] Admin JWT authentication
-   [x] Ownership rejection test
-   [x] RBAC rejection test
-   [x] Admin protected-operation test
-   [x] Postman evidence
-   [x] Sprint 2 retrospective
-   [x] Day 5 documentation

> **Note:** The Notion deliverable was not included because it was
> cancelled for this internship workflow.

------------------------------------------------------------------------

## Status

**Week 7 --- Day 5: Completed**

**Sprint 2: Closed**
