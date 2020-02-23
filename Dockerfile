#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Hastnama.Ekipchi.Api/Hastnama.Ekipchi.Api.csproj", "Hastnama.Ekipchi.Api/"]
COPY ["Hastnama.Ekipchi.Common/Hastnama.Ekipchi.Common.csproj", "Hastnama.Ekipchi.Common/"]
COPY ["Hastnama.Ekipchi.Business/Hastnama.Ekipchi.Business.csproj", "Hastnama.Ekipchi.Business/"]
COPY ["Hastnama.Ekipchi.DataAccess/Hastnama.Ekipchi.DataAccess.csproj", "Hastnama.Ekipchi.DataAccess/"]
COPY ["Hastnama.Ekipchi.Data/Hastnama.Ekipchi.Data.csproj", "Hastnama.Ekipchi.Data/"]
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