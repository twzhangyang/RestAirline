output "domain_name" {
  value = aws_cloudfront_distribution.default.domain_name
}

output "id" {
  value = aws_cloudfront_distribution.default.id
}

output "arn" {
  value = aws_cloudfront_distribution.default.arn
}

output "cname" {
  value = aws_route53_record.restairline.name
}