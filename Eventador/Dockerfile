FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["Eventador/Eventador.csproj", "Eventador.API/"]
RUN dotnet restore "Eventador/Eventador.csproj"
COPY . .
WORKDIR "/src/Eventador.API"
RUN dotnet build "Eventador.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Eventador.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Eventador.API.dll"]