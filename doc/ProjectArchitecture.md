This project follow Clean Architecture

```
FirstNETWebApp
├─ application
├─ appsettings.Development.json
├─ appsettings.json
├─ doc
│  └─ ProjectArchitecture.md
├─ commands
│  ├─ genType.ps1
│  └─ setup.ps1
├─ core
│  ├─ entities
│  │  ├─ Exercise.cs
│  ├─ enums
│  │  ├─ MembershipTierEnum.cs
│  │  ├─ MuscleEnum.cs
│  │  └─ RoleEnum.cs
│  └─ repository
│     └─ IBaseRepository.cs
├─ Dockerfile.db
├─ FirstNETWebApp.csproj
├─ FirstNETWebApp.http
├─ FirstNETWebApp.sln
├─ infrastructure
│  ├─ database
│  │  ├─ aws
│  │  │  ├─ dynamodb
│  │  │  └─ s3
│  │  └─ entityFramework
│  │     ├─ data
│  │     ├─ dbContext
│  │     ├─ EFBaseRepository.cs
│  │     └─ models
│  └─ security
├─ Program.cs
├─ Properties
│  └─ launchSettings.json
├─ ToDo.md
└─ use-case

```
