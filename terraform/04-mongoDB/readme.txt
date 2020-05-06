terraform init
terraform apply -var-file=dev.tfvars
ssh -i ~/.ssh/id_rsa.pub ubuntu@18.162.200.157
