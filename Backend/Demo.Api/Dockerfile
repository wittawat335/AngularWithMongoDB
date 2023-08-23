#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Demo.Api/Demo.Api.csproj", "Demo.Api/"]
COPY ["Demo.Core/Demo.Core.csproj", "Demo.Core/"]
COPY ["Demo.Domain/Demo.Domain.csproj", "Demo.Domain/"]
COPY ["Demo.Infrastructure/Demo.Infrastructure.csproj", "Demo.Infrastructure/"]
RUN dotnet restore "Demo.Api/Demo.Api.csproj"
COPY . .
WORKDIR "/src/Demo.Api"
RUN dotnet build "Demo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.Api.dll"]