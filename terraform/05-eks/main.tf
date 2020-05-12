terraform {
  required_version = ">= 0.12.0"

  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/eks.tfstate"
    region = "ap-east-1"
  }
}

provider "aws" {
  version = ">= 2.28.1"
  region  = var.region
}

provider "random" {
  version = "~> 2.1"
}

provider "local" {
  version = "~> 1.2"
}

provider "null" {
  version = "~> 2.1"
}

provider "kubernetes" {
  host                   = data.aws_eks_cluster.cluster.endpoint
  cluster_ca_certificate = base64decode(data.aws_eks_cluster.cluster.certificate_authority.0.data)
  token                  = data.aws_eks_cluster_auth.cluster.token
  load_config_file       = false
  version                = "~> 1.11"
}

locals {
  cluster_name = "${var.name}-${var.env}-eks"
}

module "eks" {
  source       = "git::https://github.com/terraform-aws-modules/terraform-aws-eks.git?ref=v11.1.0"
  cluster_name = local.cluster_name
  subnets      = var.subnets
  vpc_id       = var.vpc_id

  cluster_version = "1.16"

  // to fix module error
  worker_ami_name_filter = "amazon-eks-node-1.16-v20200423"
  worker_ami_owner_id    = "800184023465"
  wait_for_cluster_cmd   = "for i in `seq 1 60`; do curl -k -s $ENDPOINT/healthz >/dev/null && exit 0 || true; sleep 5; done; echo TIMEOUT && exit 1"

  worker_groups = [
    {
      name                          = "group-1"
      instance_type                 = var.instance_type
      key_name                      = var.key_name
      additional_userdata           = "echo foo bar"
      asg_desired_capacity          = 1
      asg_max_size                  = 3
      asg_min_size                  = 1
      additional_security_group_ids = [module.security_group.this_security_group_id]

      tags = [
        {
          "key"                 = "k8s.io/cluster-autoscaler/enabled"
          "propagate_at_launch" = "true"
          "value"               = "true"
        },
        {
          "key"                 = "k8s.io/cluster-autoscaler/${local.cluster_name}"
          "propagate_at_launch" = "true"
          "value"               = "true"
        }
      ]
    }
  ]

  tags = {
    env = var.env
  }
}

data "aws_eks_cluster" "cluster" {
  name = module.eks.cluster_id
}

data "aws_eks_cluster_auth" "cluster" {
  name = module.eks.cluster_id
}
