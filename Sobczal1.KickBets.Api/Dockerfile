FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sobczal1.KickBets.Api/Sobczal1.KickBets.Api.csproj", "Sobczal1.KickBets.Api/"]
COPY ["Sobczal1.KickBets.Application/Sobczal1.KickBets.Application.csproj", "Sobczal1.KickBets.Application/"]
COPY ["Sobczal1.KickBets.Domain/Sobczal1.KickBets.Domain.csproj", "Sobczal1.KickBets.Domain/"]
COPY ["Sobczal1.KickBets.Persistence/Sobczal1.KickBets.Persistence.csproj", "Sobczal1.KickBets.Persistence/"]
RUN dotnet restore "Sobczal1.KickBets.Api/Sobczal1.KickBets.Api.csproj"
COPY . .
WORKDIR "/src/Sobczal1.KickBets.Api"
RUN dotnet build "Sobczal1.KickBets.Api.csproj" -c Release -o /app/build
RUN dotnet tool install --global dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools

FROM build AS publish
RUN dotnet publish "Sobczal1.KickBets.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sobczal1.KickBets.Api.dll"]
