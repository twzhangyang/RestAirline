region = "ap-east-1"
name = "restairline"
env = "dev"
vpc_id = "vpc-0a7a8ac8fa92833ff"
subnets = ["subnet-0ad730881b3ab3ba2", "subnet-0e55e3e0e32d2ab7b", "subnet-0f5669b325f8ab8a2"]
instance_type = "t3.small"
key_name = "restairline"

map_accounts = ["77777777"]
map_roles = [
    {
      rolearn  = "arn:aws:iam::66666666666:role/role1"
      username = "role1"
      groups   = ["system:masters"]
    },
  ]
map_users = [
    {
      userarn  = "arn:aws:iam::332679337602:user/test-user"
      username = "test-user"
      groups   = ["system:masters"]
    }
  ]

