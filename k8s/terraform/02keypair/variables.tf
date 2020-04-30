variable "region" {
  type = string 
  default = "ap-east-1" 
}

variable "key_name" {
    type = string
    default = "restairline"
}

variable "public_key_path" {
  description = <<DESCRIPTION
Path to the SSH public key to be used for authentication.
Ensure this keypair is added to your local SSH agent so provisioners can
connect.
Example: ~/.ssh/terraform.pub
DESCRIPTION
  default     = "~/.ssh/id_rsa.pub"
}

variable "env" {
  type = string
  default = "dev" 
}
