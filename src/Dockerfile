# No Multi Stage build in GKE version 1.10.7-gke.6
FROM microsoft/dotnet:2.1-sdk
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish "HappyBirthdayWorld.Api.csproj" -c Release -o out

ENTRYPOINT ["dotnet", "out/HappyBirthdayWorld.Api.dll"]