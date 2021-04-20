#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/ABB.3Tier.WebApi/ABB.NTier.WebApi.csproj", "src/ABB.3Tier.WebApi/"]
RUN dotnet restore "src/ABB.3Tier.WebApi/ABB.NTier.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ABB.3Tier.WebApi"
RUN dotnet build "ABB.NTier.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ABB.NTier.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ABB.NTier.WebApi.dll"]
