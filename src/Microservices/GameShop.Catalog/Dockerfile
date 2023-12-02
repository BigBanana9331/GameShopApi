FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "GameShop.Catalog.Service/GameShop.Catalog.Service.csproj"


WORKDIR /src
RUN dotnet build "GameShop.Catalog.Service/GameShop.Catalog.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameShop.Catalog.Service/GameShop.Catalog.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameShop.Catalog.Service.dll"]









# FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src
# # COPY ["packages", "/src/packages"]
# # # COPY ["../packages", "/src/packages"]
# # RUN dotnet nuget add source "/src/packages"
# COPY ["./GameShop.Catalog/GameShop.Catalog.csproj", "."] 
# RUN dotnet restore "GameShop.Catalog.csproj"
# COPY ./GameShop.Catalog .

# WORKDIR /src
# RUN dotnet build "GameShop.Catalog.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "GameShop.Catalog.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "GameShop.Catalog.dll"]










# FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src
# COPY ["GameShop.Catalog/GameShop.Catalog.csproj", "GameShop.Catalog/"] 
# RUN dotnet restore "GameShop.Authentication/GameShop.Authentication.csproj"
# COPY . .
# WORKDIR /src/GameShop.Catalog
# RUN dotnet build "GameShop.Catalog.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "GameShop.Catalog.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "GameShop.Catalog.dll"]

