# Build,Publish And Run My App
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Hastnama.Ekipchi.Api/Hastnama.Ekipchi.Api.csproj", "Hastnama.Ekipchi.Api/"]
RUN dotnet restore "Hastnama.Ekipchi.Api/Hastnama.Ekipchi.Api.csproj"
COPY . .
WORKDIR "/src/Hastnama.Ekipchi.Api"
RUN dotnet build "Hastnama.Ekipchi.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hastnama.Ekipchi.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hastnama.Ekipchi.Api.dll"]