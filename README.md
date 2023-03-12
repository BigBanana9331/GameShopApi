# GameShopAPI project

This is a REST API project for game key shop using .NET 7 based on microservices architecture

## System design

This system have several microservices include: [Catalog](/docs/catalog-api.md), [Cart](/docs/cart-api.md), [Order](/docs/order-api.md), and [User](/docs/user-api.md).
![System overview](/docs/assets/gameshopapi.drawio.png).

<!-- ## Versioning
| GitHub Release | .NET Core Version | Diagnostics HealthChecks Version |
|----------------|------------ |---------------------|
| main | 6.0.100-preview.6.21355.2 | 2.2.0 | -->

<!-- ## Project Structure
```
│   .gitignore
│   docker-compose.yaml
│   GameShop.sln
│   README.md
│
├───.vscode
│       launch.json
│       tasks.json
│
├───ApiGateway
│   │   ApiGateway.csproj
│   │   appsettings.Development.json
│   │   appsettings.json
│   │   Dockerfile
│   │   ocelot.json
│   │   Program.cs
│   │
│   └───Properties
│           launchSettings.json
│
├───docs
│   │   cart-api.md
│   │   catalog-api.md
│   │   order-api.md
│   │   user-api.md
│   │
│   └───assets
│           gameshopapi.drawio.png
│
├───GameShop.Cart
│   │   .dockerignore
│   │   appsettings.Development.json
│   │   appsettings.json
│   │   cart-depl.yaml
│   │   Dockerfile
│   │   GameShop.Cart.csproj
│   │   Program.cs
│   │
│   ├───Client
│   │       GameClient.cs
│   │
│   ├───Consumer
│   │       CatalogItemCreatedConsumer.cs
│   │       CatalogItemDeletedConsumer.cs
│   │       CatalogItemUpdatedConsumer.cs
│   │       CustomerCreatedConsumer.cs
│   │       CustomerDeletedConsumer.cs
│   │
│   ├───Controllers
│   │       CartsController.cs
│   │
│   ├───Entities
│   │       CartItem.cs
│   │       CatalogItem.cs
│   │       Customer.cs
│   │       CustomerCart.cs
│   │
│   └───Properties
│           launchSettings.json
│
├───GameShop.Catalog
│   │   .dockerignore
│   │   appsettings.Development.json
│   │   appsettings.json
│   │   catalog-depl.yaml
│   │   Dockerfile
│   │   GameShop.Catalog.csproj
│   │   Program.cs
│   │
│   ├───Controllers
│   │       GamesController.cs
│   │
│   ├───Entities
│   │       Game.cs
│   │
│   └───Properties
│           launchSettings.json
│
├───GameShop.Common
│   │   GameShop.Common.csproj
│   │   IEntity.cs
│   │   IRepository.cs
│   │
│   ├───Authorization
│   │       BadgeEntryHandler.cs
│   │       BuildingEntryRequirement.cs
│   │       Extensions.cs
│   │       MinimumAgeHandler.cs
│   │       MinimumAgeRequirement.cs
│   │       PermissionHandler.cs
│   │       TemporaryStickerHandler.cs
│   │
│   ├───Jwt
│   │       Extensions.cs
│   │
│   ├───MassTransit
│   │       Extensions.cs
│   │
│   ├───MongoDB
│   │       Extensions.cs
│   │       MongoRepository.cs
│   │
│   └───Settings
│           JwtSettings.cs
│           MongoDbSettings.cs
│           RabbitMQSettings.cs
│           ServiceSettings.cs
│
├───GameShop.Contract
│   │   GameShop.Contract.csproj
│   │
│   ├───Cart
│   │       CartItemRequest.cs
│   │       CartItemResponse.cs
│   │       CartResponse.cs
│   │
│   ├───Game
│   │       GameCreated.cs
│   │       GameDeleted.cs
│   │       GameRequest.cs
│   │       GameResponse.cs
│   │       GameUpdated.cs
│   │
│   ├───Order
│   │       OrderItemRequest.cs
│   │       OrderItemResponse.cs
│   │       OrderRequest.cs
│   │       OrderResponse.cs
│   │
│   └───User
│           AuthResult.cs
│           LoginRequest.cs
│           LoginResponse.cs
│           RegisterUserRequest.cs
│           TokenRequest.cs
│           UpdateUserRequest.cs
│           UserCreated.cs
│           UserDeleted.cs
│           UserReponse.cs
│           UserRequest.cs
│           UserUpdated.cs
│
├───GameShop.Order
│   │   .dockerignore
│   │   appsettings.Development.json
│   │   appsettings.json
│   │   Dockerfile
│   │   GameShop.Order.csproj
│   │   Program.cs
│   │
│   ├───Consumer
│   │       CatalogItemCreatedConsumer.cs
│   │       CatalogItemDeletedConsumer.cs
│   │       CatalogItemUpdatedConsumer.cs
│   │       CustomerCreatedConsumer.cs
│   │       CustomerDeletedConsumer.cs
│   │
│   ├───Controllers
│   │       OrdersController.cs
│   │
│   ├───Entities
│   │       CatalogItem.cs
│   │       Customer.cs
│   │       OrderItem.cs
│   │       OrderSumary.cs
│   │
│   └───Properties
│           launchSettings.json
│
├───GameShop.User
│   │   .dockerignore
│   │   appsettings.Development.json
│   │   appsettings.json
│   │   Dockerfile
│   │   GameShop.User.csproj
│   │   Program.cs
│   │
│   ├───Controllers
│   │       UsersController.cs
│   │
│   ├───Entities
│   │       RefreshToken.cs
│   │       Token.cs
│   │       UserAccount.cs
│   │
│   ├───Properties
│   │       launchSettings.json
│   │
│   └───Services
│           Extensions.cs
│           IJwtTokenHandler.cs
│           IPasswordHandler.cs
│           JwtTokenHandler.cs
│           PasswordHandler.cs
│
├───K8S
│       catalog-api.yaml
│
└───packages
        GameShop.Common.1.0.1.nupkg
        GameShop.Contract.1.0.1.nupkg
```

- `Dockerfile` is .NET Core Web API Multistage Dockerfile (following Docker Best Practices)
- `KubernetesLocalProcessConfig.yaml` is [Bridge to Kubernetes](https://devblogs.microsoft.com/visualstudio/bridge-to-kubernetes-ga/) config to supports developing .NET Core Web API microservice on Kubernetes
- `configs` folder will contain .NET Core Web API centralized config structure
- `appsettings.Development.json` is .NET Core Web API development environment config
- `manifests` folder will contain Kubernetes manifests (deployment, service)
- `Startup.cs` is .NET Core Web API startup & path routing config
- `Program.cs` is .NET Core Web API environment variable mapping config  -->

## Setting Up

To setup this project, you need to clone the git repo

```sh
$ git clone https://github.com/BigBanana9331/GameShopApi.git
$ cd GameShopApi
```

followed by

```sh
$ docker compose up
```

<!-- ## Deploying a .NET Core Web API microservice on Kubernetes

### Prerequisite:

- .NET Core Web API Docker Image

Preparing Config Map for .NET Core Web API microservice

```sh
$ kubectl apply -k configs/prod
```

To deploy the microservice on Kubernetes, run following command:

```sh
$ kubectl apply -f manifests
```

This will deploy it on Kubernetes with the centralized config.

## Deploying a .NET Core Web API microservice on Azure Container Instance (ACI)

### Prerequisite:

- [ACI Context](https://docs.docker.com/cloud/aci-integration/#run-docker-containers-on-aci)


To deploy the microservice on ACI, run following command:

```sh
$ docker compose -f aci-docker-compose.yaml up -d
```

## Deploying a .NET Core Web API microservice on [AWS App Runner](https://aws.amazon.com/apprunner/) using AWS Copilot

### Prerequisite:

- [AWS Copilot](https://aws.github.io/copilot-cli/docs/getting-started/install/)

To deploy the microservice on AWS, following these steps:

- Prepare AWS IAM roles and AWS ECR repository for the microservice

```sh
$ copilot init --app kubeops-demo
```

- Create the test environment on AWS

```sh
$ copilot env init --name test --app kubeops-demo
```

- Deploy the microservice on the test environment

```sh
$ copilot svc deploy --env test
```


## Learning Resources:

- [.NET Thailand](https://www.dotnetthailand.com/)
- [Announcing .NET 6 Preview 4](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-6-preview-4/)
- [Breaking changes in .NET 6](https://docs.microsoft.com/en-us/dotnet/core/compatibility/6.0) -->
