
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Hastnama.Ekipchi.Api/Hastnama.Ekipchi.Api.csproj /app/Hastnama.Ekipchi.Api/
COPY Hastnama.Ekipchi.DataAccess/Hastnama.Ekipchi.DataAccess.csproj /app/Hastnama.Ekipchi.DataAccess/
COPY Hastnama.Ekipchi.Data/Hastnama.Ekipchi.Data.csproj /app/Hastnama.Ekipchi.Data/
COPY Hastnama.Ekipchi.Business/Hastnama.Ekipchi.Business.csproj /app/Hastnama.Ekipchi.Business/
COPY Hastnama.Ekipchi.Common/Hastnama.Ekipchi.Common.csproj /app/Hastnama.Ekipchi.Common/
RUN dotnet restore

# copy everything else and build app
COPY . ./
WORKDIR /app/Hastnama.Ekipchi.Api
RUN dotnet publish  -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS runtime
WORKDIR /app
COPY --from=build /app/Hastnama.Ekipchi.Api/out ./
ENTRYPOINT ["dotnet", "Hastnama.Ekipchi.Api.dll"]
