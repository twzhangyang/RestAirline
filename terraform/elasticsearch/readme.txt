terraform init
terraform apply -var-file=dev.tfvars
ssh -i ~/.ssh/id_rsa.pub ubuntu@18.163.49.2


# install elasticSearch6.14
docker run -d --name elasticsearch -p 9200:9200 -p 9300:9300 -e "cluster.name=elasticsearch" -e "discovery.type=single-node" -e "bootstrap.memory_lock=true" -e "ES_JAVA_OPTS=-Xms256m -Xmx256m" docker.elastic.co/elasticsearch/elasticsearch:6.1.4

# install in local(not work, don't know why)
docker run -d --name kibana -p 5601:5601 -e "server.name=kibana" -e "server.host=restairline-es" -e "elasticsearch.url=http://18.163.49.2:9200" docker.elastic.co/kibana/kibana:6.1.4