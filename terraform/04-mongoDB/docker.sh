#!/bin/bash

echo "start installing docker..."
sudo apt-get update
sudo apt install docker.io -y
sudo systemctl start docker
sudo systemctl enable docker
sudo chmod 666 /var/run/docker.sock

echo "docker was installed"

# install mongoDB
docker run --name mongodb -d -p 27017:27017 -v $PWD/mongo:/data/db mongo:4.0
