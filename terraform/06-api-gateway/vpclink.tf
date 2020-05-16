locals {
  name = "${var.name}-${var.env}"
  vpc_link_name = "${local.name}-vpc"
}

data "aws_lb" "booking_elb" {
  name = var.alb_name 
}

resource "aws_api_gateway_vpc_link" "booking_vpc" {

  name        = local.vpc_link_name
  description = "vpc link for eks cluster"
  target_arns = ["${data.aws_lb.booking_elb.arn}"]
}