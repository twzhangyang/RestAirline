#!/bin/bash
set -ex

# BASEDIR=$(dirname "$0")

VERSION=${Build_Id:-'DEV'}
REPOSITORY='restairline.azurecr.io'


docker build -f ./src/RestAirline.Api/Dockerfile ./src --tag $REPOSITORY:$VERSION --tag $REPOSITORY:latest 
