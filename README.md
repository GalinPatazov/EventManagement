# EventManagement

## Project Overview
This is a personal/project internship project – an **Event Management System** built with **C# and Entity Framework**. The system allows users to browse, register for, and manage events, while administrators can manage events, users, and speakers. The project follows a **clean architecture** with DTOs, AutoMapper, and layered structure for maintainability and scalability.

---

## Database Structure

### **Event**
Represents events that users can attend.  
**Columns:**  
- `EventID` (Primary Key)  
- `Title`  
- `Description`  
- `Date`  
- `Location`  
- `SpeakerID` (Foreign Key to Speaker)

### **User**
Represents users who register for events.  
**Columns:**  
- `UserID` (Primary Key)  
- `FirstName`  
- `LastName`  
- `Email`  
- `DateOfBirth`

### **Registration**
Represents the registration of users for events.  
**Columns:**  
- `RegistrationID` (Primary Key)  
- `UserID` (Foreign Key to User)  
- `EventID` (Foreign Key to Event)  
- `RegistrationDate`

### **Speaker**
Represents speakers at events.  
**Columns:**  
- `SpeakerID` (Primary Key)  
- `Name`  
- `Biography`  
- `Email`

---

## Features / Tasks

### 1. CRUD Operations
- Create, read, update, and delete events, users, speakers, and registrations.  
- Implemented using **Entity Framework** endpoints.

### 2. Complex Queries
- Retrieve all events a specific user has registered for (with option for upcoming or past events).  
- Retrieve all users registered for a particular event.  
- Fetch upcoming events along with their speaker details.

### 3. Validation
- Validate that event dates are not in the past.  
- Ensure that user and speaker emails are in a valid format.  

---

## Technologies Used
- **C#**  
- **.NET / Entity Framework**  
- **DTOs & AutoMapper**  
- **Clean Architecture Principles**

---

## Project Structure
- **Entities:** Database models  
- **DTOs:** Data Transfer Objects for cleaner API responses  
- **Repositories / Services:** Handles business logic and data access  
- **Controllers / Endpoints:** RESTful API endpoints  

---

## Goals
- Build a scalable and maintainable event management system.  
- Practice clean architecture and layered design.  
- Implement CRUD operations, complex queries, and validation.  
