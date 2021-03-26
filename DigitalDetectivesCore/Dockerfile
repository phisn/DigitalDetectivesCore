#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-arm32v7 AS base

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq \
    && npm install -g @ionic/cli
	
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-backend

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq \
    && npm install -g @ionic/cli

WORKDIR /src
COPY ["Server/Server.csproj", "Server/"]
RUN dotnet restore "Server/Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "Server.csproj" -c Release -o /app/build

FROM build-backend AS publish-backend
RUN dotnet publish "Server.csproj" -c Release -o /app/publish

FROM build-backend

FROM base AS final
WORKDIR /app
COPY --from=publish-backend /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]