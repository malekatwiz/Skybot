FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Skybot.Api/Skybot.Api.csproj", "Skybot.Api/"]
RUN dotnet restore "Skybot.Api/Skybot.Api.csproj"
COPY . .
WORKDIR "/src/Skybot.Api"
RUN dotnet build "Skybot.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Skybot.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Skybot.Api.dll"]
