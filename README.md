
```
FirstNETWebApp
├─ .config
│  └─ dotnet-tools.json
├─ .husky
│  ├─ pre-commit
│  ├─ task-runner.json
│  └─ _
│     └─ husky.sh
├─ appsettings.Development.json
├─ appsettings.json
├─ commands
│  ├─ genType.ps1
│  └─ setup.ps1
├─ doc
│  └─ ProjectArchitecture.md
├─ Dockerfile.db
├─ FirstNETWebApp.http
├─ FirstNETWebApp.sln
├─ Migrations
│  ├─ 20251023083400_InitialCreate.cs
│  ├─ 20251023083400_InitialCreate.Designer.cs
│  └─ DatabaseContextModelSnapshot.cs
├─ Properties
│  └─ launchSettings.json
├─ src
│  ├─ core
│  │  ├─ entities
│  │  │  ├─ BodyTrack.cs
│  │  │  ├─ Exercise.cs
│  │  │  ├─ Plan.cs
│  │  │  ├─ Session.cs
│  │  │  ├─ Set.cs
│  │  │  └─ User.cs
│  │  ├─ enums
│  │  │  ├─ MembershipTierEnum.cs
│  │  │  ├─ MuscleEnum.cs
│  │  │  └─ RoleEnum.cs
│  │  ├─ repository
│  │  │  ├─ IBaseRepository.cs
│  │  │  ├─ IBodyTrackRepository.cs
│  │  │  ├─ IExerciseRepository.cs
│  │  │  ├─ IPlanRepository.cs
│  │  │  ├─ ISessionRepository.cs
│  │  │  ├─ ISetRepository.cs
│  │  │  └─ IUserRepository.cs
│  │  └─ unitOfWork
│  │     └─ IUnitOfWork.cs
│  ├─ FirstNETWebApp copy.csproj
│  ├─ FirstNETWebApp.csproj
│  ├─ infrastructure
│  │  ├─ database
│  │  │  ├─ aws
│  │  │  │  ├─ dynamodb
│  │  │  │  └─ s3
│  │  │  └─ entityFramework
│  │  │     ├─ AppDbContext.cs
│  │  │     ├─ EFBaseRepository.cs
│  │  │     ├─ EFUnitOfWork.cs
│  │  │     └─ repository
│  │  │        ├─ BodyTrackRepository.cs
│  │  │        ├─ ExerciseRepository.cs
│  │  │        ├─ PlanRepository.cs
│  │  │        ├─ SessionRepository.cs
│  │  │        ├─ SetRepository.cs
│  │  │        └─ UserRepository.cs
│  │  └─ security
│  ├─ Program.cs
│  └─ use-case
│     ├─ base
│     │  └─ IUseCase.cs
│     ├─ createUser
│     │  ├─ CreateUserService.cs
│     │  ├─ CreateUserUseCase.cs
│     │  ├─ dtos
│     │  │  ├─ CreateUserRequest.cs
│     │  │  └─ CreateUserResponse.cs
│     │  ├─ helpers
│     │  │  └─ ASPHashService.cs
│     │  └─ interfaces
│     │     ├─ ICreateUserService.cs
│     │     └─ IHashService.cs
│     └─ updateUser
├─ tests
│  ├─ core
│  │  ├─ BaseRepo.cs
│  │  └─ mock
│  │     └─ MockUserRepository.cs
│  ├─ FirstNETWebApp.Tests.csproj
│  ├─ infrastructure
│  └─ use-case
└─ ToDo.md
└─ .env

```