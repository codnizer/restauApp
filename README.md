# Restaurant Reservation System 🍽️

A modern restaurant management system built with .NET 8 and MySQL, featuring table reservations, staff management, and customer reviews.

![App Screenshot](/screenshots/dashboard.png) 

## Features ✨

- 🏆 Customer loyalty program 
- 📅 Table reservation management
- 👨‍🍳 Employee management
- ⭐ Review and rating system


## Technology Stack 🛠️

- **Backend**: .NET 8 MVC
- **Database**: MySQL 8.0
- **ORM**: Entity Framework Core
- **Frontend**: Razor Views


## Getting Started 🚀

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL 8.0+](https://dev.mysql.com/downloads/) or XAMPP
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

### Installation

1. Clone the repository:

        git clone https://github.com/yourusername/RestauApp.git
        cd RestauApp

2. Configure the database:
    Create a MySQL database named restauapp

    Update connection string in appsettings.json:
    
        "ConnectionStrings": {
        "DefaultConnection": "server=localhost;port=3306;database=restauapp;user=root;password=yourpassword;"
        }

3. Apply database migrations:

         dotnet ef database update

    📌 Notes
                    Make sure your .NET SDK version matches the project’s target framework.
                        If you face any EF migration issues, use:

                                    dotnet ef migrations add InitialCreate
                                    dotnet ef database update

4. Run the application:

        dotnet run

5. Access the app at: https://localhost:5001


### Development Workflow 🔄
1. Create a new branch:

           git checkout -b feature/your-feature

2. After making changes:
 
           dotnet build

3. Commit and push:
   
        git add .
        git commit -m "Add your message"
        git push origin feature/your-feature
        Create a Pull Request to main branch

 ## 📸 Application Screenshots

### Dashboard Overview
![Dashboard](screenshots/dashboard.png)
*Homepage*

---

### Restaurant Management
![Restaurants](screenshots/restau.png)
*View and manage restaurant details*

---

### Dining Rooms (Salles)
![Salles](screenshots/salles.png)
*Manage different dining areas and their capacities*

---

### Tables Management
![Tables](screenshots/tables.png)
*Configure and view table arrangements*

---

### Staff Management
![Employees](screenshots/employe.png)
*Add and manage restaurant staff members*

---

### Customer Accounts
![Users](screenshots/utilisateur.png)
*Customer profiles and loyalty programs*

---

### Reservation System
![Reservations](screenshots/resrvation.png)
*Booking interface*

---

### Reviews & Ratings
![Reviews](screenshots/avis.png)
*Customer feedback and rating management*
