#!/bin/bash
set -ex

BASEDIR=$(dirname "$0")
VERSION=${Build_Id:-'DEV'}
REPOSITORY='restairline.azurecr.io'


docker build -f $BASEDIR/../src/RestAirline.Api/Dockerfile $BASEDIR/../src --tag $REPOSITORY:$VERSION --tag $REPOSITORY:latest 
