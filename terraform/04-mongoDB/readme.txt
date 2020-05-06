terraform init
terraform apply -var-file=dev.tfvars
ssh -i ~/.ssh/id_rsa.pub ubuntu@18.162.200.157


# install docker 

sudo apt-get update
sudo apt install docker.io
sudo systemctl start docker
sudo systemctl enable docker

sudo chmod 666 /var/run/docker.sock


# install mongoDB

