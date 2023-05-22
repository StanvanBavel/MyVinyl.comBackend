FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5194

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyVinyl.com/MyVinyl.com.csproj", "MyVinyl.com/"]
RUN dotnet restore "MyVinyl.com/MyVinyl.com.csproj"
COPY . .
WORKDIR "/src/MyVinyl.com"
RUN dotnet build "MyVinyl.com.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyVinyl.com.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5194
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyVinyl.com.dll"]

