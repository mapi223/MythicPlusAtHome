FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project files and restore dependencies
COPY ConfigurtionService.csproj ./
RUN dotnet restore "./ConfigurtionService.csproj"

# Copy the rest of the source code and build it
COPY . . 
RUN dotnet build "./ConfigurtionService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app for production
FROM build AS publish
RUN dotnet publish "./ConfigurtionService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Set up the final image with the .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Copy the published files from the build stage
COPY --from=publish /app/publish ./

# Define the entry point for the container
ENTRYPOINT ["dotnet", "ConfigurtionService.dll"]
