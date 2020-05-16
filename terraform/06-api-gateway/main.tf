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

data "aws_api_gateway_vpc_link" "booking_vpc" {
  name = local.vpc_link_name
}

resource "aws_api_gateway_rest_api" "booking" {
  name        = "booking"
  description = "booking apis for restairline"
}


resource "aws_api_gateway_resource" "booking" {
  rest_api_id = aws_api_gateway_rest_api.booking.id
  parent_id   = aws_api_gateway_rest_api.booking.root_resource_id
  path_part   = "booking"
}

resource "aws_api_gateway_resource" "home" {
  rest_api_id = aws_api_gateway_rest_api.booking.id
  parent_id   = aws_api_gateway_resource.booking.id
  path_part   = "home"
}

resource "aws_api_gateway_method" "home_get" {
  rest_api_id   = aws_api_gateway_rest_api.booking.id
  resource_id   = aws_api_gateway_resource.home.id
  http_method   = "GET"
  authorization = "NONE"

  request_models = {
    "application/json" = "Error"
  }
}

resource "aws_api_gateway_integration" "home_get" {
  depends_on = [aws_api_gateway_vpc_link.booking_vpc]

  rest_api_id = "${aws_api_gateway_rest_api.booking.id}"
  resource_id = "${aws_api_gateway_resource.home.id}"
  http_method = "${aws_api_gateway_method.home_get.http_method}"

  request_templates = {
    "application/json" = ""
  }

  request_parameters = {
    "integration.request.header.X-Authorization" = "'static'"
    "integration.request.header.X-Foo"           = "'Bar'"
  }

  type                    = "HTTP_PROXY"
  uri                     = "http://ae86ab60655e5491ebebb227fc1659e9-794585ebfabade3d.elb.ap-east-1.amazonaws.com/booking/home"
  integration_http_method = "GET"
  passthrough_behavior    = "WHEN_NO_MATCH"
  content_handling        = "CONVERT_TO_TEXT"

  connection_type = "VPC_LINK"
  connection_id   = "${aws_api_gateway_vpc_link.booking_vpc.id}"
}

resource "aws_api_gateway_deployment" "booking" {
  depends_on = [
    aws_api_gateway_integration.home_get
  ]

  rest_api_id = aws_api_gateway_rest_api.booking.id
  stage_name  = "test"
}

