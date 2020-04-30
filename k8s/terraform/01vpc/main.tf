provider "aws" {
    region = var.region
}

module "vpc" {
#   source     = "git::https://github.com/cloudposse/terraform-aws-vpc.git?ref=master"
  source     = "git::https://github.com/terraform-aws-modules/terraform-aws-vpc.git?ref=master"
  name       = var.name
  cidr = "10.0.0.0/16"

  azs = var.availability_zones
  private_subnets = ["10.0.1.0/24", "10.0.2.0/24", "10.0.3.0/24"]
  public_subnets = ["10.0.101.0/24", "10.0.102.0/24", "10.0.103.0/24"]

  enable_ipv6 = false
  enable_nat_gateway = true
  single_nat_gateway = true

  public_subnet_tags = {
      name = "restairline-vpc-public"
  }

  private_subnet_tags = {
      name = "reastairline-vpc-private"
  }

  vpc_tags = {
      name = "restairline-vpc"
  }

  tags = {
      Environment = "dev"
      name = "restairline"
  }
}

