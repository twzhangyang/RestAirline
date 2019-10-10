#!/bin/bash -e

[ -z $GO_PIPELINE_NAME ] && echo "Not in gocd env" && exit 0
type docker && docker pull gosu/alpine || exit 1

docker run --rm -v $(pwd):/app gosu/alpine gosu root chown -R 1000:1000 /app
