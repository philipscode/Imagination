FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Imagination.Server/Imagination.Server.csproj", "src/Imagination.Server/"]
RUN dotnet restore "src/Imagination.Server/Imagination.Server.csproj"
COPY . .
WORKDIR "src/Imagination.Server"
RUN dotnet build "Imagination.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Imagination.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Imagination.Server.dll"]
