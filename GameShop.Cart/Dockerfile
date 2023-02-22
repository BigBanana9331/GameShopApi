FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["packages", "/src/packages"]
RUN dotnet nuget add source "/src/packages"
COPY ["GameShop.Cart.csproj", "."] 
RUN dotnet restore "GameShop.Cart.csproj"
COPY . .

WORKDIR /src
RUN dotnet build "GameShop.Cart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameShop.Cart.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameShop.Cart.dll"]















# FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
# WORKDIR /app
# EXPOSE 7001

# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src
# COPY ["packages", "/src/packages"]
# RUN dotnet nuget add source "/src/packages"
# COPY ["GameShop.Cart.csproj", "GameShop.Cart/"] 
# RUN dotnet restore "GameShop.Cart/GameShop.Cart.csproj"
# COPY . .

# WORKDIR /src/GameShop.Cart
# RUN dotnet build "GameShop.Cart.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "GameShop.Cart.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "GameShop.Cart.dll"]