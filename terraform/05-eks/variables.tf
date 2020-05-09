variable "env" {
  type        = string
  default     = "dev"
}

variable "name" {
  type = string 
  default = "restairline"
}

variable "vpc_id" {
  type = string
}

variable "region" {
  type        = string
  description = "ap-east-1"
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


