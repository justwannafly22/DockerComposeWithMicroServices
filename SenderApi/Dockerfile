FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SenderApi/SenderApi.csproj", "SenderApi/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "./SenderApi/./SenderApi.csproj"
COPY . .
WORKDIR "/src/SenderApi"
RUN dotnet build "./SenderApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SenderApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SenderApi.dll"]