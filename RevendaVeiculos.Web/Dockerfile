#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RevendaVeiculos.Web/RevendaVeiculos.Web.csproj", "RevendaVeiculos.Web/"]
COPY ["RevendaVeiculos.Service/RevendaVeiculos.Service.csproj", "RevendaVeiculos.Service/"]
COPY ["RevendaVeiculos.Data/RevendaVeiculos.Data.csproj", "RevendaVeiculos.Data/"]
COPY ["RevendaVeiculos.Message/RevendaVeiculos.Message.csproj", "RevendaVeiculos.Message/"]
RUN dotnet restore "RevendaVeiculos.Web/RevendaVeiculos.Web.csproj"
COPY . .
WORKDIR "/src/RevendaVeiculos.Web"
RUN dotnet build "RevendaVeiculos.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RevendaVeiculos.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RevendaVeiculos.Web.dll"]
