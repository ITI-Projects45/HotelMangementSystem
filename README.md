#Hotels Management System
Our focus was mainly on the backend architecture, ensuring the system is scalable, well-structured, and efficient. Here's a detailed breakdown of what we built:

✅ Project Scope & User Roles:
We implemented full Role-Based Access Control using ASP.NET Identity, with three distinct user types:

Top-Level Admin
▪ Full control over the platform (users, hotels, rooms)

Hotel Owner
▪ Can add hotels, manage rooms, and handle bookings

Regular User
▪ Can browse hotels, filter by location or rating, book rooms, leave reviews , and book rooms

✅ Core Features:

Hotel Filtering
▪ By location (city, area, etc.)
▪ By rating, allowing users to see the most recommended hotels first

Room Booking System
▪ Users can check room availability and make real bookings

Reviews & Comments
▪ Integrated SignalR for real-time review updates
▪ Users can leave comments and ratings, instantly visible to others

Hotel Management Dashboard
▪ A complete panel for hotel owners to manage their properties and rooms with ease

✅ Technologies & Architecture:

ASP.NET MVC – Used for building the structure of the web application

ASP.NET Identity – For secure and flexible user management and authentication

LINQ – For querying and manipulating data efficiently

SignalR – For real-time communication and instant updates on reviews/comments

Repository Pattern – Ensured clean code structure, testability, and better separation of concerns

✅ Learning Outcome:
This project helped us gain real-world experience in:

Building role-based platforms

Handling real-time data updates

Designing clean backend architecture using design patterns

Working in a team environment with clear responsibilities

We focused more on the backend logic, as it was our goal to build a solid, scalable foundation before polishing the UI.
