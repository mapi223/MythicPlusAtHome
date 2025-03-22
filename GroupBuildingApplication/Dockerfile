# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the .csproj file and restore any dependencies
COPY GroupBuildingService.csproj ./
RUN dotnet restore "./GroupBuildingService.csproj"

# Copy the rest of the files and build the app
COPY . . 
RUN dotnet build "./GroupBuildingService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app for production
FROM build AS publish
RUN dotnet publish "./GroupBuildingService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Set up the final image with the .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8082

# Copy the published files from the build stage
COPY --from=publish /app/publish ./

# Define the entry point for the container
ENTRYPOINT ["dotnet", "GroupBuildingService.dll"]
