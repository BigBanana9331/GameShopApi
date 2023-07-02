FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "GameShop.User.Service/GameShop.User.Service.csproj"


WORKDIR /src
RUN dotnet build "GameShop.User.Service/GameShop.User.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameShop.User.Service/GameShop.User.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameShop.User.Service.dll"]














# FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src
# # COPY ["packages", "/src/packages"]
# # # COPY ["../packages", "/src/packages"]
# # RUN dotnet nuget add source "/src/packages"
# COPY ["./GameShop.User/GameShop.User.csproj", "."] 
# RUN dotnet restore "GameShop.User.csproj"
# COPY ./GameShop.User .

# WORKDIR /src
# RUN dotnet build "GameShop.User.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "GameShop.User.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "GameShop.User.dll"]








# https://hub.docker.com/_/microsoft-dotnet
# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /source

# # copy csproj and restore as distinct layers
# COPY *.sln .
# COPY aspnetapp/*.csproj ./aspnetapp/
# RUN dotnet restore

# # copy everything else and build app
# COPY aspnetapp/. ./aspnetapp/
# WORKDIR /source/aspnetapp
# RUN dotnet publish -c release -o /app --no-restore

# # final stage/image
# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# WORKDIR /app
# COPY --from=build /app ./
# ENTRYPOINT ["dotnet", "aspnetapp.dll"]


# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# ARG source
# WORKDIR /app
# EXPOSE 80
# COPY ${source:-obj/Docker/publish} .
# ENTRYPOINT ["dotnet", " MySingleContainerWebApp.dll "]


# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src
# COPY MyMicroservice.csproj .
# RUN dotnet restore
# COPY . .
# RUN dotnet publish -c release -o /app

# FROM mcr.microsoft.com/dotnet/aspnet:7.0
# WORKDIR /app
# COPY --from=build /app .
# ENTRYPOINT ["dotnet", "MyMicroservice.dll"]