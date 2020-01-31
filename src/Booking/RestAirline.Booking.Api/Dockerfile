FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
ARG Version

COPY . .

WORKDIR Booking/RestAirline.Booking.Domain
RUN dotnet restore

RUN dotnet publish /p:Version=$Version --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "RestAirline.Booking.Api.dll"]
