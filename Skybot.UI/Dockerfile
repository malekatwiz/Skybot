FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Skybot.UI/Skybot.UI.csproj", "Skybot.UI/"]
RUN dotnet restore "Skybot.UI/Skybot.UI.csproj"
COPY . .
WORKDIR /src/Skybot.UI
RUN dotnet build "Skybot.UI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Skybot.UI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Skybot.UI.dll"]
