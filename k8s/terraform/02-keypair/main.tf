terraform {
  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/keypair"
    region = "ap-east-1"
  }
}

provider "aws" {
 region = var.region 
}

resource "aws_key_pair" "this" {
  key_name        = var.key_name
  public_key      = file(var.public_key_path)

  tags = {
      env = var.env
  }
}