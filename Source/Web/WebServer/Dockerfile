#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Source/Web/WebServer/WebServer.csproj", "Source/Web/WebServer/"]
COPY ["Source/Modules/ChannelModule/Web/Server/ChannelModule.Server.csproj", "Source/Modules/ChannelModule/Web/Server/"]
COPY ["Source/Shared/SharedKernel/SharedKernel.csproj", "Source/Shared/SharedKernel/"]
COPY ["Source/Modules/ChannelModule/Layers/Application/ChannelModule.Application.csproj", "Source/Modules/ChannelModule/Layers/Application/"]
COPY ["Source/Shared/Modules/Layers/Application/ModulesShared.Application.csproj", "Source/Shared/Modules/Layers/Application/"]
COPY ["Source/Shared/Modules/Layers/Infrastructure/ModulesShared.Infrastructure.csproj", "Source/Shared/Modules/Layers/Infrastructure/"]
COPY ["Source/Shared/Modules/Layers/Domain/ModulesShared.Domain.csproj", "Source/Shared/Modules/Layers/Domain/"]
COPY ["Source/Modules/ChannelModule/Layers/Infrastructure/ChannelModule.Infrastructure.csproj", "Source/Modules/ChannelModule/Layers/Infrastructure/"]
COPY ["Source/Modules/ChannelModule/Layers/Domain/ChannelModule.Domain.csproj", "Source/Modules/ChannelModule/Layers/Domain/"]
COPY ["Source/Modules/ChannelModule/Web/DTOs/ChannelModule.DTOs.csproj", "Source/Modules/ChannelModule/Web/DTOs/"]
COPY ["Source/Modules/SubscriptionModule/Web/Server/SubscriptionModule.Server.csproj", "Source/Modules/SubscriptionModule/Web/Server/"]
COPY ["Source/Modules/SubscriptionModule/Layers/Application/SubscriptionModule.Application.csproj", "Source/Modules/SubscriptionModule/Layers/Application/"]
COPY ["Source/Modules/SubscriptionModule/Layers/Infrastructure/SubscriptionModule.Infrastructure.csproj", "Source/Modules/SubscriptionModule/Layers/Infrastructure/"]
COPY ["Source/Modules/SubscriptionModule/Layers/Domain/SubscriptionModule.Domain.csproj", "Source/Modules/SubscriptionModule/Layers/Domain/"]
COPY ["Source/Modules/TenantIdentityModule/Web/Server/TenantIdentityModule.Server.csproj", "Source/Modules/TenantIdentityModule/Web/Server/"]
COPY ["Source/Modules/TenantIdentityModule/Layers/Application/TenantIdentityModule.Application.csproj", "Source/Modules/TenantIdentityModule/Layers/Application/"]
COPY ["Source/Modules/TenantIdentityModule/Layers/Infrastructure/TenantIdentityModule.Infrastructure.csproj", "Source/Modules/TenantIdentityModule/Layers/Infrastructure/"]
COPY ["Source/Modules/TenantIdentityModule/Layers/Domain/TenantIdentityModule.Domain.csproj", "Source/Modules/TenantIdentityModule/Layers/Domain/"]
COPY ["Source/Modules/TenantIdentityModule/Web/DTOs/TenantIdentityModule.DTOs.csproj", "Source/Modules/TenantIdentityModule/Web/DTOs/"]
COPY ["Source/Shared/Modules/Web/Server/ModulesShared.Server.csproj", "Source/Shared/Modules/Web/Server/"]
COPY ["Source/Web/WebWasmClient/WebWasmClient.csproj", "Source/Web/WebWasmClient/"]
COPY ["Source/Modules/ChannelModule/Web/Client/ChannelModule.Client.csproj", "Source/Modules/ChannelModule/Web/Client/"]
COPY ["Source/Shared/Modules/Web/Client/ModulesShared.Client.csproj", "Source/Shared/Modules/Web/Client/"]
COPY ["Source/Modules/SubscriptionModule/Web/Client/SubscriptionModule.Client.csproj", "Source/Modules/SubscriptionModule/Web/Client/"]
COPY ["Source/Modules/SubscriptionModule/Web/DTOs/SubscriptionModule.DTOs.csproj", "Source/Modules/SubscriptionModule/Web/DTOs/"]
COPY ["Source/Modules/TenantIdentityModule/Web/Client/TenantIdentityModule.Client.csproj", "Source/Modules/TenantIdentityModule/Web/Client/"]
RUN dotnet restore "Source/Web/WebServer/WebServer.csproj"
COPY . .
WORKDIR "/src/Source/Web/WebServer"
RUN dotnet build "WebServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebServer.dll"]