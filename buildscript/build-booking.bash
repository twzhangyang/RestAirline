#!/bin/bash
cd $(dirname "$0")/..

echo $PWD

VERSION=${BUILD_BUILDID:-'dev'}
LABEL=${BUILD_BUILDID:-'1'}

REPOSITORY=restairline.azurecr.io/restairline-booking

docker build --build-arg Version=$LABEL -f ./src/Booking/RestAirline.Booking.Api/Dockerfile ./src --tag $REPOSITORY:$VERSION

echo "Cleaning docker images..."
docker image ls | grep $REPOSITORY | awk '{print $1":"$2}' | xargs docker rmi
# docker system prune -f


