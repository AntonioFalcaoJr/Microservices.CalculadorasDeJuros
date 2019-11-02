FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Microservices.CalculadorasDeJuros/Microservices.CalculadorasDeJuros.csproj", "Microservices.CalculadorasDeJuros/"]
RUN dotnet restore "Microservices.CalculadorasDeJuros/Microservices.CalculadorasDeJuros.csproj"
COPY . .
WORKDIR "/src/Microservices.CalculadorasDeJuros"
RUN dotnet build "Microservices.CalculadorasDeJuros.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.CalculadorasDeJuros.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.CalculadorasDeJuros.dll"]