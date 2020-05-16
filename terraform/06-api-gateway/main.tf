terraform {
  required_version = ">= 0.12.0"

  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/apigateway.tfstate"
    region = "ap-east-1"
  }
}

provider "aws" {
  version = ">= 2.28.1"
  region  = var.region
}

resource "null_resource" "booking_vpc" {
    depends_on = [aws_api_gateway_vpc_link.booking_vpc]
}

