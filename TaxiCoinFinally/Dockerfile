FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY TaxiCoinFinally/TaxiCoinFinally.csproj TaxiCoinFinally/
COPY TokenAPI/TokenAPI.csproj TokenAPI/
RUN dotnet restore TaxiCoinFinally/TaxiCoinFinally.csproj
COPY . .
WORKDIR /src/TaxiCoinFinally
RUN dotnet build TaxiCoinFinally.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TaxiCoinFinally.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TaxiCoinFinally.dll"]
