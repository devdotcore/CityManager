
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS builder  

# EXPOSE 5000:8443
# EXPOSE 8443:8443
# EXPOSE 443:8443
EXPOSE 80

WORKDIR /app  

COPY ./*.sln ./nuget.config  ./

# Copy the main source project files
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

# Copy the test project files
COPY test/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p test/${file%.*}/ && mv $file test/${file%.*}/; done

# Restore to cache the layers
RUN dotnet restore

# Copy all the source code and build
COPY ./test ./test  
COPY ./src ./src  
RUN dotnet build -c Release --no-restore

# Run dotnet test on the solution
RUN dotnet test "./test/CityManager.Tests/CityManager.Tests.csproj" -c Release --no-build --no-restore

RUN dotnet publish "./src/CityManager/CityManager.csproj" -c Release -o /publish/ --no-restore

#App image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 
WORKDIR /publish  
COPY --from=builder /publish .  
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
ENTRYPOINT ["dotnet", "CityManager.dll"]
