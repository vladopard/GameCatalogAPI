#malo se razlikuje u odnosu na neke druge zato sto je u c# samoj app definisano da slusa na 8080
#docker build -t gamecatalogapi:dev .
#docker run --rm -d -p 5000:8080 --name gcapi gamecatalogapi:dev

# 1) Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# 2) Build + restore
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GameCatalogAPI.csproj", "./"]
RUN dotnet restore "GameCatalogAPI.csproj"
COPY . .

# 3) Publish
FROM build AS publish
RUN dotnet publish "GameCatalogAPI.csproj" -c Release -o /app/publish

# 4) Final image
FROM base AS final
WORKDIR /app

# copy published app
COPY --from=publish /app/publish .

# include your SQLite DB
COPY GameCatalog.db* ./

# tell ASP.NET to bind to container port 80
#ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "GameCatalogAPI.dll"]
