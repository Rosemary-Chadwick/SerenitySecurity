# Serenity Security
//# <img src="./serenity_security/client/src/assets/logo.svg" alt="Serenity Security Logo" width="50" height="50"/> Serenity Security

## Introduction

Serenity Security is a comprehensive vulnerability management platform designed to help users identify, track, and remediate security vulnerabilities across their digital assets. The application enables users to register and monitor multiple system assets, scan for vulnerabilities using real-time CVE data from the National Vulnerability Database (NVD) Api, generate detailed security reports, and generate and track remediation progress.

## Demo

[Watch Demo Video](https://youtu.be/placeholder) - *Coming Soon*

## Screenshots

*Screenshots coming soon*

## Tech Stack

### Frontend
- **Framework**: [React](https://react.dev/)
- **Routing**: [React Router DOM](https://reactrouter.com/)
- **UI Components**: [Bootstrap](https://getbootstrap.com/) & [Reactstrap](https://reactstrap.github.io/)
- **Data Visualization**: [Recharts](https://recharts.org/)
- **Styling**: Custom CSS with theme support

### Backend
- **Language**: [C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- **Framework**: [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- **Authentication**: [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)
- **API Documentation**: [Swagger](https://swagger.io/)

### Database
- **DBMS**: [PostgreSQL](https://www.postgresql.org/)
- **ORM**: [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

## Features

### Asset Management

- Register and manage various system types (databases, network firewalls, etc.)
- Track system details including IP address, OS version, and system type
- Manage asset inventory with user-friendly interfaces

### Vulnerability Scanning & Reporting

- integration with the National Vulnerability Database (NVD) API
- Comprehensive vulnerability data including CVE IDs, detailed description, date published, CVSS scores, and severity levels
- Real-time reports with detailed vulnerability information and actionable results

### Custom Remediation Checklists

- Automatically generated remediation steps for each vulnerability
- Custom algorithm analyzes multiple data points and keywords to provide tailored remediation guidance
- Track remediation progress with interactive verification steps and completion status

### Data Visualization with Recharts

- Donut charts showing vulnerability severity distribution
- Color-coded visualizations using a custom security-focused palette:
  - High severity: #a83246 (muted red)
  - Medium severity: #d9b55a (muted yellow)
  - Low severity: #5a9178 (muted green)
- Responsive design that scales across different device sizes

### Matrix-Inspired Animated Theme System

- Dynamic light and dark theme options with animated matrix background
  - Light mode: Subtle cream background with navy matrix animation
  - Dark mode: Deep navy background with animated cream matrix effect
- Theme toggle persists across sessions for a consistent user experience
- Matrix animation dynamically adjusts to viewport size and theme changes

## Installation

### Prerequisites
- [Node.js](https://nodejs.org/) and npm
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- EF Core tools: `dotnet tool install --global dotnet-ef --framework net8.0`

### Setup Steps

1. **Clone the repository**
   ```bash
   git clone [your-repository-url]
   cd serenity-security
   ```

2. **Install dependencies**

   Backend:
   ```bash
   dotnet restore
   ```

   Frontend:
   ```bash
   cd client
   npm install
   ```

3. **Configure user secrets**
   ```bash
   dotnet user-secrets set "ConnectionStrings:SerenitySecurityDbConnectionString" "Host=localhost;Port=5432;Username=<your_postgres_username>;Password=<your_postgresql_password>;Database=serenity_security"
   dotnet user-secrets set "NvdApiKey" "<your-nvd-api-key>"
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**

   Backend:
   ```bash
   dotnet run
   ```

   Frontend:
   ```bash
   cd client
   npm run dev
   ```
## Architecture

Serenity Security follows a clean multi-tier architecture:

- **Presentation Layer**: React components organized by feature
- **Client Services Layer**: JavaScript managers for API communication
- **API Layer**: ASP.NET Core controllers handling business logic
- **Data Access Layer**: Entity Framework Core with PostgreSQL
- **External Service Integration**: NVD API for vulnerability data


## License
© [Rosemary Chadwick] 2025. All Rights Reserved.
Proprietary License
This software and associated documentation files (the "Software") are the proprietary property of the copyright holder. The Software is made available for private use only, subject to the following conditions:

You may use the Software for personal or internal business purposes only.
You may not distribute, sublicense, modify, or create derivative works based on the Software without explicit permission from the copyright holder.
The name "Serenity Security", associated logos, visual design elements, and the remediation generation algorithms are protected intellectual property and may not be used separately from this Software.
All rights not expressly granted herein are reserved by the copyright holder.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
