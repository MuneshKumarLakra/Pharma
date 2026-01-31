# Pharma Management System

> A comprehensive pharmaceutical service management application built with ASP.NET Core and Vue.js

---

## 📘 Executive Summary

### Project Purpose and Goals
The Pharma Management System is a full-stack web application designed to streamline pharmaceutical service operations, including pharmacy details management, service booking, on-site appointments, and order entry processing. The system enables efficient tracking and management of pharmaceutical services from booking through completion.

### Business Value and Justification
- **Operational Efficiency**: Centralizes pharmaceutical service management in a unified platform
- **Service Quality**: Standardizes service workflows and ensures consistent delivery
- **Data Accessibility**: Provides real-time access to pharmacy information and service records
- **Compliance**: Maintains detailed audit trails and documentation for regulatory requirements
- **Cost Reduction**: Reduces manual processes and improves resource allocation

### Target Users
- **Pharmacy Administrators**: Manage pharmacy details and operations
- **Service Technicians**: Track on-site visits and service appointments
- **Operations Managers**: Monitor service delivery and performance metrics
- **Support Staff**: Handle customer inquiries and service bookings

### High-Level Problem Being Solved
Eliminates fragmented pharmaceutical service management by providing a centralized system for:
- Pharmacy information management
- Service appointment scheduling
- On-site service tracking
- Order entry and processing
- Hours of operation management
- Contact and communication tracking

### Key Outcomes and KPIs
- Service booking completion rate
- Average service response time
- Customer satisfaction scores
- Data accuracy and completeness
- System uptime and availability
- User adoption rate

---

## 🧭 Architecture Overview

### System Architecture
```
┌─────────────────────────────────────────────────────────────┐
│                     Client Browser                           │
└────────────────────┬────────────────────────────────────────┘
                     │ HTTPS
┌────────────────────▼────────────────────────────────────────┐
│              PharmaClient (ASP.NET Core 5.0)                 │
│              ┌──────────────────────────────┐                │
│              │   Vue.js SPA (Vuetify)       │                │
│              │   - Components               │                │
│              │   - Router                   │                │
│              │   - Vuex Store              │                │
│              └──────────────────────────────┘                │
└────────────────────┬────────────────────────────────────────┘
                     │ HTTP/HTTPS API Calls
┌────────────────────▼────────────────────────────────────────┐
│         PharmaCoreApi (ASP.NET Core 3.1)                     │
│         ┌──────────────────────────────────────┐             │
│         │  Controllers                         │             │
│         │  - PharmaController                  │             │
│         │  - FileController                    │             │
│         └────────────┬─────────────────────────┘             │
│                      │                                        │
│         ┌────────────▼─────────────────────────┐             │
│         │  CouchDB Repository Layer            │             │
│         │  - CRUD Operations                   │             │
│         │  - Query Handling                    │             │
│         └────────────┬─────────────────────────┘             │
└──────────────────────┼────────────────────────────────────────┘
                       │ HTTP API
┌──────────────────────▼────────────────────────────────────────┐
│                   Apache CouchDB                               │
│                   - Document Storage                           │
│                   - Views & Queries                           │
└────────────────────────────────────────────────────────────────┘
```

### Core Components and Responsibilities

#### **PharmaClient** (Frontend)
- **Vue.js SPA**: Single-page application providing user interface
- **Vuetify**: Material Design component framework
- **Vue Router**: Client-side routing and navigation
- **Vuex**: State management for application data
- **Components**:
  - Service booking and management
  - Pharmacy details display
  - Calendar/scheduling interface
  - On-site appointment tracking
  - Order entry lists
  - Product management

#### **PharmaCoreApi** (Backend)
- **PharmaController**: Handles pharmacy and service-related operations
- **FileController**: Manages file uploads and downloads
- **CouchRepository**: Data access layer for CouchDB operations
- **Helper Classes**: Encryption (AES), stream handling, extensions
- **NLog**: Structured logging and diagnostics
- **Application Insights**: Monitoring and telemetry

### Data Flow Overview
1. **User Interaction**: User interacts with Vue.js frontend
2. **State Management**: Vuex store manages application state
3. **API Requests**: Frontend makes HTTP calls to PharmaCoreApi
4. **Business Logic**: API controllers process requests
5. **Data Layer**: CouchRepository interacts with CouchDB
6. **Response**: Data flows back through layers to frontend
7. **UI Update**: Vue components reactively update display

### Integration Points with External Systems
- **Apache CouchDB**: NoSQL document database for data persistence
- **Azure Application Insights**: Application performance monitoring
- **File System**: Document and file storage
- **External HTTP Services**: Configured via HttpClient

### Technology Stack Summary

**Backend:**
- ASP.NET Core 3.1 / 5.0
- C# / .NET
- NLog for logging
- Microsoft.ApplicationInsights
- Newtonsoft.Json

**Frontend:**
- Vue.js 2.6
- Vuetify 2.5 (Material Design)
- Vue Router 3.5
- Vuex 3.6
- Moment.js for date handling


**Database:**
- Apache CouchDB (NoSQL document database)

---

## 📝 Logging Framework Details

The Pharma application uses **NLog** as its primary logging framework for structured logging and diagnostics in the backend (`PharmaCoreApi`).

- **File Logging**: All logs (including trace-level) are written to daily log files under the `Log` directory, with detailed timestamps, messages, and stack traces for exceptions.
- **Application Insights Integration**: NLog is extended with the `Microsoft.ApplicationInsights.NLogTarget` to send logs and telemetry data directly to Azure Application Insights for centralized monitoring, performance tracking, and diagnostics.
- **Exception Handling**: The backend uses a global exception handler (see `ExtensionHelper.ConfigureExceptionHandler`) to capture unhandled exceptions, log them using NLog, and return standardized error responses.
- **Configuration**: Logging rules and targets are defined in `nlog.config`, specifying log levels, output formats, and destinations (file and Application Insights).
- **Telemetry Context**: Additional context properties (such as thread ID) are attached to Application Insights logs for enhanced traceability.

**Key Files:**
- `PharmaCoreApi/nlog.config`: NLog configuration (targets, rules, Application Insights integration)
- `PharmaCoreApi/Startup.cs`: NLog initialization and logger injection
- `PharmaCoreApi/Helper/ExtensionHelper.cs`: Centralized exception logging

**Benefits:**
- Centralized and persistent log storage (file and cloud)
- Real-time monitoring and alerting via Azure Application Insights
- Detailed error tracking and diagnostics for faster troubleshooting
- Compliance with operational and audit requirements

For more details, see the `nlog.config` file and the `ConfigureExceptionHandler` method in the backend source code.

**Development Tools:**
- Vue CLI 4.5
- Babel
- ESLint
- Sass/SCSS

---

## 🧱 Repository Structure

```
Pharma/
├── Pharma.sln                          # Visual Studio solution file
│
├── PharmaClient/                       # Frontend ASP.NET Core + Vue.js application
│   ├── PharmaClient.csproj            # Client project file (.NET 5.0)
│   ├── Program.cs                      # Application entry point
│   ├── Startup.cs                      # ASP.NET Core configuration
│   ├── appsettings.json               # Application configuration
│   ├── appsettings.Development.json   # Development environment config
│   │
│   ├── clientapp/                      # Vue.js SPA
│   │   ├── package.json               # NPM dependencies
│   │   ├── vue.config.js              # Vue CLI configuration
│   │   ├── babel.config.js            # Babel transpiler config
│   │   ├── public/                    # Static assets
│   │   │   └── index.html             # HTML entry point
│   │   │
│   │   └── src/                       # Vue.js source code
│   │       ├── App.vue                # Root Vue component
│   │       ├── main.js                # Vue application entry
│   │       ├── components/            # Vue components
│   │       │   ├── calender.vue       # Calendar/scheduling
│   │       │   ├── Modal.vue          # Modal dialogs
│   │       │   ├── OnSiteApp.vue      # On-site appointments
│   │       │   ├── OrderEntryList.vue # Order management
│   │       │   ├── PharmaDetails.vue  # Pharmacy details display
│   │       │   ├── ProductList.vue    # Product catalog
│   │       │   ├── ServiceBook.vue    # Service booking
│   │       │   ├── StartService.vue   # Service initiation
│   │       │   ├── ViewOnSite.vue     # On-site view
│   │       │   ├── Exceptions/        # Error handling components
│   │       │   └── ServiceBookPanels/ # Service booking sub-components
│   │       ├── Model/                 # Data models
│   │       ├── plugins/               # Vue plugins (Vuetify)
│   │       ├── router/                # Vue Router configuration
│   │       └── store/                 # Vuex state management
│   │
│   └── Properties/
│       └── launchSettings.json        # Launch profiles
│
└── PharmaCoreApi/                     # Backend REST API
    ├── PharmaCoreApi.csproj          # API project file (.NET Core 3.1)
    ├── Program.cs                     # Application entry point
    ├── Startup.cs                     # API configuration and DI
    ├── appsettings.json              # API configuration
    ├── appsettings.Development.json  # Development config
    ├── nlog.config                    # NLog logging configuration
    │
    ├── Controllers/                   # API endpoints
    │   ├── PharmaController.cs       # Pharmacy operations
    │   └── FileController.cs         # File handling
    │
    ├── DBContext/                     # Data access layer
    │   ├── CouchRepository.cs        # CouchDB implementation
    │   └── ICouchRepository.cs       # Repository interface
    │
    ├── Helper/                        # Utility classes
    │   ├── Aes.cs                    # AES encryption
    │   ├── ExtensionHelper.cs        # Extension methods
    │   ├── StreamHelper.cs           # Stream utilities
    │   └── TrimmingConverter.cs      # JSON trimming
    │
    ├── Models/                        # Data transfer objects
    │   ├── PharmaDetails.cs          # Pharmacy entity
    │   ├── Documents.cs              # Document model
    │   ├── DrugDetails.cs            # Drug information
    │   ├── ErrorDetails.cs           # Error handling
    │   └── [Other models]            # Various DTOs
    │
    └── Properties/
        ├── launchSettings.json        # Launch profiles
        └── serviceDependencies*.json  # Azure dependencies
```

### Key Modules and Their Purpose

- **PharmaClient**: Frontend web application serving Vue.js SPA
- **PharmaCoreApi**: RESTful API backend for data operations
- **Controllers**: HTTP endpoint handlers
- **DBContext**: Database abstraction and CouchDB integration
- **Helper**: Utility functions for encryption, streams, extensions
- **Models**: Data structures and DTOs
- **Components**: Reusable Vue UI components
- **Store**: Centralized Vuex state management

### Important Configuration Files

- **appsettings.json**: Database connection strings, API URLs, secrets
- **nlog.config**: Logging configuration and targets
- **vue.config.js**: Vue CLI and webpack configuration
- **package.json**: NPM dependencies and scripts
- **launchSettings.json**: Development server profiles

---

## ⚙️ Prerequisites

### Required Runtimes and Versions

**Backend Requirements:**
- **.NET Core 3.1 SDK** (for PharmaCoreApi)
- **.NET 5.0 SDK** (for PharmaClient)
- Download from: https://dotnet.microsoft.com/download

**Frontend Requirements:**
- **Node.js**: v14.x or v16.x
- **NPM**: v6.x or higher (comes with Node.js)
- Download from: https://nodejs.org/

### Infrastructure Dependencies

**Database:**
- **Apache CouchDB**: v3.x or higher
  - Installation guide: https://docs.couchdb.org/en/stable/install/index.html
  - Default port: 5984
  - Required permissions: Database admin access

**Cloud Services (Optional):**
- **Azure Application Insights**: For monitoring and telemetry
  - Instrumentation key required in appsettings.json

### Required Credentials and Secrets

Configure the following in `PharmaCoreApi/appsettings.json`:

```json
{
  "CouchDB": {
    "URL": "http://localhost:5984",
    "User": "admin:password_in_base64",
    "Database": "pharma_db"
  },
  "ApplicationInsights": {
    "InstrumentationKey": "your-instrumentation-key"
  }
}
```

**🔐 Secure Handling:**
- Never commit credentials to version control
- Use **User Secrets** for local development:
  ```bash
  dotnet user-secrets set "CouchDB:User" "admin:yourpassword"
  ```
- Use **Azure Key Vault** or environment variables in production
- The project uses UserSecretsId: `bb2197da-9e03-4b44-9845-cda88f4848cb`

### Supported Operating Systems
- **Windows**: Windows 10/11, Windows Server 2016+
- **Linux**: Ubuntu 18.04+, Debian 10+, RHEL 7+
- **macOS**: macOS 10.15 (Catalina) or higher

---

## 🛠️ Setup & Installation

### 1. Clone the Repository
```bash
git clone <repository-url>
cd Pharma
```

### 2. Backend Setup (PharmaCoreApi)

#### Install Dependencies
```bash
cd PharmaCoreApi
dotnet restore
```

#### Configure CouchDB Connection
Edit `appsettings.Development.json` or use User Secrets:
```bash
dotnet user-secrets set "CouchDB:URL" "http://localhost:5984"
dotnet user-secrets set "CouchDB:User" "admin:yourpassword"
dotnet user-secrets set "CouchDB:Database" "pharma_db"
```

#### Build the API
```bash
dotnet build
```

### 3. Frontend Setup (PharmaClient)

#### Navigate to Client App
```bash
cd ../PharmaClient/clientapp
```

#### Install NPM Dependencies
```bash
npm install
```

#### Configure API Endpoint
Update API base URL in frontend configuration if needed (typically in environment files or Vuex store).

### 4. Database Setup

#### Create CouchDB Database
```bash
curl -X PUT http://admin:password@localhost:5984/pharma_db
```

Or use the CouchDB web interface (Fauxton):
- Navigate to `http://localhost:5984/_utils`
- Create a new database named `pharma_db`

### 5. Environment Variable Configuration

#### Sample .env Template (Frontend)
Create `.env.local` in `PharmaClient/clientapp/`:
```env
VUE_APP_API_BASE_URL=https://localhost:5001
VUE_APP_ENABLE_LOGGING=true
```

#### Sample appsettings Template (Backend)
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  },
  "AllowedHosts": "*",
  "CouchDB": {
    "URL": "http://localhost:5984",
    "User": "<username>:<password>",
    "Database": "pharma_db"
  },
  "ApplicationInsights": {
    "InstrumentationKey": "your-key-here"
  }
}
```

---

## 🚀 Running the Project

### Local Development

#### Option 1: Run Backend and Frontend Separately

**Terminal 1 - Backend API:**
```bash
cd PharmaCoreApi
dotnet run
```
API will be available at: `https://localhost:5001` and `http://localhost:5000`

**Terminal 2 - Frontend Client:**
```bash
cd PharmaClient
dotnet run
```
Client will be available at: `https://localhost:5003` (or configured port)

**Terminal 3 - Vue.js Dev Server (optional):**
```bash
cd PharmaClient/clientapp
npm run serve
```
Vue dev server: `http://localhost:8080`

#### Option 2: Run from Solution
```bash
# From repository root
dotnet run --project PharmaCoreApi/PharmaCoreApi.csproj
dotnet run --project PharmaClient/PharmaClient.csproj
```

### Docker Run Instructions

#### Build Docker Images
```bash
# Backend API
docker build -t pharma-api:latest -f PharmaCoreApi/Dockerfile .

# Frontend Client
docker build -t pharma-client:latest -f PharmaClient/Dockerfile .
```

#### Run with Docker Compose
Create `docker-compose.yml`:
```yaml
version: '3.8'
services:
  couchdb:
    image: couchdb:3
    ports:
      - "5984:5984"
    environment:
      COUCHDB_USER: admin
      COUCHDB_PASSWORD: password
    volumes:
      - couchdb-data:/opt/couchdb/data

  pharma-api:
    image: pharma-api:latest
    ports:
      - "5001:80"
    environment:
      CouchDB__URL: http://couchdb:5984
      CouchDB__User: admin:password
      CouchDB__Database: pharma_db
    depends_on:
      - couchdb

  pharma-client:
    image: pharma-client:latest
    ports:
      - "5003:80"
    depends_on:
      - pharma-api

volumes:
  couchdb-data:
```

Run containers:
```bash
docker-compose up -d
```

### Production Run Instructions

#### Build for Production
```bash
# Backend
cd PharmaCoreApi
dotnet publish -c Release -o ./publish

# Frontend
cd PharmaClient/clientapp
npm run build
```

#### Run Production Build
```bash
cd PharmaCoreApi/publish
dotnet PharmaCoreApi.dll
```

### Startup Verification Steps

1. **Check API Health:**
   ```bash
   curl https://localhost:5001/api/health
   ```

2. **Verify CouchDB Connection:**
   ```bash
   curl http://localhost:5984/_all_dbs
   ```

3. **Access Frontend:**
   - Navigate to `https://localhost:5003`
   - Verify login page loads
   - Check browser console for errors

4. **Review Logs:**
   - Check `PharmaCoreApi/Log/` directory for NLog outputs
   - Review Application Insights for telemetry (if configured)

---

## 🧪 Testing & Quality Gates

### Unit Tests
```bash
# Run all unit tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true /p:CoverageReportFormat=opencover
```

### Integration Test Setup
```bash
# Ensure test database is available
# Run integration tests
dotnet test --filter Category=Integration
```

### Frontend Tests
```bash
cd PharmaClient/clientapp

# Run Vue component tests
npm run test:unit

# Run end-to-end tests
npm run test:e2e
```

### Linting and Static Analysis

**Backend:**
```bash
# Code analysis (configure in .csproj)
dotnet build /p:RunAnalyzers=true
```

**Frontend:**
```bash
cd PharmaClient/clientapp

# ESLint
npm run lint

# Fix auto-fixable issues
npm run lint -- --fix
```

### Code Coverage Requirements
- **Minimum Coverage**: 70%
- **Critical Paths**: 90%
- **Controllers**: 80%
- **Utilities/Helpers**: 85%

### Quality Gate Thresholds
- **Build**: Must succeed without errors
- **Tests**: 100% pass rate
- **Code Coverage**: ≥ 70%
- **Security Vulnerabilities**: 0 critical or high
- **Code Smells**: < 5 major issues
- **Duplication**: < 3%

---

## 🔐 Security & Compliance

### Authentication and Authorization Model
- **User Authentication**: Configured in ASP.NET Core Identity (or custom implementation)
- **API Authorization**: Bearer token-based authentication
- **Role-Based Access Control**: Admin, ServiceTech, User roles
- **CORS Policy**: Configured in Startup.cs for allowed origins

### Secret Management Approach
- **Development**: .NET User Secrets (`dotnet user-secrets`)
- **Staging/Production**: Azure Key Vault or environment variables
- **Database Credentials**: Base64-encoded in appsettings, encrypted in transit
- **API Keys**: Never hardcode; use configuration providers
- **Encryption**: AES encryption for sensitive data (see `Helper/Aes.cs`)

### Data Classification and Handling Rules

| Classification | Examples | Handling Requirements |
|---|---|---|
| **Public** | Product catalog, hours | No special handling |
| **Internal** | Service logs, analytics | Restricted to authorized users |
| **Confidential** | Customer PII, pharmacy details | Encrypted at rest and in transit |
| **Restricted** | Authentication credentials | Encrypted, access-logged, vault-stored |

### Security Best Practices
- ✅ Always use HTTPS in production
- ✅ Enable HSTS (HTTP Strict Transport Security)
- ✅ Implement proper input validation and sanitization
- ✅ Use parameterized queries to prevent injection attacks
- ✅ Keep dependencies updated (use `dotnet outdated` and `npm audit`)
- ✅ Enable Content Security Policy (CSP) headers
- ✅ Implement rate limiting on API endpoints
- ✅ Log security events for audit trails
- ✅ Regular security scanning with tools like SonarQube

### Known Security Constraints
- CouchDB admin credentials in Base64 (consider OAuth integration)
- CORS policy must be tightly controlled for production
- Application Insights may log sensitive data; configure filtering
- File uploads require size limits and content-type validation

---

## 📦 Deployment Guide

### CI/CD Pipeline Overview

**Recommended Pipeline (Azure DevOps / GitHub Actions):**
1. **Source**: Trigger on push to main/develop branches
2. **Build**:
   - Restore NuGet packages
   - Build .NET projects
   - Run NPM install and build
3. **Test**:
   - Run unit tests
   - Run integration tests
   - Generate code coverage reports
4. **Quality Gates**:
   - SonarQube analysis
   - Security vulnerability scan
5. **Package**:
   - Create deployment artifacts
   - Build Docker images
6. **Deploy**:
   - Deploy to target environment
   - Run smoke tests
7. **Notify**: Send deployment status notifications

### Deployment Environments

#### Development
- **Purpose**: Active development and testing
- **URL**: `https://dev.pharma.local`
- **Database**: dev_pharma_db
- **Auto-deploy**: On commit to `develop` branch


#### QA
- **Purpose**: Quality assurance testing, pre-production validation
- **URL**: `https://qa.pharma.local`
- **Database**: qa_pharma_db
- **Deploy**: Manual approval from develop branch

#### UAT (User Acceptance Testing)
- **Purpose**: Final user acceptance testing before production
- **URL**: `https://uat.pharma.local`
- **Database**: uat_pharma_db
- **Deploy**: Manual approval from QA branch or after QA sign-off

#### Production
- **Purpose**: Live production environment
- **URL**: `https://pharma.yourcompany.com`
- **Database**: prod_pharma_db
- **Deploy**: Manual approval from `main` branch
- **Backup**: Automated daily backups

### Deployment Steps

#### Manual Deployment
```bash
# 1. Build artifacts
dotnet publish PharmaCoreApi/PharmaCoreApi.csproj -c Release -o ./artifacts/api
cd PharmaClient/clientapp && npm run build && cd ../..

# 2. Stop running services
systemctl stop pharma-api
systemctl stop pharma-client

# 3. Backup current version
cp -r /var/www/pharma-api /var/www/pharma-api.backup
cp -r /var/www/pharma-client /var/www/pharma-client.backup

# 4. Deploy new version
cp -r ./artifacts/api/* /var/www/pharma-api/
cp -r ./PharmaClient/clientapp/dist/* /var/www/pharma-client/

# 5. Update configuration
cp appsettings.Production.json /var/www/pharma-api/appsettings.json

# 6. Start services
systemctl start pharma-api
systemctl start pharma-client

# 7. Verify deployment
curl https://localhost:5001/api/health
```

#### Automated Deployment (Example GitHub Actions)
```yaml
name: Deploy to Production

on:
  push:
    branches: [main]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Setup .NET
        uses: actions/setup-dotnet@v1
        with:
          dotnet-version: '5.0.x'
      - name: Build and Publish
        run: |
          dotnet publish -c Release
      - name: Deploy to Server
        # Add deployment steps
```

### Rollback Procedures

#### Quick Rollback
```bash
# 1. Stop current services
systemctl stop pharma-api pharma-client

# 2. Restore from backup
rm -rf /var/www/pharma-api
rm -rf /var/www/pharma-client
mv /var/www/pharma-api.backup /var/www/pharma-api
mv /var/www/pharma-client.backup /var/www/pharma-client

# 3. Restart services
systemctl start pharma-api pharma-client

# 4. Verify rollback
curl https://localhost:5001/api/health
```

#### Database Rollback
```bash
# Restore from CouchDB backup
curl -X POST http://admin:password@localhost:5984/_replicate \
  -H "Content-Type: application/json" \
  -d '{
    "source": "pharma_db_backup_YYYYMMDD",
    "target": "pharma_db",
    "create_target": true
  }'
```

### Release Process
1. **Version Bump**: Update version in project files
2. **Changelog**: Document changes in CHANGELOG.md
3. **Tag Release**: Create Git tag (e.g., `v1.2.0`)
4. **Build Artifacts**: Generate release artifacts
5. **QA Approval**: Obtain sign-off from QA team
6. **Deploy to Production**: Execute deployment
7. **Smoke Tests**: Verify critical functionality
8. **Monitor**: Watch logs and metrics for issues
9. **Announce**: Notify stakeholders of release

---

## 📄 API Documentation

### Base URL
- **Development**: `https://localhost:5001`
- **Production**: `https://api.pharma.yourcompany.com`

### Endpoint List

#### Pharmacy Operations

**GET** `/api/Pharma/GetPharmaDetails/{id}`
- **Description**: Retrieve pharmacy details by ID
- **Auth**: Required (Bearer token)
- **Parameters**: `id` (string) - Pharmacy identifier
- **Response**: `200 OK` with PharmaDetails object

**POST** `/api/Pharma/SavePharmaDetails`
- **Description**: Create or update pharmacy details
- **Auth**: Required
- **Body**: PharmaDetails object (JSON)
- **Response**: `201 Created` with saved entity

**GET** `/api/Pharma/ListPharmas`
- **Description**: List all pharmacies
- **Auth**: Required
- **Query Params**: `page`, `limit`, `search`
- **Response**: `200 OK` with array of pharmacies

**PUT** `/api/Pharma/UpdatePharmaDetails/{id}`
- **Description**: Update existing pharmacy
- **Auth**: Required
- **Parameters**: `id` (string)
- **Body**: UpdatePharmaDetails object
- **Response**: `200 OK`

**DELETE** `/api/Pharma/DeletePharma/{id}`
- **Description**: Delete pharmacy record
- **Auth**: Required (Admin only)
- **Parameters**: `id` (string)
- **Response**: `204 No Content`

#### File Operations

**POST** `/api/File/Upload`
- **Description**: Upload file/document
- **Auth**: Required
- **Body**: Multipart form data
- **Response**: `200 OK` with file metadata

**GET** `/api/File/Download/{id}`
- **Description**: Download file by ID
- **Auth**: Required
- **Parameters**: `id` (string)
- **Response**: `200 OK` with file stream

### Request/Response Examples

#### Get Pharmacy Details
**Request:**
```http
GET /api/Pharma/GetPharmaDetails/123 HTTP/1.1
Host: localhost:5001
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response:**
```json
{
  "id": "123",
  "name": "Central Pharmacy",
  "address": "123 Main St, City, State 12345",
  "phone": "(555) 123-4567",
  "email": "contact@centralpharmacy.com",
  "hoursOfOperation": {
    "monday": "9:00 AM - 6:00 PM",
    "tuesday": "9:00 AM - 6:00 PM",
    "saturday": "10:00 AM - 4:00 PM"
  },
  "services": ["Prescription", "Consultation", "Delivery"]
}
```

#### Save Pharmacy Details
**Request:**
```http
POST /api/Pharma/SavePharmaDetails HTTP/1.1
Host: localhost:5001
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "name": "New Pharmacy",
  "address": "456 Oak Ave, Town, State 67890",
  "phone": "(555) 987-6543",
  "email": "info@newpharmacy.com"
}
```

**Response:**
```json
{
  "id": "124",
  "name": "New Pharmacy",
  "address": "456 Oak Ave, Town, State 67890",
  "phone": "(555) 987-6543",
  "email": "info@newpharmacy.com",
  "createdAt": "2026-01-07T10:30:00Z"
}
```

### Authentication Requirements
- **Type**: Bearer Token (JWT)
- **Header**: `Authorization: Bearer <token>`
- **Token Lifetime**: 60 minutes
- **Refresh**: Use refresh token endpoint

### Error Codes and Formats

| Status Code | Description | Example |
|---|---|---|
| 200 | Success | Request completed successfully |
| 201 | Created | Resource created successfully |
| 400 | Bad Request | Invalid input parameters |
| 401 | Unauthorized | Missing or invalid token |
| 403 | Forbidden | Insufficient permissions |
| 404 | Not Found | Resource not found |
| 500 | Internal Server Error | Server-side error |

**Error Response Format:**
```json
{
  "statusCode": 400,
  "message": "Invalid pharmacy ID format",
  "details": "The ID must be a valid GUID",
  "timestamp": "2026-01-07T10:30:00Z",
  "path": "/api/Pharma/GetPharmaDetails/invalid-id"
}
```

---

## 🧩 Usage Examples

### Sample Scripts

#### Creating a New Pharmacy Entry
```javascript
// Using Axios in Vue.js
import axios from 'axios';

async function createPharmacy() {
  try {
    const response = await axios.post('/api/Pharma/SavePharmaDetails', {
      name: 'West Side Pharmacy',
      address: '789 West Blvd, City, State 11111',
      phone: '(555) 222-3333',
      email: 'westside@pharmacy.com',
      hoursOfOperation: {
        monday: '8:00 AM - 8:00 PM',
        tuesday: '8:00 AM - 8:00 PM',
        wednesday: '8:00 AM - 8:00 PM',
        thursday: '8:00 AM - 8:00 PM',
        friday: '8:00 AM - 8:00 PM',
        saturday: '9:00 AM - 5:00 PM',
        sunday: 'Closed'
      }
    });
    
    console.log('Pharmacy created:', response.data);
    return response.data;
  } catch (error) {
    console.error('Error creating pharmacy:', error);
    throw error;
  }
}
```

#### Booking a Service Appointment
```javascript
// Vuex action for service booking
async bookService({ commit }, serviceData) {
  commit('SET_LOADING', true);
  
  try {
    const response = await this.$http.post('/api/Service/Book', {
      pharmacyId: serviceData.pharmacyId,
      serviceType: serviceData.serviceType,
      scheduledDate: serviceData.date,
      technicianId: serviceData.technicianId,
      notes: serviceData.notes
    });
    
    commit('ADD_BOOKING', response.data);
    commit('SET_LOADING', false);
    
    return response.data;
  } catch (error) {
    commit('SET_ERROR', error.message);
    commit('SET_LOADING', false);
    throw error;
  }
}
```

### Demo Workflows

#### 1. Pharmacy Registration Workflow
```
1. User navigates to "Add Pharmacy" page
2. Fills out pharmacy details form
3. Uploads pharmacy license documents
4. Submits form
5. System validates inputs
6. API saves data to CouchDB
7. Confirmation email sent
8. User redirected to pharmacy details view
```

#### 2. Service Booking Workflow
```
1. User searches for pharmacy
2. Views pharmacy details and available services
3. Selects service type (e.g., "Equipment Maintenance")
4. Chooses date/time from calendar
5. Assigns technician
6. Adds pre-service notes
7. Confirms booking
8. System creates service record
9. Technician receives notification
10. Booking appears in calendar view
```

### Real-World Usage Scenarios

#### Scenario 1: Monthly Equipment Maintenance
```
A pharmacy needs monthly equipment calibration:
- Pharmacy admin logs in
- Navigates to "Service Book" component
- Selects "Preventive Maintenance" service
- Schedules recurring monthly appointment
- System generates service orders
- Technicians receive automated notifications
- Service completion tracked in "Service" panel
- Post-service reports uploaded via FileController
```

#### Scenario 2: Emergency On-Site Visit
```
Equipment malfunction requiring immediate attention:
- Pharmacy calls support hotline
- Support staff creates "OnSiteApp" entry
- Priority marked as "Emergency"
- System notifies available technicians
- Technician accepts assignment
- Real-time status updates in "ViewOnSite"
- Issue documented in "Comments" panel
- Resolution recorded and signed off
```

---

## 🤝 Contribution Guidelines

### Branching Strategy

We follow **Git Flow** branching model:

```
main (production)
  ├── release/v1.x (release candidates)
  ├── develop (integration branch)
      ├── feature/pharmacy-search (new features)
      ├── feature/service-calendar
      ├── bugfix/order-entry-validation (bug fixes)
      └── hotfix/security-patch (critical fixes)
```

**Branch Naming Conventions:**
- `feature/<short-description>` - New features
- `bugfix/<issue-number>-<description>` - Bug fixes
- `hotfix/<critical-issue>` - Production hotfixes
- `release/v<version>` - Release branches
- `docs/<topic>` - Documentation updates

### Commit Message Conventions

Follow **Conventional Commits** specification:

```
<type>(<scope>): <subject>

<body>

<footer>
```

**Types:**
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting, no logic change)
- `refactor`: Code refactoring
- `perf`: Performance improvements
- `test`: Adding or updating tests
- `chore`: Build process or auxiliary tool changes
- `ci`: CI/CD pipeline changes

**Examples:**
```
feat(pharmacy): add pharmacy search filtering

Implement advanced search with filters for location,
services, and hours of operation.

Closes #123
```

```
fix(service-book): resolve date picker timezone issue

Fixed bug where service appointments were being scheduled
in incorrect timezone causing booking conflicts.

Fixes #456
```

### Pull Request Process

1. **Create Feature Branch**
   ```bash
   git checkout develop
   git pull origin develop
   git checkout -b feature/your-feature-name
   ```

2. **Make Changes**
   - Write clean, documented code
   - Follow coding standards
   - Add unit tests
   - Update documentation

3. **Commit Changes**
   ```bash
   git add .
   git commit -m "feat(module): descriptive message"
   ```

4. **Push to Remote**
   ```bash
   git push origin feature/your-feature-name
   ```

5. **Open Pull Request**
   - Title: Clear, concise description
   - Description: What, why, how
   - Link related issues
   - Add screenshots/GIFs if UI changes
   - Request reviewers

6. **Code Review**
   - Address review comments
   - Update code as needed
   - Push updates to same branch

7. **Merge Requirements**
   - ✅ All CI checks pass
   - ✅ At least 2 approvals
   - ✅ No merge conflicts
   - ✅ Code coverage maintained
   - ✅ Documentation updated

8. **Merge to Develop**
   - Use "Squash and merge" for feature branches
   - Use "Merge commit" for release branches
   - Delete feature branch after merge

### Coding Standards

#### C# / .NET
- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use **PascalCase** for public members
- Use **camelCase** for private fields, parameters
- Prefix interfaces with `I` (e.g., `ICouchRepository`)
- Use meaningful, descriptive names
- Add XML documentation comments for public APIs
- Keep methods focused (Single Responsibility Principle)
- Maximum method length: 50 lines

#### JavaScript / Vue.js
- Follow [Vue.js Style Guide](https://vuejs.org/style-guide/)
- Use **camelCase** for variables and functions
- Use **PascalCase** for component names
- 2-space indentation
- Use ES6+ features (arrow functions, destructuring, async/await)
- Prefer `const` over `let`, avoid `var`
- Add JSDoc comments for complex functions
- Component file naming: `PascalCase.vue`

#### General
- Maximum line length: 120 characters
- Use meaningful variable names (avoid `x`, `temp`, `data`)
- No commented-out code in commits
- Remove console.log statements before committing
- Use TODO comments sparingly with issue references

### Review Guidelines

**As a Reviewer:**
- ✅ Verify code meets functional requirements
- ✅ Check for security vulnerabilities
- ✅ Ensure proper error handling
- ✅ Validate test coverage
- ✅ Review for performance issues
- ✅ Confirm documentation is updated
- ✅ Provide constructive, specific feedback
- ✅ Approve or request changes within 24 hours

**Review Checklist:**
- [ ] Code compiles without errors
- [ ] All tests pass
- [ ] No security vulnerabilities introduced
- [ ] Follows coding standards
- [ ] Properly handles edge cases
- [ ] Sufficient logging added
- [ ] Documentation updated
- [ ] No hardcoded values or secrets
- [ ] API changes backward compatible (or documented)

---

## 📝 Changelog

### Version History

#### [Unreleased]
- In development

#### [1.0.0] - 2026-01-07
**Added**
- Initial release of Pharma Management System
- Pharmacy details management
- Service booking functionality
- On-site appointment tracking
- Order entry processing
- Calendar/scheduling interface
- File upload/download capabilities
- CouchDB integration
- NLog structured logging
- Application Insights monitoring
- Vue.js SPA with Vuetify UI
- RESTful API with ASP.NET Core
- User authentication and authorization
- Hours of operation management
- Contact management
- Pre/post-service workflows
- Exception handling components

**Known Issues**
- Calendar timezone handling needs improvement
- File upload size limited to 10MB
- Search functionality does not support fuzzy matching

---

## 🗺️ Roadmap

### Planned Features

#### Q1 2026
- [ ] **Advanced Search**: Fuzzy matching, filters, autocomplete
- [ ] **Mobile App**: React Native companion app
- [ ] **Reporting Dashboard**: Analytics and KPIs visualization
- [ ] **Email Notifications**: Automated service reminders
- [ ] **Multi-language Support**: i18n implementation

#### Q2 2026
- [ ] **Inventory Management**: Track pharmacy inventory levels
- [ ] **Prescription Integration**: Connect with prescription systems
- [ ] **SMS Notifications**: Two-way SMS for appointments
- [ ] **Offline Mode**: PWA with offline capabilities
- [ ] **Advanced Analytics**: ML-based insights and predictions

#### Q3 2026
- [ ] **Third-party Integrations**: Pharmacy benefit managers (PBMs)
- [ ] **E-signature Support**: Digital signatures for service completion
- [ ] **Video Consultation**: Telemedicine integration
- [ ] **Advanced Scheduling**: AI-powered technician routing

### Future Enhancements
- Blockchain for audit trails
- IoT device integration
- Voice assistant integration (Alexa, Google Assistant)
- Advanced reporting with Power BI embedding
- Multi-tenant architecture for SaaS offering

### Deprecation Plans
- **CouchDB Migration**: Evaluate migration to MongoDB or PostgreSQL by Q4 2026
- **.NET Core 3.1**: Upgrade to .NET 6+ LTS (End of support: Dec 2022)
- **Vue 2**: Migrate to Vue 3 Composition API by Q3 2026

---

## 🧯 Troubleshooting & FAQ

### Common Issues and Fixes

#### Issue: API returns 500 error on startup
**Symptoms:** PharmaCoreApi crashes immediately after starting
**Cause:** CouchDB connection failure
**Fix:**
```bash
# Check CouchDB is running
curl http://localhost:5984

# Verify credentials in appsettings.json
# Ensure database exists
curl -X PUT http://admin:password@localhost:5984/pharma_db
```

#### Issue: Vue app shows blank page
**Symptoms:** Frontend loads but displays white screen
**Cause:** Build configuration or router issue
**Fix:**
```bash
# Clear node_modules and reinstall
cd PharmaClient/clientapp
rm -rf node_modules package-lock.json
npm install

# Rebuild
npm run build

# Check browser console for errors
```

#### Issue: CORS errors in browser console
**Symptoms:** API calls fail with CORS policy error
**Cause:** Frontend origin not allowed in API CORS configuration
**Fix:**
Edit `PharmaCoreApi/Startup.cs`:
```csharp
services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins",
        builder => builder
            .WithOrigins("https://localhost:5003", "http://localhost:8080")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
```

#### Issue: File upload fails
**Symptoms:** Upload button doesn't work or returns 413 error
**Cause:** Request size limit exceeded
**Fix:**
Add to `PharmaCoreApi/Startup.cs`:
```csharp
services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 52428800; // 50 MB
});
```

### Debugging Tips

**Enable Detailed Logging:**
```json
// appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information",
      "Microsoft.AspNetCore": "Debug"
    }
  }
}
```

**Vue.js Debug Mode:**
```javascript
// main.js
Vue.config.devtools = true;
Vue.config.productionTip = false;
Vue.config.silent = false;
```

**Check Logs:**
```bash
# API logs
tail -f PharmaCoreApi/Log/internal-nlog.txt
tail -f PharmaCoreApi/Log/nlog-all-*.log

# System logs (Linux)
journalctl -u pharma-api -f
```

### Known Limitations
- Maximum file upload size: 50 MB
- Concurrent users: Tested up to 100 simultaneous users
- CouchDB view queries may be slow for large datasets (>100k records)
- Calendar component doesn't support drag-and-drop rescheduling
- Reports generated synchronously (may timeout for large datasets)

### FAQ

**Q: How do I reset my database?**
```bash
curl -X DELETE http://admin:password@localhost:5984/pharma_db
curl -X PUT http://admin:password@localhost:5984/pharma_db
```

**Q: Can I run this on Windows Server?**
A: Yes, install .NET runtime and IIS with ASP.NET Core hosting bundle.

**Q: How do I add a new API endpoint?**
A: Create method in appropriate controller, follow RESTful conventions, add route attribute.

**Q: How do I customize the Vue.js theme?**
A: Edit `PharmaClient/clientapp/src/plugins/vuetify.js` to change Vuetify theme colors.

**Q: Where are uploaded files stored?**
A: Check `FileController.cs` - likely in file system or CouchDB attachments.

---

## 👥 Ownership & Contacts

### Product Owner
- **Name**: [Product Owner Name]
- **Email**: product.owner@yourcompany.com
- **Role**: Defines product vision, prioritizes features, stakeholder liaison

### Technical Owner
- **Name**: [Tech Lead Name]
- **Email**: tech.lead@yourcompany.com
- **Role**: Architecture decisions, code reviews, technical direction

### Development Team
- **Backend Lead**: backend.lead@yourcompany.com
- **Frontend Lead**: frontend.lead@yourcompany.com
- **DevOps Engineer**: devops@yourcompany.com
- **QA Lead**: qa.lead@yourcompany.com

### Support Contacts
- **Level 1 Support**: support@yourcompany.com
- **On-Call Rotation**: oncall@yourcompany.com (24/7)
- **Business Hours**: Monday-Friday, 9 AM - 6 PM EST
- **Emergency Hotline**: +1 (555) 999-9999

### Escalation Path
1. **Level 1**: Support team (support@yourcompany.com)
2. **Level 2**: Development team leads
3. **Level 3**: Technical owner / Senior architects
4. **Critical**: Product owner + CTO

### Communication Channels
- **Slack**: #pharma-team, #pharma-support
- **Email**: pharma-team@yourcompany.com
- **Jira**: [Project Board URL]
- **Wiki**: [Confluence/Documentation URL]

---

## 📜 License

### License Type
This project is proprietary software developed by [Your Company Name].

**Copyright © 2026 [Your Company Name]. All rights reserved.**

### Usage Restrictions
- This software is licensed for internal use only
- Redistribution, modification, or commercial use is prohibited without written permission
- All rights reserved by [Your Company Name]
- Unauthorized copying, distribution, or use may result in legal action

### Third-Party Licenses
This project uses open-source components with the following licenses:
- **ASP.NET Core**: [MIT License](https://github.com/dotnet/aspnetcore/blob/main/LICENSE.txt)
- **Vue.js**: [MIT License](https://github.com/vuejs/vue/blob/dev/LICENSE)
- **Vuetify**: [MIT License](https://github.com/vuetifyjs/vuetify/blob/master/LICENSE.md)
- **NLog**: [BSD License](https://github.com/NLog/NLog/blob/dev/LICENSE.txt)
- **CouchDB**: [Apache License 2.0](https://github.com/apache/couchdb/blob/main/LICENSE)

Full list of dependencies and licenses available in `THIRD-PARTY-NOTICES.txt`

### Legal Notices
- This software may contain confidential and proprietary information
- Usage is subject to company policies and agreements
- For licensing inquiries, contact: legal@yourcompany.com
- Patent pending (if applicable)

---

## 📞 Getting Help

For questions, issues, or contributions:

1. **Check Documentation**: Review this README and code comments
2. **Search Issues**: Check existing GitHub issues
3. **Ask the Team**: Post in #pharma-team Slack channel
4. **Create Issue**: Open a new GitHub issue with details
5. **Contact Support**: Email support@yourcompany.com

---

**Built with ❤️ by the Pharma Development Team**

*Last Updated: January 7, 2026*
