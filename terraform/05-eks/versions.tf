terraform {
  required_version = "~> 0.12.0"
  required_providers {
    aws        = "~> 2.0"
    template   = "~> 2.0"
    null       = "~> 2.0"
    local      = "~> 1.3"
    kubernetes = "~> 1.11"
  }

  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/eks.tfstate"
    region = "ap-east-1"
  }
}
