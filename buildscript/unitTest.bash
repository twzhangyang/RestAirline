#!/usr/bin/env bash
set -ex

for PROJECT in $(ls -1 ./tests | grep -e RestAirline | grep -v TestsHelper)
do
  docker run --rm microsoft/dotnet:2.2.103-sdk dotnet test tests/$PROJECT -v minimal
done