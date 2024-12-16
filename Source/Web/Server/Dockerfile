# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/Web/Server/Web.Server.csproj", "Source/Web/Server/"]
COPY ["Source/Modules/Channels/Web/Server/Modules.Channels.Web.Server.csproj", "Source/Modules/Channels/Web/Server/"]
COPY ["Source/Shared/Features/Shared.Features.csproj", "Source/Shared/Features/"]
COPY ["Source/Shared/Kernel/Shared.Kernel.csproj", "Source/Shared/Kernel/"]
COPY ["Source/Modules/Channels/Features/Modules.Channels.Features.csproj", "Source/Modules/Channels/Features/"]
COPY ["Source/Modules/Channels/Shared/Modules.Channels.Shared.csproj", "Source/Modules/Channels/Shared/"]
COPY ["Source/Modules/LandingPages/Web/Server/Modules.LandingPages.Server.csproj", "Source/Modules/LandingPages/Web/Server/"]
COPY ["Source/Modules/Subscriptions/Web/Server/Modules.Subscriptions.Web.Server.csproj", "Source/Modules/Subscriptions/Web/Server/"]
COPY ["Source/Modules/Subscriptions/Features/Modules.Subscriptions.Features.csproj", "Source/Modules/Subscriptions/Features/"]
COPY ["Source/Modules/TenantIdentity/IntegrationEvents/Modules.TenantIdentity.IntegrationEvents.csproj", "Source/Modules/TenantIdentity/IntegrationEvents/"]
COPY ["Source/Modules/Subscriptions/IntegrationEvents/Modules.Subscriptions.IntegrationEvents.csproj", "Source/Modules/Subscriptions/IntegrationEvents/"]
COPY ["Source/Modules/TenantIdentity/Web/Server/Modules.TenantIdentity.Web.Server.csproj", "Source/Modules/TenantIdentity/Web/Server/"]
COPY ["Source/Modules/TenantIdentity/Features/Modules.TenantIdentity.Features/Modules.TenantIdentity.Features.csproj", "Source/Modules/TenantIdentity/Features/Modules.TenantIdentity.Features/"]
COPY ["Source/Modules/TenantIdentity/Shared/Modules.TenantIdentity.Shared.csproj", "Source/Modules/TenantIdentity/Shared/"]
COPY ["Source/Modules/Subscriptions/Shared/Modules.Subscriptions.Shared.csproj", "Source/Modules/Subscriptions/Shared/"]
COPY ["Source/Web/Client/Web.Client.csproj", "Source/Web/Client/"]
COPY ["Source/Modules/Channels/Web/Client/Modules.Channels.Web.Client.csproj", "Source/Modules/Channels/Web/Client/"]
COPY ["Source/Shared/Client/Shared.Client.csproj", "Source/Shared/Client/"]
COPY ["Source/Modules/TenantIdentity/Web/Client/Modules.TenantIdentity.Web.Client.csproj", "Source/Modules/TenantIdentity/Web/Client/"]
RUN dotnet restore "./Source/Web/Server/Web.Server.csproj"
COPY . .
WORKDIR "/src/Source/Web/Server"
RUN dotnet build "./Web.Server.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web.Server.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Server.dll"]