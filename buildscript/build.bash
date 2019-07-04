#!/bin/bash
set -ex

VERSION=${BUILD_BUILDID:-'dev'}
LABEL=${BUILD_BUILDID:-'1'}

REPOSITORY=restairline.azurecr.io/restairline

docker build --build-arg Version=$LABEL.0.0 -f ./src/RestAirline.Api/Dockerfile ./src --tag $REPOSITORY:$VERSION --tag $REPOSITORY:latest