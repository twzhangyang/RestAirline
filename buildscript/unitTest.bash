#!/usr/bin/env bash
set -ex

# PROJECT_BASE_DIR=$(pwd)/$(dirname $0)/..

for PROJECT in $(ls -1 ./tests | grep -e RestAiline | grep -v TestsHelper)
do
  docker run --rm microsoft/dotnet:2.2.103-sdk dotnet test tests/$PROJECT -v minimal
done
