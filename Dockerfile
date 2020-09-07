FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY Eventador.API/Eventador.API.csproj Eventador.API/
RUN dotnet restore Eventador.API/Eventador.API.csproj
COPY . .
WORKDIR /src/Eventador.API
RUN dotnet build Eventador.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Eventador.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Eventador.API.dll"]
