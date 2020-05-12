provider "aws" {
  region = var.region
}

terraform {
  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/vpc.tfstate"
    region = "ap-east-1"
  }
}

locals {
  # The usage of the specific kubernetes.io/cluster/* resource tags below are required
  # for EKS and Kubernetes to discover and manage networking resources
  # https://docs.aws.amazon.com/eks/latest/userguide/network_reqs.html
  eks_tag = map("kubernetes.io/cluster/${var.cluster_name}", "shared")
  name    = "${var.name}-${var.env}"
}

module "vpc" {
  source = "git::https://github.com/terraform-aws-modules/terraform-aws-vpc.git?ref=master"
  name   = local.name
  cidr   = "10.0.0.0/16"

  azs             = var.availability_zones
  private_subnets = ["10.0.1.0/24", "10.0.2.0/24", "10.0.3.0/24"]
  public_subnets  = ["10.0.101.0/24", "10.0.102.0/24", "10.0.103.0/24"]

  enable_nat_gateway   = true
  single_nat_gateway   = true
  enable_dns_hostnames = true
  enable_dns_support   = true


  public_subnet_tags = merge({
    env                      = var.env
    "kubernetes.io/role/elb" = "1"
  }, local.eks_tag)

  public_route_table_tags = {
    env = var.env
  }

  private_subnet_tags = merge({
    env                               = var.env
    "kubernetes.io/role/internal-elb" = 1
  }, local.eks_tag)

  private_route_table_tags = {
    env = var.env
  }

  default_network_acl_name = local.name
  default_network_acl_tags = {
    env = var.env
  }

  igw_tags = {
    env = var.env
  }

  nat_eip_tags = {
    env = var.env
  }

  nat_gateway_tags = {
    env = var.env
  }

  tags = merge({
    env = var.env
  }, local.eks_tag)
}

