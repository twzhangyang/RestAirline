#!/bin/bash

echo "start installing docker..."
sudo apt-get update
sudo apt install docker.io -y
sudo systemctl start docker
sudo systemctl enable docker
sudo chmod 666 /var/run/docker.sock

echo "docker was installed"


echo "install elasticsearch6.1.4"

# install elasticSearch6.14
docker run -d --name elasticsearch -p 9200:9200 -p 9300:9300 -e "cluster.name=elasticsearch" -e "discovery.type=single-node" -e "bootstrap.memory_lock=true" -e "ES_JAVA_OPTS=-Xms256m -Xmx256m" docker.elastic.co/elasticsearch/elasticsearch:6.1.4

echo "elasticsearch was installed"
