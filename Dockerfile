FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["FGC.Notifications.Api/FGC.Notifications.Api.csproj", "FGC.Notifications.Api/"]
COPY ["FGC.Notifications.Application/FGC.Notifications.Application.csproj", "FGC.Notifications.Application/"]
COPY ["FGC.Notifications.Infrastructure/FGC.Notifications.Infrastructure.csproj", "FGC.Notifications.Infrastructure/"]
RUN dotnet restore "FGC.Notifications.Api/FGC.Notifications.Api.csproj"

COPY . .
WORKDIR "/src/FGC.Notifications.Api"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FGC.Notifications.Api.dll"]
