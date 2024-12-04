# Stage 1: Build all dependencies
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY ["FoodStuffService/FoodStuffService.csproj", "./FoodStuffService/"]
COPY ["FoodStuffService.Application/FoodStuffService.Application.csproj", "./FoodStuffService.Application/"]
COPY ["FoodStuffService.Domain/FoodStuffService.Domain.csproj", "./FoodStuffService.Domain/"]
COPY ["FoodStuffService.Infrastructure/FoodStuffService.Infrastructure.csproj", "./FoodStuffService.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "FoodStuffService/FoodStuffService.csproj"

# Copy the full solution
COPY . .

# Build all projects
RUN dotnet build "FoodStuffService/FoodStuffService.csproj" -c Release

# Stage 2: Run tests
FROM build AS test
WORKDIR /src/FoodStuffServiceTests

# Run tests and ensure they pass
RUN dotnet test "FoodStuffServiceTests.csproj" --no-build --logger:trx --results-directory /src/TestResults

# Stage 3: Publish FoodStuffService (only proceeds if tests pass)
FROM build AS publish
WORKDIR /src/FoodStuffService
RUN dotnet publish "FoodStuffService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime image for FoodStuffService
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodStuffService.dll"]