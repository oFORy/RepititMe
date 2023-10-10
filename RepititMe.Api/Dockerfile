#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RepititMe.Api/RepititMe.Api.csproj", "RepititMe.Api/"]
COPY ["RepititMe.Application/RepititMe.Application.csproj", "RepititMe.Application/"]
COPY ["RepititMe.Domain/RepititMe.Domain.csproj", "RepititMe.Domain/"]
COPY ["RepititMe.Infrastructure/RepititMe.Infrastructure.csproj", "RepititMe.Infrastructure/"]
RUN dotnet restore "RepititMe.Api/RepititMe.Api.csproj"
COPY . .
WORKDIR "/src/RepititMe.Api"
RUN dotnet build "RepititMe.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RepititMe.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RepititMe.Api.dll"]