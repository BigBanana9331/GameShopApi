FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "GameShop.Cart.Service/GameShop.Cart.Service.csproj"


WORKDIR /src
RUN dotnet build "GameShop.Cart.Service/GameShop.Cart.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameShop.Cart.Service/GameShop.Cart.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameShop.Cart.Service.dll"]



























# FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# # WORKDIR /src
# # # COPY ["packages", "/src/packages"]
# # # COPY ["../packages", "/src/packages"]
# # # RUN dotnet nuget add source "/src/packages"
# # COPY ["GameShop.Cart.Service.csproj", "."] 
# # RUN dotnet restore "GameShop.Cart.Service.csproj"
# # COPY . .



# WORKDIR /src
# RUN dotnet build "GameShop.Cart.Service.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "GameShop.Cart.Service.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "GameShop.Cart.Service.dll"]















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