#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Plateaumed.EHR.Migrator/Plateaumed.EHR.Migrator.csproj", "src/Plateaumed.EHR.Migrator/"]
COPY ["src/Plateaumed.EHR.EntityFrameworkCore/Plateaumed.EHR.EntityFrameworkCore.csproj", "src/Plateaumed.EHR.EntityFrameworkCore/"]
COPY ["src/Plateaumed.EHR.Core/Plateaumed.EHR.Core.csproj", "src/Plateaumed.EHR.Core/"]
COPY ["src/Plateaumed.EHR.Core.Shared/Plateaumed.EHR.Core.Shared.csproj", "src/Plateaumed.EHR.Core.Shared/"]
RUN dotnet restore "src/Plateaumed.EHR.Migrator/Plateaumed.EHR.Migrator.csproj"
COPY . .
WORKDIR "/src/src/Plateaumed.EHR.Migrator"
RUN dotnet build "Plateaumed.EHR.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Plateaumed.EHR.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Plateaumed.EHR.Migrator.dll", "-s"]
