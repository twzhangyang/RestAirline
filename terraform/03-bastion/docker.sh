#!/bin/bash

echo "start installing docker..."
sudo apt-get update
sudo apt install docker.io -y
sudo systemctl start docker
sudo systemctl enable docker
sudo chmod 666 /var/run/docker.sock

echo "docker was installed"

# install awscli
sudo apt install awscli -y

