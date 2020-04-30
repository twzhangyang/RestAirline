provider "aws" {
    region = "ap-east-1"
}

terraform {
  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/bastion.tfstate"
    region = "ap-east-1"
  }
}

module "security_group" {
  source  = "git::https://github.com/terraform-aws-modules/terraform-aws-security-group.git?ref=master"

  name        = "restairline-bastion-sg"
  description = "Security group for bastion"
  vpc_id      = "vpc-0a7a8ac8fa92833ff"

  ingress_with_cidr_blocks = [
      {
        from_port   = 0
        to_port     = 65535
        protocol    = "tcp"
        description = "all tcp"
        cidr_blocks = "0.0.0.0/0"
      }
    ]

  egress_with_cidr_blocks = [
    {
      from_port   = 0
      to_port     = 65535
      protocol    = "tcp"
      description = "all tcp"
      cidr_blocks = "0.0.0.0/0"
    }
  ]

  tags = {
    env = "dev"
  }
}

module "ec2" {
  source = "git::https://github.com/terraform-aws-modules/terraform-aws-ec2-instance.git?ref=master"

  instance_count = 1

  name          = "reastairline-bastion"
  ami           = "ami-dd7731ac"
  instance_type = "t3.micro"
  subnet_id     = "subnet-039a22209dda403fa"
  vpc_security_group_ids      = [module.security_group.this_security_group_id]
  associate_public_ip_address = true
  key_name = "restairline"

  tags = {
    env      = "dev"
  }
}