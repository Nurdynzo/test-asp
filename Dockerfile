#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Plateaumed.EHR.Web.Host/Plateaumed.EHR.Web.Host.csproj", "src/Plateaumed.EHR.Web.Host/"]
COPY ["src/Plateaumed.EHR.Web.Core/Plateaumed.EHR.Web.Core.csproj", "src/Plateaumed.EHR.Web.Core/"]
COPY ["src/Plateaumed.EHR.GraphQL/Plateaumed.EHR.GraphQL.csproj", "src/Plateaumed.EHR.GraphQL/"]
COPY ["src/Plateaumed.EHR.Application.Shared/Plateaumed.EHR.Application.Shared.csproj", "src/Plateaumed.EHR.Application.Shared/"]
COPY ["src/Plateaumed.EHR.Core.Shared/Plateaumed.EHR.Core.Shared.csproj", "src/Plateaumed.EHR.Core.Shared/"]
COPY ["src/Plateaumed.EHR.Core/Plateaumed.EHR.Core.csproj", "src/Plateaumed.EHR.Core/"]
COPY ["src/Plateaumed.EHR.Application/Plateaumed.EHR.Application.csproj", "src/Plateaumed.EHR.Application/"]
COPY ["src/Plateaumed.EHR.EntityFrameworkCore/Plateaumed.EHR.EntityFrameworkCore.csproj", "src/Plateaumed.EHR.EntityFrameworkCore/"]
RUN dotnet restore "./src/Plateaumed.EHR.Web.Host/./Plateaumed.EHR.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/Plateaumed.EHR.Web.Host"
RUN dotnet build "./Plateaumed.EHR.Web.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Plateaumed.EHR.Web.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Plateaumed.EHR.Web.Host.dll"]
