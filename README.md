[![Build Status](https://dev.azure.com/restairline/restairline/_apis/build/status/restairline?branchName=master)](https://dev.azure.com/restairline/restairline/_build/latest?definitionId=4&branchName=master)

# Overview

* A classic DDD with CQRS-ES, Hypermedia API project based on EventFlow. It's targeted to ASP.NET Core 2.2 and can be deployed to docker and k8s.
* Based on [EventFlow](https://github.com/eventflow/EventFlow)
* [Wiki](https://github.com/twzhangyang/RestAirline/wiki) is in progress

# How to Run
## Clone this repo
```
git clone https://github.com/twzhangyang/RestAirline.git
```

## Running the container
Then spin up a new container using `docker-compose`
```
docker-compose up
```
Note: add a `-d` to run the container in background

An API service and mssql will run in docker

## Run in local
This project based on .NET Core SDK 2.2.103, please install corresponding SDK for your operating system:

[Window](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.103-windows-x64-installer)

[Mac](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.103-macos-x64-installer)

After installing, please run below command to make sure current .NET Core version is 2.2.103
`dotnet --version`

The EventStore and ReadModel are based on EF Core and connect to MSSQL, before run this service please update EventStore connect string and
ReadModel connect string in `settings.json` under `RestAirline.Api` project.
After set connect string two database will be migrate automatically by the API service.

## Run the API
1. Try to input home api link in Postman:
```
GET http://localhost:61200/api/home/
```
---
2. Select Journey
```
POST api/booking/journeys
```
![add journey](https://user-images.githubusercontent.com/22952792/59654415-a046ec00-91c8-11e9-9147-32fe157339e3.png)
---
3. Add passenger
```
POST api/booking-{id}/passenger
```
![add booking](https://user-images.githubusercontent.com/22952792/59654417-a2a94600-91c8-11e9-8d98-dc9b7b4b4607.png)
---
4. Get booking
```
GET api/booking/booking-{id}
```
![get booking](https://user-images.githubusercontent.com/22952792/59654419-a63ccd00-91c8-11e9-90b8-b307b30a7e94.png)


## Business 
The example is regarding online booking for an airline company. An airline company named 'RestAirline' is offering online booking. 
* After passenger submited one of the available journey that means this passenger starting create an online booking.
* passenger can submit multiple available journeys, every journey including a flight.
* After passenger added journeys, he/she can add passengers.
* Once passenger have been added in booking, passenger can update passenger name for each passenger.
* passenger can submit available seats for each flight and each passenger, seat may just including seat number.
* Once passegner submitted seats, passenger still can update seat.
* After all of these steps, passenger have a chance to order insurance for all passenger some of them.
* Last step is pay for all booking, if payment is successful then create a pnr(six digit) for this booking.
* Online checkin is allowed for all the flights. Passenger can checkin at below time window:

    ```2h <= timewindow <= departure time - 30m``` 
* Passeger can do online checkin, after this step passegner start to his/her journey. 

## Possible Domain
There are four possbile Domains for above business:
But let's focus on `Booking` for now and mock other two domains even if you can hardcode data from these two domains.

![domain](https://user-images.githubusercontent.com/22952792/59654892-bbb2f680-91ca-11e9-8465-a628a57e13b2.png)


