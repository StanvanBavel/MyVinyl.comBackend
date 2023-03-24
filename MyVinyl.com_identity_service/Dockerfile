FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 44002
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyVinyl.com_identity_service/MyVinyl.com_identity_service.csproj", "MyVinyl.com_identity_service/"]
RUN dotnet restore "MyVinyl.com_identity_service/MyVinyl.com_identity_service.csproj"
COPY . .
WORKDIR "/src/MyVinyl.com_identity_service"
RUN dotnet build "MyVinyl.com_identity_service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyVinyl.com_identity_service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyVinyl.com_identity_service.dll"]
