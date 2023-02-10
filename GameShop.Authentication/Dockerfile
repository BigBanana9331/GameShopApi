FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GameShop.Authentication/GameShop.Authentication.csproj", "GameShop.Authentication/"] 
COPY ["JwtAuthenticationManager/JwtAuthenticationManager.csproj", "JwtAuthenticationManager/"] 
RUN dotnet restore "GameShop.Authentication/GameShop.Authentication.csproj"
COPY . .
WORKDIR /src/GameShop.Authentication
RUN dotnet build "GameShop.Authentication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameShop.Authentication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameShop.Authentication.dll"]