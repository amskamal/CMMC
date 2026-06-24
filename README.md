# 🔧 CMMS — Computerized Maintenance Management System

A web-based **Hospital Instrument Management System (CMMS)** built with **ASP.NET Core MVC**, **C#**, **SQL Server**, and **Neo4j**. This system is designed to track, manage, and maintain medical instruments and equipment within a hospital environment.

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core MVC (.NET 5) |
| Language | C# |
| Relational Database | SQL Server (SSMS) + Entity Framework Core |
| Graph Database | Neo4j (via Neo4j.Driver 4.4.0) |
| Frontend | HTML, CSS, JavaScript |
| ORM | Entity Framework Core (Code-First with Migrations) |
| Scaffolding | Microsoft.VisualStudio.Web.CodeGeneration.Design |

---

## 📁 Project Structure

```
CMMC/
├── Controllers/        # MVC Controllers (request handling & routing)
├── Data/               # DbContext and database configuration
├── Migrations/         # Entity Framework migration files
├── Models/             # Data models / entities (instruments, maintenance, etc.)
├── Views/              # Razor views (UI templates)
├── wwwroot/            # Static files (CSS, JS, images)
├── Program.cs          # Application entry point
├── Startup.cs          # Service configuration and middleware pipeline
├── appsettings.json    # App configuration (connection strings, etc.)
└── CMMS.csproj         # Project file
```

---

## ⚙️ Prerequisites

Before running this project, make sure you have the following installed:

- [.NET 5 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [Neo4j Desktop](https://neo4j.com/download/) or [Neo4j Community Edition](https://neo4j.com/download-center/#community)
- [Visual Studio 2019/2022](https://visualstudio.microsoft.com/) (recommended) or VS Code

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/amskamal/CMMC.git
cd CMMC
```

### 2. Configure the Database Connections

Open `appsettings.json` and update the connection strings for both SQL Server and Neo4j:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=CMMSDB;Trusted_Connection=True;MultipleActiveResultSets=true"
},
"Neo4j": {
  "Uri": "bolt://localhost:7687",
  "Username": "neo4j",
  "Password": "YOUR_NEO4J_PASSWORD"
}
```

### 3. Apply Database Migrations (SQL Server)

Run the following command to create and seed the relational database:

```bash
dotnet ef database update
```

### 4. Start Neo4j

Make sure your Neo4j database instance is running before launching the application. You can start it via Neo4j Desktop or by running the Neo4j service.

### 5. Run the Application

```bash
dotnet run
```

Or open `CMMS.sln` in Visual Studio and press **F5** to run.

The app will be available at `https://localhost:5001` or `http://localhost:5000`.

---

## 🌐 Features

- Track and manage hospital medical instruments and equipment
- Dual-database architecture: SQL Server for relational data + Neo4j for graph-based relationships
- MVC architecture with clean separation of concerns
- Entity Framework Code-First database migrations
- Responsive web UI with static assets (CSS/JS)
- Standard ASP.NET Core routing and authorization middleware

---

## 📦 Key NuGet Packages

| Package | Version | Purpose |
|---|---|---|
| `Microsoft.EntityFrameworkCore.SqlServer` | 5.0.12 | SQL Server integration |
| `Microsoft.EntityFrameworkCore.Tools` | 5.0.12 | EF Core CLI tools & migrations |
| `Microsoft.VisualStudio.Web.CodeGeneration.Design` | 5.0.2 | Scaffolding controllers & views |
| `Neo4j.Driver` | 4.4.0 | Neo4j graph database driver |

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m "Add your feature"`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Open a Pull Request

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

---

## 👤 Author

**amskamal** — [GitHub Profile](https://github.com/amskamal)
