variable "env" {
  type    = string
}

variable "name" {
  type    = string
}

variable "vpc_id" {
  type = string
}

variable "region" {
  type        = string
}

variable "subnets" {
  description = "A list of subnet IDs to launch the cluster in"
  type        = list(string)
}

variable "instance_type" {
  type = string
}

variable "key_name" {
  type = string
}

variable "map_accounts" {
  description = "Additional AWS account numbers to add to the aws-auth configmap."
  type        = list(string)
}

variable "map_roles" {
  description = "Additional IAM roles to add to the aws-auth configmap."
  type = list(object({
    rolearn  = string
    username = string
    groups   = list(string)
  }))
}

variable "map_users" {
  description = "Additional IAM users to add to the aws-auth configmap."
  type = list(object({
    userarn  = string
    username = string
    groups   = list(string)
  }))
}

