[![Build Status](https://dev.azure.com/restairline/restairline/_apis/build/status/restairline?branchName=master)](https://dev.azure.com/restairline/restairline/_build/latest?definitionId=4&branchName=master)

# Overview

* A example of DDD+CQRS+EventSourcing+Hypermedia api+ASP.NET Core 2.2
* Based on [EventFlow](https://github.com/eventflow/EventFlow)

# How to Run
## Clone this repo
```
git clone https://github.com/twzhangyang/RestAirline.git
```

## Building the image for the first time
If you want to modify the files in the image, then you'll have to build locally.
go to `docker-compose` folder:

Build with `docker-compose`:
```
docker-compose build
```

## Running the container
Modify the env variables to your liking in the `docker-compose.yml`.

Then spin up a new container using `docker-compose`
```
docker-compose up
```
Note: add a `-d` to run the container in background

## Connecting to the container
To connect to the SQL Server in the container, you can docker exec with sqlcmd.
```
docker exec -it restairline_mssql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -Q "select 1"
```

## Run in local
This project based on .NET Core SDK 2.2.103, please install corresponding SDK for your operating system:

[Window](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.103-windows-x64-installer)
[Mac](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.103-macos-x64-installer)

After installing, please run below command to make sure current .NET Core version is 2.2.103
`dotnet --version`

## Run k8s on AKS
Please follow up steps under `k8s/steps`:
* Create Azure Kubernetes Service (AKS) and Azure Container Registry(ACR) - `1.aks.txt`
* Once you have ACR you can push docker image into your private docker image repository - `2.image.txt`
* Deploy k8s by yaml file - `3.deployk8s.txt`
* k8s scale up/down, roll back, rolling update - `4.scale.txt`
* Clear up - `5.clearup.txt`

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

## Migrate Entity framework read model
If you want to change Entity framework read model, please follow EF migration steps:
* Update data structures in ReadModelDbContext
* Go to `RestAirline/src/RestAirline.ReadModel.EntityFramework` folder
* `dotnet ef migrations add "migration names"`
* `dotnet ef database update`

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


