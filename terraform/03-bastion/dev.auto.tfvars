region = "ap-east-1"
key_name = "restairline"
env = "dev"
name = "restairline"

//change below values according your vpc
vpc_id = "vpc-03a5ef036341129a9"
subnet_id = "subnet-088057dbcc814b53b"

ami = "ami-c790d6b6"
instance_type = "t3.micro"

//set it to false if you don't have customize DNS
create_cname = true
zone_name = "reactlife.cn."
cname = "bastion.reactlife.cn."