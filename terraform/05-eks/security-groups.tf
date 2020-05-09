module "security_group" {
  source  = "git::https://github.com/terraform-aws-modules/terraform-aws-security-group.git?ref=master"

  name        = "restairline-eks-sg"
  description = "Security group for bastion"
  vpc_id      =  var.vpc_id

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
    env = var.env
  }
}