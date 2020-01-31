FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
ARG Version

COPY . .

WORKDIR Booking/RestAirline.FlightAvailability.Api
RUN dotnet restore

RUN dotnet publish /p:Version=$Version -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "RestAirline.FlightAvailability.Api.dll"]
