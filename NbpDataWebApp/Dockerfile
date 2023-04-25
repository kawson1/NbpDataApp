FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NbpDataWebApp/NbpDataWebApp.csproj", "NbpDataWebApp/"]
RUN dotnet restore "NbpDataWebApp/NbpDataWebApp.csproj"
COPY . .
WORKDIR "/src/NbpDataWebApp"
RUN dotnet build "NbpDataWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NbpDataWebApp.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get update && apt-get install -y curl
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NbpDataWebApp.dll"]
