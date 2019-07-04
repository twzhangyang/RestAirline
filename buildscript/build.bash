#!/bin/bash
set -ex


VERSION=${Build.BuildId:-'dev'}
LABEL=${Build.BuildId:-'1'}

REPOSITORY=restairline.azurecr.io/restairline

docker build --build-arg Version=LABEL -f ./src/RestAirline.Api/Dockerfile ./src --tag $REPOSITORY:$VERSION --tag $REPOSITORY:latest