terraform {
  required_version = ">= 0.12.0"

  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/cloudfront.tfstate"
    region = "ap-east-1"
  }
}

provider "aws" {
  version = ">= 2.28.1"
  region  = var.region
}

locals {
  origin_id = "Custom-${var.api_gateway_domain}"
}


resource "aws_cloudfront_distribution" "default" {
  comment = "The cdn of the restariline apis"
  enabled = true
  aliases = ["${var.cname}"]

  origin {
    domain_name = var.api_gateway_domain
    origin_id   = local.origin_id
    origin_path = "/${var.env}"

    custom_origin_config {
      http_port              = "80"
      https_port             = "443"
      origin_protocol_policy = "https-only"
      origin_ssl_protocols   = ["TLSv1.2"]
    }
  }

  viewer_certificate {
    acm_certificate_arn            = "${var.acm_certificate_arn}"
    ssl_support_method             = "sni-only"
    minimum_protocol_version       = "TLSv1.2_2018"
    cloudfront_default_certificate = "${var.acm_certificate_arn == "" ? true : false}"
  }

  default_cache_behavior {
    allowed_methods  = ["HEAD", "GET", "OPTIONS"]
    cached_methods   = ["GET", "HEAD"]
    target_origin_id = local.origin_id
    compress         = false

    forwarded_values {
      headers = []

      query_string = "false"

      cookies {
        forward = "none"
      }
    }

    viewer_protocol_policy = "redirect-to-https"
    default_ttl            = "60"
    min_ttl                = "0"
    max_ttl                = "31536000"
  }

  web_acl_id = ""

  restrictions {
    geo_restriction {
      restriction_type = "none"
      locations        = []
    }
  }

  tags = {
    env  = var.env
    name = var.name
  }
}

data "aws_route53_zone" "default" {
  name         = var.zone_name
  private_zone = false
}

resource "aws_route53_record" "restairline" {
  zone_id         = data.aws_route53_zone.default.zone_id
  name            = var.cname
  type            = "CNAME"
  ttl             = "300"
  records         = [aws_cloudfront_distribution.default.domain_name]
  allow_overwrite = true
}
