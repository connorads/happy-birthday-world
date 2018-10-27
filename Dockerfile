FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["happy-birthday-world.api.csproj", ""]
RUN dotnet restore "/happy-birthday-world.api.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "happy-birthday-world.api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "happy-birthday-world.api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "happy-birthday-world.api.dll"]