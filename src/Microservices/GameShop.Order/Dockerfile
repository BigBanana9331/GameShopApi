FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "GameShop.Order.Service/GameShop.Order.Service.csproj"


WORKDIR /src
RUN dotnet build "GameShop.Order.Service/GameShop.Order.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameShop.Order.Service/GameShop.Order.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameShop.Order.Service.dll"]













# FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
# WORKDIR /app
# EXPOSE 8001


# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src
# # COPY ["packages", "/src/packages"]
# # # COPY ["../packages", "/src/packages"]
# # RUN dotnet nuget add source "/src/packages"
# COPY ["./GameShop.Order/GameShop.Order.csproj", "."] 
# RUN dotnet restore "GameShop.Order.csproj"
# COPY ./GameShop.Order .

# WORKDIR /src
# RUN dotnet build "GameShop.Order.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "GameShop.Order.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "GameShop.Order.dll"]