provider "aws" {
    region = var.region 
}

terraform {
  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/mongoDB.tfstate"
    region = "ap-east-1"
  }
}

module "security_group" {
  source  = "git::https://github.com/terraform-aws-modules/terraform-aws-security-group.git?ref=master"

  name        = "restairline-mongoDB-sg"
  description = "Security group for mongoDB"
  vpc_id      =  var.vpc_id

  ingress_with_cidr_blocks = [
      {
        from_port   = 27017 
        to_port     = 27017 
        protocol    = "tcp"
        description = "mongo DB"
        cidr_blocks = "0.0.0.0/0"
      },
      {
        from_port   = 22 
        to_port     = 22  
        protocol    = "tcp"
        description = "ssh"
        cidr_blocks = "0.0.0.0/0"
      }
    ]

  tags = {
    env = var.env
  }
}

module "ec2" {
  source = "git::https://github.com/terraform-aws-modules/terraform-aws-ec2-instance.git?ref=master"

  instance_count = 1

  name          = "reastairline-${var.name}"
  ami           = var.ami
  instance_type = var.instance_type
  subnet_id     = var.subnet_id
  vpc_security_group_ids      = [module.security_group.this_security_group_id]
  associate_public_ip_address = true
  key_name = var.key_name

  tags = {
    env      =  var.env
  }
}