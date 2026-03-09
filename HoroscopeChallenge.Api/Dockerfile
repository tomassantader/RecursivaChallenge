
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY HoroscopeChallenge.Api/*.csproj HoroscopeChallenge.Api/

RUN dotnet restore HoroscopeChallenge.Api/HoroscopeChallenge.Api.csproj

COPY . .

WORKDIR /src/HoroscopeChallenge.Api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "HoroscopeChallenge.Api.dll"]