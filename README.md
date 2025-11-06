# README

Author: Dat NguyenChanh
dljfndsfndskf

Describe:
A BE Repo for healthcare .NET(CSharp) version (see [TS version](https://github.com/NguyenChanhDat/workout-tracking-TS) here) services system in TS implements Clean Architecture. You can also view the project's folder structure inside **doc** folder.I also add Unit Test for each Component which stored inside **test** folder. Thanks for viewing this repo, feel free to drop a star if you find this repo helpful :))

## DEPENDENCY

- [Docker Desktop **27.4.0**](https://docs.docker.com/engine/release-notes/27/#2741)
- [EntityFramework](https://learn.microsoft.com/en-us/ef)
- [.NET](https://dotnet.microsoft.com/en-us)

## DEVELOPING

### Setup

- Install Dotnet

```link
https://dotnet.microsoft.com/en-us/download
```

- Running Setup

```bash
dotnet restore .\src\FirstNETWebApp.csproj
```

### Start local server

```bash
dotnet run --project .\src\FirstNETWebApp.csproj
```

## TESTING

### Unit testing

```bash
dotnet test .\tests\FirstNETWebApp.Tests.csproj
```

