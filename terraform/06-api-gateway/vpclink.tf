locals {
  name = "${var.name}-${var.env}"
}

data "aws_lb" "booking_elb" {
  name = var.alb_name 
}

resource "aws_api_gateway_vpc_link" "booking_vpc" {

  name        = "${local.name}-vpc"
  description = "vpc link for eks cluster"
  target_arns = ["${data.aws_lb.booking_elb.arn}"]
}