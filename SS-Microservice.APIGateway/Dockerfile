#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["./SS-Microservice.Common/SS-Microservice.Common.csproj", "SS-Microservice.Common/"]
COPY ["./SS-Microservice.Contracts/SS-Microservice.Contracts.csproj", "SS-Microservice.Contracts/"]
COPY ["./SS-Microservice.APIGateway/SS-Microservice.APIGateway.csproj", "SS-Microservice.APIGateway/"]

RUN dotnet restore "/src/SS-Microservice.APIGateway/SS-Microservice.APIGateway.csproj"
COPY . .
WORKDIR "/src/SS-Microservice.APIGateway"
RUN dotnet build "SS-Microservice.APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SS-Microservice.APIGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SS-Microservice.APIGateway.dll", "--environment=Docker"]