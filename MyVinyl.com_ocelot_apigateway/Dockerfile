FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR "/src/MyVinyl.com_ocelot_apigateway"
COPY ["MyVinyl.com_ocelot_apigateway.csproj", "MyVinyl.com_ocelot_apigateway/"]
RUN dotnet restore "MyVinyl.com_ocelot_apigateway/MyVinyl.com_ocelot_apigateway.csproj"
COPY . .
WORKDIR "/src/MyVinyl.com_ocelot_apigateway"
RUN dotnet build "MyVinyl.com_ocelot_apigateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyVinyl.com_ocelot_apigateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
# COPY --from= publish / app / publish.
COPY --from= build-env /App.
ENTRYPOINT ["dotnet", "MyVinyl.com_ocelot_apigateway.dll"]
