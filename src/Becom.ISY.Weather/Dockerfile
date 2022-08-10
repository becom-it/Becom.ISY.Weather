#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY *.sln .
COPY ["src/Becom.ISY.Weather/Becom.ISY.Weather.csproj", "src/Becom.ISY.Weather/"]
COPY ["src/Becom.ISY.Weather.Contracts/Becom.ISY.Weather.Contracts.csproj", "src/Becom.ISY.Weather.Contracts/"]

#RUN dotnet restore "Becom.ISY.Weather/Becom.ISY.Weather.csproj"
RUN dotnet restore

COPY /src/Becom.ISY.Weather/. src/Becom.ISY.Weather/.
COPY /src/Becom.ISY.Weather.Contracts/. src/Becom.ISY.Weather.Contracts/.

WORKDIR "/src/Becom.ISY.Weather"
RUN dotnet build "Becom.ISY.Weather.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Becom.ISY.Weather.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Becom.ISY.Weather.dll"]