# Use the .NET runtime image for the base layer
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8086
EXPOSE 8087

# Use the .NET SDK image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the UserApplication.csproj to the working directory
COPY UserApplication.csproj ./

# Restore dependencies for UserApplication
RUN dotnet restore "./UserApplication.csproj"

# Copy the rest of the project files
COPY . .

# Set the working directory to UserApplication for build
WORKDIR "/src/UserApplication"
RUN dotnet build "./UserApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "./UserApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the runtime image for the final stage
FROM base AS final
WORKDIR /app

# Copy the published files from the previous stage
COPY --from=publish /app/publish ./

# Set the entry point for the container
ENTRYPOINT ["dotnet", "UserApplication.dll"]
