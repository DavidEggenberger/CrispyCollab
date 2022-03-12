#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Source/Web/WebServer/WebServer.csproj", "Source/Web/WebServer/"]
COPY ["Source/Infrastructure/Infrastructure.csproj", "Source/Infrastructure/"]
COPY ["Source/Domain/Domain.csproj", "Source/Domain/"]
COPY ["Source/Application/Application.csproj", "Source/Application/"]
COPY ["Source/Web/WebWasmClient/WebWasmClient.csproj", "Source/Web/WebWasmClient/"]
COPY ["Source/Web/DTOs/Common.csproj", "Source/Web/DTOs/"]
RUN dotnet restore "Source/Web/WebServer/WebServer.csproj"
COPY . .
WORKDIR "/src/Source/Web/WebServer"
RUN dotnet build "WebServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebServer.dll"]