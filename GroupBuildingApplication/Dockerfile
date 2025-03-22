# Use the .NET runtime image for the base layer
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8084
EXPOSE 8085

# Use the .NET SDK image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the GroupBuildingService.csproj to the working directory
COPY GroupBuildingService.csproj ./

# Restore dependencies for GroupBuildingService
RUN dotnet restore "./GroupBuildingService.csproj"

# Copy the rest of the project files
COPY . .

# Set the working directory to GroupBuildingService for build
WORKDIR "/src/GroupBuildingService"
RUN dotnet build "./GroupBuildingService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "./GroupBuildingService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the runtime image for the final stage
FROM base AS final
WORKDIR /app

# Copy the published files from the previous stage
COPY --from=publish /app/publish ./ 

# Set the entry point for the container
ENTRYPOINT ["dotnet", "GroupBuildingService.dll"]
