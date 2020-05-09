provider "aws" {
  region = var.region
}

module "eks" {
  source       = "git::https://github.com/terraform-aws-modules/terraform-aws-eks.git?ref=v11.1.0"
  cluster_name = "${var.name}-${var.env}-eks"
  subnets      = var.subnets
  vpc_id       = var.vpc_id

  cluster_version = "1.16"

  // to fix module error
  worker_ami_name_filter = "amazon-eks-node-1.16-v20200423"
  worker_ami_owner_id    = "800184023465"
  wait_for_cluster_cmd   = "echo hello && exit 0"

  worker_groups = [
    {
      name                          = "worker-group-1"
      instance_type                 = var.instance_type
      key_name                      = var.key_name
      additional_userdata           = "echo foo bar"
      asg_desired_capacity          = 1
      asg_max_size                  = 3
      asg_min_size                  = 1
      additional_security_group_ids = [module.security_group.this_security_group_id]
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
