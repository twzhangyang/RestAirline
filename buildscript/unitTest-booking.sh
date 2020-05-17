#!/usr/bin/env bash
set -ex
cd $(dirname $0)/..
TEST_FOLDER="./tests/Booking"

for PROJECT in $(ls -1 $TEST_FOLDER |\
 grep -e RestAirline |\
 grep -v TestsHelper |\
 grep -v Elasticsearch |\
 grep -v MassTransit)
do
  echo $PROJECT
  docker run --rm -v $(pwd):/project -w /project mcr.microsoft.com/dotnet/core/sdk:3.1 dotnet test $TEST_FOLDER/$PROJECT -v minimal
done