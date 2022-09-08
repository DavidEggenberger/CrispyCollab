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
COPY ["Source/Layers/Domain/Domain.csproj", "Source/Layers/Domain/"]
COPY ["Source/Common/Common.csproj", "Source/Common/"]
COPY ["Source/Web/WebWasmClient/WebWasmClient.csproj", "Source/Web/WebWasmClient/"]
COPY ["Source/Web/DTOs/WebShared.csproj", "Source/Web/DTOs/"]
COPY ["Source/Layers/Application/Application.csproj", "Source/Layers/Application/"]
COPY ["Source/Layers/Infrastructure/Infrastructure.csproj", "Source/Layers/Infrastructure/"]
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
