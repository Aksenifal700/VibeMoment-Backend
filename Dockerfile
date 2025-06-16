FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["VibeMoment.Infrastructure/VibeMoment.Infrastructure.csproj", "VibeMoment.Infrastructure/"]

RUN dotnet restore "VibeMoment.Api/VibeMoment.Api.csproj"
COPY . .
WORKDIR "/src/VibeMoment.Api"
RUN dotnet publish "VibeMoment.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "VibeMoment.Api.dll"]