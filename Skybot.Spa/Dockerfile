FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
LABEL author="Malek Atwiz" email="hello@malekatwiz.net"

RUN apt-get update && \
	apt-get install -y wget && \
	apt-get install -y gnupg2 && \
	wget -qO- https://deb.nodesource.com/setup_11.x | bash - && \
    apt-get install -y build-essential nodejs
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Skybot.Spa/Skybot.Spa.csproj", "Skybot.Spa/"]
RUN dotnet restore "Skybot.Spa/Skybot.Spa.csproj"
COPY . .
WORKDIR /src/Skybot.Spa
RUN dotnet build "Skybot.Spa.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Skybot.Spa.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Skybot.Spa.dll"]
