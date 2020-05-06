provider "aws" {
    region = var.region 
}

terraform {
  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/elasticsearch.tfstate"
    region = "ap-east-1"
  }
}

module "security_group" {
  source  = "git::https://github.com/terraform-aws-modules/terraform-aws-security-group.git?ref=master"

  name        = "restairline-elasticsearch-sg"
  description = "Security group for elasticsearch"
  vpc_id      =  var.vpc_id

  ingress_with_cidr_blocks = [
      {
        from_port   = 22 
        to_port     = 22  
        protocol    = "tcp"
        description = "ssh"
        cidr_blocks = "0.0.0.0/0"
      },
      {
        from_port   = 9200 
        to_port     = 9200  
        protocol    = "tcp"
        description = "elastic search"
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
    env = var.env
  }
}

resource "aws_instance" "es"{
  ami           =  var.ami
  instance_type =  var.instance_type
  subnet_id = var.subnet_id
  vpc_security_group_ids      = [module.security_group.this_security_group_id]
  key_name = var.key_name

 connection {
    type  = "ssh"
    host  = aws_instance.es.public_ip
    user  = var.user 
    port  = 22
    agent = true
  }

  tags = { 
    Name = var.name
    env = var.env
  }
}

resource "null_resource" "provisioner" {
  triggers = {
    public_ip = aws_instance.es.public_ip
  }

  connection {
    type  = "ssh"
    host  = aws_instance.es.public_ip
    user  = var.ssh_user
    port  = 22
    agent = true
  }

  provisioner "remote-exec" {
    scripts = [
      "docker.sh"
    ]
  }
}