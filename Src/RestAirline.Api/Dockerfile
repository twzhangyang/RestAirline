FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
ARG Version

COPY . .
WORKDIR /src/RestAirline.Api

RUN dotnet restore
RUN dotnet build /p:Version=$Version -c Release -o /app

FROM build AS publish
ARG Version
RUN dotnet publish /p:Version=$Version -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RestAirline.Api.dll"]
