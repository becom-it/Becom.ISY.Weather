#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Becom.ISY.Weather/Becom.ISY.Weather.csproj", "Becom.ISY.Weather/"]
RUN dotnet restore "Becom.ISY.Weather/Becom.ISY.Weather.csproj"
COPY . .
WORKDIR "/src/Becom.ISY.Weather"
RUN dotnet build "Becom.ISY.Weather.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Becom.ISY.Weather.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Becom.ISY.Weather.dll"]