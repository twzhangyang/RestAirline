FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
ARG Version

COPY . .
WORKDIR Booking/RestAirline.Booking.Api

#RUN pwd
#RUN ls

RUN dotnet restore

FROM build AS publish
ARG Version
RUN dotnet publish /p:Version=$Version -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RestAirline.Booking.Api.dll"]
