provider "aws" {
  region = var.region
}

terraform {
  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/bastion.tfstate"
    region = "ap-east-1"
  }
}

locals {
  name = "${var.name}-${var.env}-bastion"
}

module "security_group" {
  source = "git::https://github.com/terraform-aws-modules/terraform-aws-security-group.git?ref=master"

  name        = local.name
  description = "Security group for bastion"
  vpc_id      = var.vpc_id

  ingress_with_cidr_blocks = [
    {
      from_port   = 22
      to_port     = 22
      protocol    = "tcp"
      description = "ssh"
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
    env  = var.env
    name = local.name
  }
}

module "ec2" {
  source = "git::https://github.com/terraform-aws-modules/terraform-aws-ec2-instance.git?ref=master"

  instance_count = 1

  name                        = local.name
  ami                         = var.ami
  instance_type               = var.instance_type
  subnet_id                   = var.subnet_id
  vpc_security_group_ids      = [module.security_group.this_security_group_id]
  associate_public_ip_address = true
  key_name                    = var.key_name

  tags = {
    env  = var.env
    name = local.name
  }
}

data "aws_route53_zone" "default" {
  name         = var.zone_name
  private_zone = false
}

resource "aws_route53_record" "elasticsearch" {
  zone_id         = data.aws_route53_zone.default.zone_id
  name            = var.cname
  type            = "A"
  ttl             = "300"
  records         = [module.ec2.public_ip[0]]
  allow_overwrite = true
}

resource "null_resource" "provisioner" {
  triggers = {
    public_ip = module.ec2.public_ip[0]
  }

  connection {
    type  = "ssh"
    host  = module.ec2.public_ip[0]
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
