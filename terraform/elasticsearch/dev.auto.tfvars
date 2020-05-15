region = "ap-east-1"
key_name = "restairline"
env = "dev"
name = "elasticsearch"

// change these values according to you vpc
vpc_id = "vpc-03a5ef036341129a9"
subnet_id = "subnet-088057dbcc814b53b"

ami = "ami-c790d6b6"
instance_type = "t3.micro"
ssh_user = "ubuntu"

//set it to flase if you don't have customize dns
create_cname = true
zone_name = "reactlife.cn."
cname = "elasticsearch.reactlife.cn."