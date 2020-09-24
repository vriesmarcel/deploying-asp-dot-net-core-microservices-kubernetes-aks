#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GloboTicket.Web.Bff/GloboTicket.Web.Bff.csproj", "GloboTicket.Web.Bff/"]
RUN dotnet restore "GloboTicket.Web.Bff/GloboTicket.Web.Bff.csproj"
COPY . .
WORKDIR "/src/GloboTicket.Web.Bff"
RUN dotnet build "GloboTicket.Web.Bff.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GloboTicket.Web.Bff.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GloboTicket.Web.Bff.dll"]