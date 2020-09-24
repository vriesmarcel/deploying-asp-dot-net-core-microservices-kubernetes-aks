#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GloboTicket.Gateway.WebBff/GloboTicket.Gateway.WebBff.csproj", "GloboTicket.Gateway.WebBff/"]
COPY ["GloboTicket.Gateway.Shared/GloboTicket.Gateway.Shared.csproj", "GloboTicket.Gateway.Shared/"]
RUN dotnet restore "GloboTicket.Gateway.WebBff/GloboTicket.Gateway.WebBff.csproj"
COPY . .
WORKDIR "/src/GloboTicket.Gateway.WebBff"
RUN dotnet build "GloboTicket.Gateway.WebBff.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GloboTicket.Gateway.WebBff.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GloboTicket.Gateway.WebBff.dll"]