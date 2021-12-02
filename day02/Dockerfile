FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["day02.fsproj", "day02/"]
RUN dotnet restore "day02/day02.fsproj"
COPY . "day02/."
WORKDIR "/src/day02"
RUN dotnet build "day02.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "day02.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY "input02.txt" .
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "day02.dll"]
