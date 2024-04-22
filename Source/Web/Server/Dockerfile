#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Source/Web/Web.Server/Web.Server.csproj", "Source/Web/Web.Server/"]
COPY ["Source/Modules/ChannelModule/Web/Server/ChannelModule.Server.csproj", "Source/Modules/ChannelModule/Web/Server/"]
COPY ["Source/Shared/SharedKernel/SharedKernel.csproj", "Source/Shared/SharedKernel/"]
COPY ["Source/Modules/ChannelModule/Layers/Application/ChannelModule.Application.csproj", "Source/Modules/ChannelModule/Layers/Application/"]
COPY ["Source/Shared/Modules/Layers/Application/ModulesShared.Application.csproj", "Source/Shared/Modules/Layers/Application/"]
COPY ["Source/Shared/Modules/Layers/Features/ModulesShared.Features.csproj", "Source/Shared/Modules/Layers/Features/"]
COPY ["Source/Shared/Modules/Layers/Domain/ModulesShared.Domain.csproj", "Source/Shared/Modules/Layers/Domain/"]
COPY ["Source/Modules/ChannelModule/Layers/Features/ChannelModule.Features.csproj", "Source/Modules/ChannelModule/Layers/Features/"]
COPY ["Source/Modules/ChannelModule/Layers/Domain/ChannelModule.Domain.csproj", "Source/Modules/ChannelModule/Layers/Domain/"]
COPY ["Source/Modules/ChannelModule/Web/DTOs/ChannelModule.DTOs.csproj", "Source/Modules/ChannelModule/Web/DTOs/"]
COPY ["Source/Modules/SubscriptionModule/Web/Server/Modules.Subscriptions.csproj", "Source/Modules/SubscriptionModule/Web/Server/"]
COPY ["Source/Modules/SubscriptionModule/Layers/Application/SubscriptionModule.Application.csproj", "Source/Modules/SubscriptionModule/Layers/Application/"]
COPY ["Source/Modules/SubscriptionModule/Layers/Features/SubscriptionModule.Features.csproj", "Source/Modules/SubscriptionModule/Layers/Features/"]
COPY ["Source/Modules/SubscriptionModule/Layers/Domain/SubscriptionModule.Domain.csproj", "Source/Modules/SubscriptionModule/Layers/Domain/"]
COPY ["Source/Modules/TenantIdentity/Web/Server/TenantIdentity.Server.csproj", "Source/Modules/TenantIdentity/Web/Server/"]
COPY ["Source/Modules/TenantIdentity/Layers/Application/TenantIdentity.Application.csproj", "Source/Modules/TenantIdentity/Layers/Application/"]
COPY ["Source/Modules/TenantIdentity/Layers/Features/TenantIdentity.Features.csproj", "Source/Modules/TenantIdentity/Layers/Features/"]
COPY ["Source/Modules/TenantIdentity/Layers/Domain/TenantIdentity.Domain.csproj", "Source/Modules/TenantIdentity/Layers/Domain/"]
COPY ["Source/Modules/TenantIdentity/Web/DTOs/TenantIdentity.DTOs.csproj", "Source/Modules/TenantIdentity/Web/DTOs/"]
COPY ["Source/Shared/Modules/Web/Server/ModulesShared.Server.csproj", "Source/Shared/Modules/Web/Server/"]
COPY ["Source/Web/Web.Client/Web.Client.csproj", "Source/Web/Web.Client/"]
COPY ["Source/Modules/ChannelModule/Web/Web.Client/ChannelModule.Web.Client.csproj", "Source/Modules/ChannelModule/Web/Web.Client/"]
COPY ["Source/Shared/Modules/Web/Web.Client/ModulesShared.Web.Client.csproj", "Source/Shared/Modules/Web/Web.Client/"]
COPY ["Source/Modules/SubscriptionModule/Web/Web.Client/SubscriptionModule.Web.Client.csproj", "Source/Modules/SubscriptionModule/Web/Web.Client/"]
COPY ["Source/Modules/SubscriptionModule/Web/DTOs/SubscriptionModule.DTOs.csproj", "Source/Modules/SubscriptionModule/Web/DTOs/"]
COPY ["Source/Modules/TenantIdentity/Web/Web.Client/TenantIdentity.Web.Client.csproj", "Source/Modules/TenantIdentity/Web/Web.Client/"]
RUN dotnet restore "Source/Web/Web.Server/Web.Server.csproj"
COPY . .
WORKDIR "/src/Source/Web/Web.Server"
RUN dotnet build "Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Server.dll"]