FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5178

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyVinyl.com_authentication_service/MyVinyl.com_authentication_service.csproj", "MyVinyl.com_authentication_service/"]
RUN dotnet restore "MyVinyl.com_authentication_service/MyVinyl.com_authentication_service.csproj"
COPY . .
WORKDIR "/src/MyVinyl.com_authentication_service"
RUN dotnet build "MyVinyl.com_authentication_service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyVinyl.com_authentication_service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5178
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyVinyl.com_authentication_service.dll"]
