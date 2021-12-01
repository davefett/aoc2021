FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["day01.fsproj", "day01/"]
RUN dotnet restore "day01/day01.fsproj"
COPY . "day01/."
WORKDIR "/src/day01"
RUN dotnet build "day01.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "day01.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY "input1.txt" .
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "day01.dll"]
