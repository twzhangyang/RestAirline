#!/usr/bin/env bash
set -ex
cd $(dirname $0)/..
FOLDER="./tests/Booking"

for PROJECT in $(ls -1 $FOLDER | grep -e RestAirline | grep -v TestsHelper | grep -v Elasticsearch | grep -v MassTransit)
do
  echo $PROJECT
  # docker run --rm -v $FOLDER:/project -w /project mcr.microsoft.com/dotnet/core/sdk:3.1 dotnet test $FOLDER/$PROJECT -v minimal
done