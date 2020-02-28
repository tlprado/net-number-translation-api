FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/NumberTranslation.Api/NumberTranslation.Api.csproj", "src/NumberTranslation.Api/"]
COPY ["src/NumberTranslation.Domain/NumberTranslation.Domain.csproj", "src/NumberTranslation.Domain/"]
COPY ["src/NumberTranslation.Utils/NumberTranslation.Utils.csproj", "src/NumberTranslation.Utils/"]
RUN dotnet restore "src/NumberTranslation.Api/NumberTranslation.Api.csproj"
COPY . .
WORKDIR "/src/src/NumberTranslation.Api"
RUN dotnet build "NumberTranslation.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NumberTranslation.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NumberTranslation.Api.dll"]