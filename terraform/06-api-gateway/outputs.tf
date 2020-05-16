output "vpc_link_id" {
  description = "The id of the vpc link"
  value       =  aws_api_gateway_vpc_link.booking_vpc.id 
}

output "invoke_url" {
  value = aws_api_gateway_deployment.booking.invoke_url
}

output "execution_arn" {
  value = aws_api_gateway_deployment.booking.execution_arn
}