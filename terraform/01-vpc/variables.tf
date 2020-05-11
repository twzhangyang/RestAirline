variable "region" {
  type = string
}

variable "availability_zones" {
  type = list(string)
}

variable "name" {
  type = string
}

variable "env" {
  type = string
}

variable "cluster_name" {
  type = string 
}