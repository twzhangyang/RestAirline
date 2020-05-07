variable "region" {
    type = string
    default = "ap-east-1"
}

variable "name" {
  type = string
  default = "restairline" 
}

variable "vpc_id" {
    type = string
}

variable "env" {
  type = string 
  default = "dev" 
}

variable "ami" {
  type = string
}

variable "instance_type" {
  type = string 
}

variable "subnet_id" {
  type = string
}

variable "key_name" {
  type = string 
}

variable "ssh_user" {
  type = string 
  default = "ubuntu"
}

variable "zone_name" {
  type = string 
}

variable "cname" {
 type = string
}



