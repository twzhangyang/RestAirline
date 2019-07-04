#!/bin/bash
set -ex

VERSION=${BUILD_BUILDID:-'dev'}
REPOSITORY=restairline.azurecr.io/restairline
RESOUCEGROUP='restairline-group'
APPNAME='restairline-test'
PASSWORD='FXnbyQuuuF+MI3J4o9Z30TDhBAOk3feI'

az webapp config container set --name $APPNAME --resource-group $RESOUCEGROUP --docker-custom-image-name $REPOSITORY:$VERSION --docker-registry-server-url https://restairline.azurecr.io --docker-registry-server-user restairline --docker-registry-server-password $PASSWORD

az webapp config appsettings set --resource-group $RESOUCEGROUP --name $APPNAME