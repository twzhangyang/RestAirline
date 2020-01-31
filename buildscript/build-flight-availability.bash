#!/bin/bash
cd $(dirname "$0")/..

echo $PWD

VERSION=${BUILD_BUILDID:-'dev'}
LABEL=${BUILD_BUILDID:-'1'}

REPOSITORY=restairline.azurecr.io/restairline-flightavailability

docker build --build-arg Version=$LABEL -f ./src/FlightAvailability/RestAirline.FlightAvailability.Api/Dockerfile ./src --tag $REPOSITORY:$VERSION


echo "Cleaning docker images..."
# docker image ls | grep $REPOSITORY | awk '{print $1":"$2}' | xargs docker rmi
docker system prune 