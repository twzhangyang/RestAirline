region = "ap-east-1"
key_name = "restairline"
env = "dev"
name = "mongoDB"

// change these values accoridng you VPC
vpc_id = "vpc-0a7a8ac8fa92833ff"
subnet_id = "subnet-039a22209dda403fa"

ami = "ami-c790d6b6"
instance_type = "t3.micro"

// set it to false if you don't have customize DNS
create_cname = true
zone_name = "reactlife.cn."
cname = "mongodb.reactlife.cn."