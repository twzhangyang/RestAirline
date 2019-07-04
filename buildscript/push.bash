#!/bin/bash
set -ex

VERSION=${BUILD_BUILDID:-'dev'}
REPOSITORY=restairline.azurecr.io/restairline

# docker login
az acr login --name restairline

docker push $REPOSITORY:$VERSION
docker push $REPOSITORY:latest

echo "Cleaning docker images..."
docker image ls | grep restairline | awk '{print $1":"$2}' | xargs docker rmi
docker system prune -f