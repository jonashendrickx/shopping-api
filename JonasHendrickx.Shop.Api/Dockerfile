#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["JonasHendrickx.Shop.Api/JonasHendrickx.Shop.Api.csproj", "JonasHendrickx.Shop.Api/"]
RUN dotnet restore "JonasHendrickx.Shop.Api/JonasHendrickx.Shop.Api.csproj"
COPY . .
WORKDIR "/src/JonasHendrickx.Shop.Api"
RUN dotnet build "JonasHendrickx.Shop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JonasHendrickx.Shop.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JonasHendrickx.Shop.Api.dll"]