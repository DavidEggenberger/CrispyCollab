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
COPY ["Source/Modules/BaseInfrastructure/BaseInfrastructure.csproj", "Source/Modules/BaseInfrastructure/"]
COPY ["Source/SharedKernel/SharedKernel.csproj", "Source/SharedKernel/"]
COPY ["Source/Modules/ChannelModule/Web/Controllers/ChannelModule.Server.csproj", "Source/Modules/ChannelModule/Web/Controllers/"]
COPY ["Source/Modules/ChannelModule/Layers/Application/ChannelModule.Application.csproj", "Source/Modules/ChannelModule/Layers/Application/"]
COPY ["Source/Modules/ChannelModule/Layers/Domain/ChannelModule.Domain.csproj", "Source/Modules/ChannelModule/Layers/Domain/"]
COPY ["Source/Modules/ChannelModule/Layers/Infrastructure/ChannelModule.Infrastructure.csproj", "Source/Modules/ChannelModule/Layers/Infrastructure/"]
COPY ["Source/Modules/ChannelModule/Web/DTOs/ChannelModule.DTOs.csproj", "Source/Modules/ChannelModule/Web/DTOs/"]
COPY ["Source/Web/WebWasmClient/WebWasmClient.csproj", "Source/Web/WebWasmClient/"]
COPY ["Source/Modules/ChannelModule/Web/ChannelContextComponents/ChannelModule.Client.csproj", "Source/Modules/ChannelModule/Web/ChannelContextComponents/"]
COPY ["Source/Modules/IdentityModule/Web/IdentityClientModule/IdentityModule.Client.csproj", "Source/Modules/IdentityModule/Web/IdentityClientModule/"]
COPY ["Source/Modules/IdentityModule/Layers/Domain/IdentityModule.Domain.csproj", "Source/Modules/IdentityModule/Layers/Domain/"]
COPY ["Source/Modules/IdentityModule/Web/DTOs/IdentityModule.DTOs.csproj", "Source/Modules/IdentityModule/Web/DTOs/"]
COPY ["Source/Modules/SubscriptionModule/Web/SubscriptionClientModule/SubscriptionModule.Client.csproj", "Source/Modules/SubscriptionModule/Web/SubscriptionClientModule/"]
COPY ["Source/Modules/SubscriptionModule/Web/DTOs/SubscriptionModule.DTOs.csproj", "Source/Modules/SubscriptionModule/Web/DTOs/"]
COPY ["Source/Modules/TenantModule/Web/TenantClientModule/TenantModule.Client.csproj", "Source/Modules/TenantModule/Web/TenantClientModule/"]
COPY ["Source/Modules/TenantModule/Web/DTOs/TenantModule.DTOs.csproj", "Source/Modules/TenantModule/Web/DTOs/"]
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