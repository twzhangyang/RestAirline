region = "ap-east-1"
name = "restairline"
env = "dev"
vpc_id = "vpc-03a5ef036341129a9"
subnets = ["subnet-0837525fc3ad85ba2", "subnet-088057dbcc814b53b", "subnet-0ab8c8eb16ffcb06d"]
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

