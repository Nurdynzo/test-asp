#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Plateaumed.EHR.Web.Public/Plateaumed.EHR.Web.Public.csproj", "src/Plateaumed.EHR.Web.Public/"]
COPY ["src/Plateaumed.EHR.Web.Core/Plateaumed.EHR.Web.Core.csproj", "src/Plateaumed.EHR.Web.Core/"]
COPY ["src/Plateaumed.EHR.Application/Plateaumed.EHR.Application.csproj", "src/Plateaumed.EHR.Application/"]
COPY ["src/Plateaumed.EHR.Application.Shared/Plateaumed.EHR.Application.Shared.csproj", "src/Plateaumed.EHR.Application.Shared/"]
COPY ["src/Plateaumed.EHR.Core.Shared/Plateaumed.EHR.Core.Shared.csproj", "src/Plateaumed.EHR.Core.Shared/"]
COPY ["src/Plateaumed.EHR.Core/Plateaumed.EHR.Core.csproj", "src/Plateaumed.EHR.Core/"]
COPY ["src/Plateaumed.EHR.EntityFrameworkCore/Plateaumed.EHR.EntityFrameworkCore.csproj", "src/Plateaumed.EHR.EntityFrameworkCore/"]
COPY ["src/Plateaumed.EHR.GraphQL/Plateaumed.EHR.GraphQL.csproj", "src/Plateaumed.EHR.GraphQL/"]
RUN dotnet restore "src/Plateaumed.EHR.Web.Public/Plateaumed.EHR.Web.Public.csproj"
COPY . .
WORKDIR "/src/src/Plateaumed.EHR.Web.Public"
RUN dotnet build "Plateaumed.EHR.Web.Public.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Plateaumed.EHR.Web.Public.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Plateaumed.EHR.Web.Public.dll"]
