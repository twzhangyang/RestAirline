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


## Run the API
Try to input home api link in Postman:
```
http://localhost:5000/api/home/
```