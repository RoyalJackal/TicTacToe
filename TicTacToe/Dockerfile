FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TicTacToe/TicTacToe.csproj", "TicTacToe/"]
COPY ["Data/Data.csproj", "Data/"]
RUN dotnet restore "TicTacToe/TicTacToe.csproj"
COPY . .
WORKDIR "/src/TicTacToe"
RUN dotnet build "TicTacToe.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicTacToe.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.dll"]
